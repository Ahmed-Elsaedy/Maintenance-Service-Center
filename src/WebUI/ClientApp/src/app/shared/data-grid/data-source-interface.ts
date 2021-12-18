import { Observable } from "rxjs";

export interface IGridDataSource {
    query: any;
    loadData: () => void;
    data: any[];
    totalRecords: number;
    loading$: Observable<boolean>
}
