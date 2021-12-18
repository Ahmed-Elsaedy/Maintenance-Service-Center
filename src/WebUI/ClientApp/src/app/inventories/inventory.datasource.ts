import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { SearchSparePartDto, SparePartsClient, SearchSparePartsViewModel, SearchInventoryDto, InventoriesClient } from "../ElarabyCA-api";
import { BehaviorSubject, Observable, of } from "rxjs";
import { catchError, finalize } from "rxjs/operators";
import { IGridDataSource } from "../shared/data-grid/data-source-interface";

export class InventoriesDataSource implements DataSource<SearchInventoryDto>, IGridDataSource {

    private inventoriesSubject = new BehaviorSubject<SearchInventoryDto[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);
    public loading$ = this.loadingSubject.asObservable();

    public totalRecords: number = 0;
    public data: SearchInventoryDto[];

    public query: any = {
        pageIndex: 0,
        pageLength: 10,
        sort: "InventoryId desc",
        filter: ''
    };

    constructor(private inventoriesClient: InventoriesClient) { }
    loadData() {
        this.loadingSubject.next(true);
        this.inventoriesClient.search(this.query)
            .pipe(
                catchError(() => of([])),
                finalize(() => this.loadingSubject.next(false)))
            .subscribe((res: SearchSparePartsViewModel) => {
                this.totalRecords= res.totalRecords;
                this.inventoriesSubject.next(res.data);
                this.data = res.data;
            });
    }

    connect(collectionViewer: CollectionViewer): Observable<SearchInventoryDto[] | readonly SearchInventoryDto[]> {
        return this.inventoriesSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.inventoriesSubject.complete();
        this.loadingSubject.complete();
    }
}