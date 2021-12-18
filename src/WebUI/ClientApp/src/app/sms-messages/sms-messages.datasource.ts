import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { SMSMessagesClient, SearchSMSMessagesViewModel, SearchSMSMessageDto } from "../ElarabyCA-api";
import { BehaviorSubject, Observable, of } from "rxjs";
import { catchError, finalize } from "rxjs/operators";
import { IGridDataSource } from "../shared/data-grid/data-source-interface";

export class SMSMessagesDataSource implements DataSource<SearchSMSMessageDto>, IGridDataSource {

    private smsMessagesSubject = new BehaviorSubject<SearchSMSMessageDto[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);
    public loading$ = this.loadingSubject.asObservable();

    public totalRecords: number = 0;
    public data: SearchSMSMessageDto[];

    public query: any = {
        pageIndex: 0,
        pageLength: 10,
        sort: "Id desc",
        filter: ''
    };

    constructor(private smsMessagesClient: SMSMessagesClient) { }
    loadData() {
        this.loadingSubject.next(true);
        this.smsMessagesClient.search(this.query)
            .pipe(
                catchError(() => of([])),
                finalize(() => this.loadingSubject.next(false)))
            .subscribe((res: SearchSMSMessagesViewModel) => {
                this.totalRecords= res.totalRecords;
                this.smsMessagesSubject.next(res.data);
                this.data = res.data;
            });
    }

    connect(collectionViewer: CollectionViewer): Observable<SearchSMSMessageDto[] | readonly SearchSMSMessageDto[]> {
        return this.smsMessagesSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.smsMessagesSubject.complete();
        this.loadingSubject.complete();
    }
}