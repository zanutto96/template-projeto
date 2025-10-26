import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import Swal from 'sweetalert2';
import { ApiService } from 'app/common/services/api.service';
import { MatDialog } from '@angular/material/dialog';
import { DialogDefaultComponent } from 'app/util/dialog-default/dialog-default.component';
import { #Entity#RegisterComponent } from '../#EntityLowerCase#-register/#EntityLowerCase#-register.component';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: '#EntityLowerCase#-list',
  templateUrl: './#EntityLowerCase#-list.component.html',
  styleUrls: ['./#EntityLowerCase#-list.component.scss']
})
export class #Entity#ListComponent implements OnInit {
  
@Output() edit = new EventEmitter<any>();
  @ViewChild('dataTable#Entity#') table: any;
  @ViewChild('#Entity#Table', { read: MatSort }) sort: MatSort;
  
  public list: any = [];
  public dtOption: any = {};
  public dataTable: any;
  public loading: boolean = false;
  public vm: any = {};
  public filters: any = {};
  public debounceTime = null;
  public #EntityHtmlDisplayedColumnsMaterialProperties#
  public filterColumns = this.displayedColumns.map((el) => "filtro_" + el);
  public dataSource: any = null;

  constructor(private service: ApiService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.getData();
  }

  getData(filters: any = null) {
    var filters = {
      ...filters
    };
    this.loading = true;
    this.service.get('#Entity#', 'GetData', filters).subscribe((result) => {
      this.vm = result.data;
      this.list = result.data.dataList;
      this.dataSource = this.list;
      this.dataSource.sort = this.sort;
    });
  }

  onDelete(id: any) {
    const dialogRef = this.dialog.open(DialogDefaultComponent, { data: { title: "Deseja apagar o registro?", text: "Esta operação não poderá ser revertida!" }, width: '425px',  });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        let model = {
          #EntityRepositoryPrimaryKeyFront#: id,
          Name: ""
        };
        this.service.delete('#Entity#', model).subscribe((a) => {
          this.getData();
        });
      }
    });
  }

  onEdit(id: any) {
    const dialogRef = this.dialog.open(#Entity#RegisterComponent, { data: { id: id } });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getData();
      }
    });
  }

  onPageChanged(pageConfig: any) {

    let modelFilter = {
      PageIndex: pageConfig.PageIndex
    };
    this.service.get('#Entity#', 'GetData', modelFilter).subscribe((result) => {
      this.list = result.data.dataList;
      this.vm = result.data;
      this.dataSource = this.list;
    });
  }

  onSort(sortEvent: any) {
    this.getData({
      ...this.filters,
      "OrderBy": sortEvent.active,
      "OrderByType": sortEvent.direction == "desc" ? "OrderByDescending" : "OrderBy"
    })
  }
  onFilter(filter: any) {
    if (this.debounceTime) {
      clearTimeout(this.debounceTime)
    }
    this.debounceTime = setTimeout(() => {
      this.getData({
        ...filter,
        "OrderBy": this.sort.active || '',
        "OrderByType": this.sort.direction == "desc" ? "OrderByDescending" : "OrderBy"
      })
    }, 500);
  }
}
