import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { BehaviorSubject, Observable, of } from "rxjs";
import { catchError, finalize } from "rxjs/operators";
import { IGridDataSource } from "../shared/data-grid/data-source-interface";
import { SearchITransactionDto, InventoryTransactionsClient, SearchInventoriesQueryViewModel, SearchInventoryDto } from "../ElarabyCA-api";

export class InventoryTransactionsDataSource implements DataSource<SearchITransactionDto>, IGridDataSource {

    private transactionsSubject = new BehaviorSubject<SearchITransactionDto[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);
    public loading$ = this.loadingSubject.asObservable();

    public totalRecords: number = 0;
    public data: SearchITransactionDto[];

    public query: any = {
        pageIndex: 0,
        pageLength: 10,
        sort: "TransactionId desc",
        filter: ''
    };

    constructor(private transactionsClient: InventoryTransactionsClient) { }
    loadData() {
        this.loadingSubject.next(true);
        this.transactionsClient.search(this.query)
            .pipe(
                catchError(() => of([])),
                finalize(() => this.loadingSubject.next(false)))
            .subscribe((res: SearchInventoriesQueryViewModel) => {
                this.totalRecords = res.totalRecords;
                this.transactionsSubject.next(res.data);
                this.data = res.data;
            });
    }

    connect(collectionViewer: CollectionViewer): Observable<SearchInventoryDto[] | readonly SearchInventoryDto[]> {
        return this.transactionsSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.transactionsSubject.complete();
        this.loadingSubject.complete();
    }
}