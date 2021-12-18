import { WhereFilterType } from './where-filter-type.enum';
import { FieldType } from './field-type.enum';

export class WhereCondition {
    constructor(
        public type: WhereFilterType,
        public title: string,
        public fieldType: FieldType,
        public requiresValue: boolean) {
    }
}