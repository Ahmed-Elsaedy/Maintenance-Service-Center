import {
  Component,
  OnInit,
  AfterViewInit,
  EventEmitter,
  Output,
  Input,
  OnDestroy,
  AfterViewChecked,
} from "@angular/core";
import { Subject, Subscription } from "rxjs";
import {
  FormGroup,
  FormBuilder,
  FormControl,
  Validators,
} from "@angular/forms";
import { LangChangeEvent, TranslateService } from "@ngx-translate/core";

@Component({
  selector: "app-datatable",
  templateUrl: "./datatable.component.html",
  styleUrls: ["./datatable.component.css"],
})
export class DatatableComponent implements OnInit, AfterViewInit, OnDestroy {
  @Output() private dtSelectionChanged: EventEmitter<[]> = new EventEmitter<
    []
  >();
  @Output() private dtColumnActionExecute: EventEmitter<any> =
    new EventEmitter<any>();
  @Input() private dtOptions: any;
  @Input() private dtRedraw: Subject<any>;
  @Input() private dtRedrawRow: Subject<any>;
  @Input() private dtSelectNone: Subject<any>;
  private dataTableObj: any;
  private subs: Subscription[] = [];

  filterColumnGroup: FormGroup;
  patchEditGroup: FormGroup;

  constructor(
    private fb: FormBuilder,
    private translateService: TranslateService
  ) {}

  ngOnInit(): void {
    this.filterColumnGroup = this.fb.group({
      column: new FormControl("", [Validators.required]),
      operator: new FormControl("", [Validators.required]),
      value: new FormControl("", [Validators.required]),
    });
  }

  selectionChanged(selection: []) {
    this.dtSelectionChanged.emit(selection);
    if (selection.length > 0) {
      $(`#datatable thead tr th li[action="${ColumnAction.Patch}"]`).show();
    } else {
      $(`#datatable thead tr th li[action="${ColumnAction.Patch}"]`).hide();
    }
  }

  ngAfterViewInit(): void {
    // this.dtOptions.language = {
    //   url : "https://cdn.datatables.net/plug-ins/1.13.5/i18n/ar.json"
    // }

    // this.translateService.getTranslation("en").subscribe(() => {
    //   debugger;

    // });

    this.translateService.onLangChange.subscribe((event: LangChangeEvent) => {
      console.log("lang change");
      if (this.dataTableObj) {
        const headerElements = $("#datatable").find("thead th");
        for (let i = 0; i < headerElements.length; i++) {
          const column = this.dtOptions.columns[i];
          const translatedTitle = this.translateService.instant(column.title);
          $(headerElements[i]).text(translatedTitle);
        }
      }
    });

    // for (const column of this.dtOptions.columns) {
    //   column.title = this.translateService.instant(column.title);
    // }

    this.dataTableObj = $("#datatable").DataTable(this.dtOptions);
    var selectionHandler = (e, dt, type, indexes) => {
      var rowData: [] = this.dataTableObj
        .rows({ selected: true })
        .data()
        .toArray();
      this.selectionChanged(rowData);
    };
    this.dataTableObj.on("select", selectionHandler);
    this.dataTableObj.on("deselect", selectionHandler);

    if (this.dtRedraw)
      this.subs.push(
        this.dtRedraw.subscribe((x) => {
          this.selectionChanged([]);
          this.dataTableObj.ajax.reload(null, false);
        })
      );
    if (this.dtRedrawRow)
      this.subs.push(
        this.dtRedrawRow.subscribe((data) => {
          this.dataTableObj.row({ selected: true }).data(data).draw();
          //this.dataTableObj.rows(x.row).invalidate("dom").draw();
          // this.dataTableObj.fnUpdate(x.data,x.row,undefined,false);
        })
      );
    if (this.dtSelectNone)
      this.subs.push(
        this.dtSelectNone.subscribe(() => {
          this.dataTableObj.rows().deselect();
        })
      );

    var d = $("#datatable tbody tr:nth(1)");
    var row = this.dataTableObj.row(d);
    var dd = row.data();

    $("#datatable_filter input").unbind();
    $("#datatable_filter input").on("keyup", (e: any) => {
      if (e.keyCode == 13) {
        this.dataTableObj.search(e.target.value).draw();
      }
    });

    // var tr = $('<tr></tr>');
    // $('#datatable thead tr:eq(0) th').each(function (i) {
    //   var title = $(this).text();
    //   tr.append(`
    //   <th style="text-align: center;">
    //     <div class="btn-group">
    //       <button data-toggle="dropdown" class="btn btn-default dropdown-toggle btn-xs" type="button">
    //           <span class="glyphicon glyphicon-cog" aria-hidden="true"></span>
    //           <span class="caret"></span>
    //       </button>
    //       <ul role="menu" class="dropdown-menu">
    //       <li colIndex="${i}" colTitle="${title}" action="${ColumnAction.Filter}" ><a class="dropdown-item "><span class="glyphicon glyphicon-filter" aria-hidden="true"> </span> Filter Data</a></li>
    //       <li colIndex="${i}" colTitle="${title}" action="${ColumnAction.Patch}" ><a class="dropdown-item "><span class="glyphicon glyphicon-filter" aria-hidden="true"> </span> Patch Edit</a></li>
    //       </ul>
    //     </div>
    //   </th>`);
    // });
    // $('#datatable thead').append(tr);

    // $('#datatable thead tr th li').on('click', (e) => {
    //   var li = $(e.target).closest('li');
    //   this.dtColumnActionExecute.emit({
    //     index: li.attr('colIndex'),
    //     title: li.attr('colTitle'),
    //     action: <ColumnAction>parseInt(li.attr('action'))
    //   })
    // });

    // $(`#datatable thead tr th li[action="${ColumnAction.Patch}"]`).hide();

    // $('#datatable thead tr').clone(true).appendTo('#datatable thead');
    // $('#datatable thead tr:eq(1) th').each(function (i) {
    //   var title = $(this).text();

    //   $(this).html(el);

    //   // $('input', this).on('keyup change', function () {
    //   //   if (table.column(i).search() !== this.value) {
    //   //     table
    //   //       .column(i)
    //   //       .search(this.value)
    //   //       .draw();
    //   //   }
    //   // });

    // });

    // var table = $('#example').DataTable({
    //   orderCellsTop: true,
    //   fixedHeader: true
    // });

    // // Setup - add a text input to each footer cell
    // $('#datatable tfoot th').each(function () {
    //   var title = $(this).text();
    //   $(this).html('<input type="text" placeholder="Search ' + title + '" />');
    // });

    // Apply the search
    // table.columns().every(function () {
    //   var that = this;

    //   $('input', this.footer()).on('keyup change clear', function () {
    //     if (that.search() !== this.value) {
    //       that
    //         .search(this.value)
    //         .draw();
    //     }
    //   });
    // });
  }

  ngOnDestroy(): void {
    this.subs.forEach((sub) => {
      sub.unsubscribe();
    });
  }
}

export enum ColumnAction {
  Filter = 1,
  Patch = 2,
}
