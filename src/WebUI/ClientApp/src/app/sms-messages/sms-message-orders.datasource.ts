import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { SMSMessagesClient, SearchSMSMessagesViewModel, SMSMessageDto, OrderSMSMessage, SearchOrderSMSMessageDto, SearchOrderSMSMessagesQueryViewModel, OrderSMSMessagesClient } from "../ElarabyCA-api";
import { BehaviorSubject, Observable, of } from "rxjs";
import { catchError, finalize } from "rxjs/operators";
import { IGridDataSource } from "../shared/data-grid/data-source-interface";

export class SMSMessageOrdersDataSource implements DataSource<SearchOrderSMSMessageDto>, IGridDataSource {

    private smsMessagesSubject = new BehaviorSubject<SearchOrderSMSMessageDto[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);
    public loading$ = this.loadingSubject.asObservable();

    public totalRecords: number = 0;
    public data: SearchOrderSMSMessageDto[];

    public query: any = {
        pageIndex: 0,
        pageLength: 10,
        sort: "Id desc",
        filter: ''
    };

    constructor(private orderSMSMessagesClient: OrderSMSMessagesClient) { }
    loadData() {
        this.loadingSubject.next(true);
        this.orderSMSMessagesClient.search(this.query)
            .pipe(
                catchError(() => of([])),
                finalize(() => this.loadingSubject.next(false)))
            .subscribe((res: SearchOrderSMSMessagesQueryViewModel) => {
                this.totalRecords= res.totalRecords;
                this.smsMessagesSubject.next(res.data);
                this.data = res.data;
            });
    }

    connect(collectionViewer: CollectionViewer): Observable<SMSMessageDto[] | readonly SMSMessageDto[]> {
        return this.smsMessagesSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.smsMessagesSubject.complete();
        this.loadingSubject.complete();
    }
}