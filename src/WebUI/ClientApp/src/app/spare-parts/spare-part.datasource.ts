import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { SearchSparePartDto, SparePartsClient, SearchSparePartsViewModel } from "../ElarabyCA-api";
import { BehaviorSubject, Observable, of } from "rxjs";
import { catchError, finalize } from "rxjs/operators";
import { IGridDataSource } from "../shared/data-grid/data-source-interface";

export class SpartPartsDataSource implements DataSource<SearchSparePartDto>, IGridDataSource {

    private sparePartsSubject = new BehaviorSubject<SearchSparePartDto[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);
    public loading$ = this.loadingSubject.asObservable();

    public totalRecords: number = 0;
    public data: SearchSparePartDto[];

    public query: any = {
        pageIndex: 0,
        pageLength: 10,
        sort: "SparePartId desc",
        filter: ''
    };

    constructor(private sparePartsClient: SparePartsClient) { }
    loadData() {
        this.loadingSubject.next(true);
        this.sparePartsClient.search(this.query)
            .pipe(
                catchError(() => of([])),
                finalize(() => this.loadingSubject.next(false)))
            .subscribe((res: SearchSparePartsViewModel) => {
                this.totalRecords = res.totalRecords;
                this.sparePartsSubject.next(res.data);
                this.data = res.data;
            });
    }

    connect(collectionViewer: CollectionViewer): Observable<SearchSparePartDto[] | readonly SearchSparePartDto[]> {
        return this.sparePartsSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.sparePartsSubject.complete();
        this.loadingSubject.complete();
    }
}