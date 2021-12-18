import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { BehaviorSubject, Observable, of } from "rxjs";
import { catchError, finalize } from "rxjs/operators";
import { IGridDataSource } from "../shared/data-grid/data-source-interface";
import { SearchITransactionDto, FinancialTransactionsClient, SearchInventoriesQueryViewModel, SearchInventoryDto, SearchFinancialTransactionDto, SearchFinancialTransactionsQueryViewModel } from "../ElarabyCA-api";

export class FinancialTransactionsDataSource implements DataSource<SearchFinancialTransactionDto>, IGridDataSource {

    private transactionsSubject = new BehaviorSubject<SearchFinancialTransactionDto[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);
    public loading$ = this.loadingSubject.asObservable();

    public totalRecords: number = 0;
    public data: SearchFinancialTransactionDto[];s

    public query: any = {
        pageIndex: 0,
        pageLength: 10,
        sort: "Date desc",
        filter: ''
    };

    constructor(private transactionsClient: FinancialTransactionsClient) { }
    loadData() {
        this.loadingSubject.next(true);
        this.transactionsClient.search(this.query)
            .pipe(
                catchError(() => of([])),
                finalize(() => this.loadingSubject.next(false)))
            .subscribe((res: SearchFinancialTransactionsQueryViewModel) => {
                this.totalRecords = res.totalRecords;
                this.transactionsSubject.next(res.data);
                this.data = res.data;
            });
    }

    connect(collectionViewer: CollectionViewer): Observable<SearchFinancialTransactionDto[] | readonly SearchFinancialTransactionDto[]> {
        return this.transactionsSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.transactionsSubject.complete();
        this.loadingSubject.complete();
    }
}