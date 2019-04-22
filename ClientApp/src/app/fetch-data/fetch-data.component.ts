import { Component, OnInit ,ViewChild} from '@angular/core';
import { MatTableDataSource,MatPaginator,MatSort } from '@angular/material';
import { DataServiceService } from '../service/data-service.service';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html',
})
export class FetchDataComponent implements OnInit {

  location: Location;

  MyDataSource: any;
  displayedColumns = [
    'provider_Name',
    'federal_Provider_Number',
    'provider_Address',
    'provider_City',
    'provider_State',
    'provider_Zip_Code',
    'provider_Phone_Number'
  ];

  public pageSize = 50;
  public currentPage = 0;
  public filteValue = '';

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  
  constructor(public dataService: DataServiceService){}

  ngOnInit() {
    this.RenderDataTable();
  }

  RenderDataTable() {

    this.dataService.GetProviderData(this.filteValue, this.currentPage, this.pageSize)
      .subscribe(
      res => {
        this.MyDataSource = new MatTableDataSource();
        this.MyDataSource.data = res.items;
        this.MyDataSource.sort = this.sort;
        this.paginator.length = res.totalRowCount;
      },
      error => {
        console.log('There was an error while retrieving providers !!!' + error);
      });
  }

  SearchTable(filter: string)
  {
    this.filteValue = filter;
    if (filter.length < 3 && filter.length > 0)
      return;

    this.currentPage = 0;
    this.pageSize = 50;
    this.paginator.length = 0;
    this.RenderDataTable();
  }

  public handlePage(e: any) {
    this.currentPage = e.pageIndex;
    this.pageSize = e.pageSize;
    this.RenderDataTable();
  }
}
