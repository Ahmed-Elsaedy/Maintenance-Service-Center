export class ToolbarAction {
    disabled: boolean;
    execute: Function;
    constructor(public id: number, public title: string, enabled?: boolean) {
        this.disabled = !enabled ? true : false;
    }
}