import { WhereFilterType } from "./where-filter-type.enum";
import { TreeFilterType } from "./tree-filter-type.enum";
export class TreeFilter {
    id: number;
    field: string;
    filterType: WhereFilterType;
    value: any;
    operatorType: TreeFilterType;
    operands: TreeFilter[];
    constructor() {
        this.filterType = WhereFilterType.None;
        this.operatorType = TreeFilterType.None;
    }
}
