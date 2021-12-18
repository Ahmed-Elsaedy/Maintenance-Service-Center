import { Component, OnInit, Input, ViewChild, AfterViewInit, Output, EventEmitter, ElementRef } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { SelectionModel } from '@angular/cdk/collections';
import { merge } from 'rxjs';
import { tap } from 'rxjs/operators';
import { IGridDataSource } from './data-source-interface';
import { MatInput } from '@angular/material/input';
import { Direction } from '@angular/cdk/bidi';


export function getDirection() {
  return <Direction>document.querySelector('html').getAttribute('dir');
}

export interface IDataGridOptions {
  columns: { title: string, name: string, mapper?: (obj) => string, hidden?: boolean, width?: string, sortable?: boolean }[],
  options?: { id: number, title: string, icon?: string, disabled?: boolean, execute?: (e) => void }[],
  dataSource: IGridDataSource,
  selectionModel: SelectionModel<any>
  localize?: boolean,
  enableOptions?: boolean,
  enableSearch?: boolean,
  optionsColumnWidth?: string,
  width?: string
}

@Component({
  selector: 'app-data-grid',
  templateUrl: './data-grid.component.html',
  styleUrls: ['./data-grid.component.css']
})
export class DataGridComponent implements OnInit, AfterViewInit {

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;
  @Input() options: IDataGridOptions;
  @ViewChild('search') search: ElementRef;
  dir: Direction;

  public get displayedColumns(): string[] {
    var result = ['Select', ...this.options?.columns.filter(x => !x.hidden).map(x => x.name)];
    if (this.options?.enableOptions)
      result.push('Options');
    return result;
  }

  constructor() { }

  ngOnInit() {
    this.dir = getDirection();
  }

  isAllSelected() {
    const numSelected = this.options.selectionModel.selected.length;
    const numRows = this.options.dataSource.data.length;
    return numSelected == numRows;
  }

  masterToggle() {
    this.isAllSelected() ?
      this.options.selectionModel.clear() :
      this.options.dataSource.data.forEach(row => this.options.selectionModel.select(row));
  }

  ngAfterViewInit() {
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);
    this.paginator.page.subscribe(() => this.options.selectionModel.clear());
    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        tap(() => this.loadData())
      ).subscribe();
  }

  private loadData() {
    this.options.dataSource.query.pageIndex = this.paginator.pageIndex;
    this.options.dataSource.query.pageLength = this.paginator.pageSize;
    this.options.dataSource.query.sort = this.sort.active && this.sort.direction ? `${this.sort.active} ${this.sort.direction}` : '';
    this.options.dataSource.query.filter = this.search ? this.search.nativeElement.value : null;
    this.options.dataSource.loadData();
  }

  onSearch() {
    this.loadData();
  }

  onClear() {
    this.search.nativeElement.value = '';
    this.loadData();
  }

  onRowClick(row) {
    this.options.selectionModel.select(row);
  }
}
