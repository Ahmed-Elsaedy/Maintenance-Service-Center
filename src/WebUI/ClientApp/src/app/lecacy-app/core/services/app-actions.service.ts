import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AppActionsService {
  actions: AppAction[] = [];

  constructor() {

    this.actions.push({ type: ActionType.NewOrder, title: 'indexView.create', disabled: false, group: ActionGroup.Orders, visible: false });
    this.actions.push({ type: ActionType.EditOrder, title: 'indexView.edit', disabled: true, group: ActionGroup.Orders, selMode: SelectionMode.Single, visible: false });
    this.actions.push({ type: ActionType.DeleteOrder, title: 'indexView.delete', disabled: true, group: ActionGroup.Orders, selMode: SelectionMode.Single, visible: false });
    this.actions.push({ type: ActionType.AddReport, title: 'indexView.addOrderReport', disabled: true, group: ActionGroup.Orders, selMode: SelectionMode.Single, visible: false });
    this.actions.push({ type: ActionType.CopyOrderToClipboard, title: 'indexView.copyOrderToClipboard', disabled: true, group: ActionGroup.Orders, selMode: SelectionMode.Multi, visible: false });
    this.actions.push({ type: ActionType.CopyIDsToClipboard, title: 'indexView.copyIDsToClipboard', disabled: true, group: ActionGroup.Orders, selMode: SelectionMode.Multi, visible: false });
    // this.actions.push({ type: ActionType.PatchEdit, title: 'Patch', disabled: true, group: ActionGroup.Orders, selMode: SelectionMode.Multi, visible: false });
    this.actions.push({ type: ActionType.OrderQuery, title: 'indexView.filter', disabled: false, group: ActionGroup.Orders, visible: false });

    this.actions.push({ type: ActionType.NewTicket, title: 'indexView.create', disabled: false, group: ActionGroup.Tickets, visible: false });
    this.actions.push({ type: ActionType.EditTicket, title: 'indexView.edit', disabled: true, group: ActionGroup.Tickets, selMode: SelectionMode.Single, visible: false });
    this.actions.push({ type: ActionType.DeleteTicket, title: 'indexView.delete', disabled: true, group: ActionGroup.Tickets, selMode: SelectionMode.Single, visible: false });

    this.actions.push({ type: ActionType.NewEmployee, title: 'indexView.create', disabled: false, group: ActionGroup.Employees, visible: false });
    this.actions.push({ type: ActionType.EditEmployee, title: 'indexView.edit', disabled: true, group: ActionGroup.Employees, selMode: SelectionMode.Single, visible: false });
    this.actions.push({ type: ActionType.DeleteEmployee, title: 'indexView.delete', disabled: true, group: ActionGroup.Employees, selMode: SelectionMode.Single, visible: false });

    this.actions.push({ type: ActionType.NewCategory, title: 'indexView.create', disabled: false, group: ActionGroup.Categories, visible: false });
    this.actions.push({ type: ActionType.EditCategory, title: 'indexView.edit', disabled: true, group: ActionGroup.Categories, selMode: SelectionMode.Single, visible: false });
    this.actions.push({ type: ActionType.DeleteCategory, title: 'indexView.delete', disabled: true, group: ActionGroup.Categories, selMode: SelectionMode.Single, visible: false });

    this.actions.push({ type: ActionType.DownloadWorkOrders, title: 'indexView.download', disabled: true, group: ActionGroup.Elaraby, selMode: SelectionMode.Manual, visible: false });
  }

  restoreGroup(group: ActionGroup) {
    this.actions.filter(x => x.group == group && x.selMode).forEach(x => x.disabled = true);
  }

  updateActionsPerUser(user: any) {
    if (user.roles?.includes("Admin")) {

    } else {
      this.actions.forEach(element => {
        element.visible = true;
      });
    }
  }

}

export interface AppAction {
  type: ActionType,
  title: string,
  disabled: boolean,
  group: ActionGroup,
  selMode?: SelectionMode
  visible: boolean
}

export enum ActionType {
  NewOrder = 0,
  EditOrder = 1,
  DeleteOrder = 2,
  AddReport = 12,
  PatchEdit = 13,
  OrderQuery = 14,
  NewTicket = 3,
  EditTicket = 4,
  DeleteTicket = 5,
  NewEmployee = 6,
  EditEmployee = 7,
  DeleteEmployee = 8,
  NewCategory = 9,
  EditCategory = 10,
  DeleteCategory = 11,
  DownloadWorkOrders = 12,
  CopyOrderToClipboard = 15,
  CopyIDsToClipboard = 16
}

export enum ActionGroup {
  Orders,
  Tickets,
  Employees,
  Categories,
  Elaraby
}

export enum SelectionMode {
  Single = 1,
  Multi = 2,
  Manual = 3
}
