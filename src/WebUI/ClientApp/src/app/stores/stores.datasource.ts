import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { SearchStoreDto, StoresClient, SearchStoresQueryViewModel, SearchStoresQuery } from "../ElarabyCA-api";
import { BehaviorSubject, Observable, of } from "rxjs";
import { catchError, finalize } from "rxjs/operators";
import { IGridDataSource } from "../shared/data-grid/data-source-interface";

export class StoresDataSource implements DataSource<SearchStoreDto>, IGridDataSource {

    private storesSubject = new BehaviorSubject<SearchStoreDto[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);
    public loading$ = this.loadingSubject.asObservable();

    public totalRecords: number = 0;
    public data: SearchStoreDto[];

    public query: any = {
        pageIndex: 0,
        pageLength: 10,
        sort: "StoreId desc",
        filter: ''
    };

    constructor(private storesClient: StoresClient) { }
    loadData() {
        this.loadingSubject.next(true);
        this.storesClient.search(this.query)
            .pipe(
                catchError(() => of([])),
                finalize(() => this.loadingSubject.next(false)))
            .subscribe((res: SearchStoresQueryViewModel) => {
                this.totalRecords= res.totalRecords;
                this.storesSubject.next(res.data);
                this.data = res.data;
            });
    }

    connect(collectionViewer: CollectionViewer): Observable<SearchStoreDto[] | readonly SearchStoreDto[]> {
        return this.storesSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.storesSubject.complete();
        this.loadingSubject.complete();
    }
}