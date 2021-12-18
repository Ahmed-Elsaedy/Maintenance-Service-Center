import { FieldType } from './field-type.enum';

export class FilterableField {
    constructor(
        public name: string,
        public title: string,
        public type: FieldType) {
    }
}