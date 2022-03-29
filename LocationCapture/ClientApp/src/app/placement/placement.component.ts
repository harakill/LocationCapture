import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Placement, PlacementClient } from '../web-api-client';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { AddPlacementComponent } from '../add-placement/add-placement.component';

@Component({
  selector: 'app-placement',
  templateUrl: './placement.component.html',
  styleUrls: ['./placement.component.css']
})
export class PlacementComponent implements OnInit {
  public placements: Placement[];

  public placement: string[] = ['id', 'vehicle', 'latitude', 'longitude', 'timeStamp'];

  id: string;
  vehicle: string;
  latitude: number; 
  longitude: number;
  timeStamp: Date;
  data: Placement;

  searchKey: string;
  
  listData: MatTableDataSource<any>;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  
  
  constructor(public plc: PlacementClient, private dialog: MatDialog) { 
    //this.onLoad();
    this.listData;
    console.log(this.listData);
  }

  ngOnInit(): void {
    this.plc.getPlacements().subscribe(result => {
      let array = result.map(item => 
        {
          let id = item.id;
          let vehicle = item.vehicle;
          let latitude = item.latitude;
          let longitude = item.longitude;
          let timeStamp = item.timeStamp;
          return {
            $key: item.id,
            id: item.id,
            vehicle,
            latitude,
            longitude,
            timeStamp
          };
        });
        
    this.listData = new MatTableDataSource(array);
    this.listData.sort = this.sort;
    this.listData.paginator = this.paginator;
    this.listData.filterPredicate = (data, filter) => {
      return this.placement.some(ele => {
        return ele != 'actions' && data[ele].toLowerCase().indexOf(filter) != -1;
      });
    };
  });
  }

  onLoad(): void {
    this.plc.getPlacements().subscribe(result => {
      this.placements = result;
    }, error => console.error(error));
  }

  // onSave(data): void {
  //   let plct = Placement.fromJS({ 
  //     vehicle: this.vehicle, 
  //     timeStamp: this.timeStamp, 
  //     latitude: this.latitude, 
  //     longitude: this.longitude 
  //   });
    
  //   this.plc.createPlacement(plct).subscribe(
  //     result => this.onLoad(), 
  //     error => console.error(error));
  // }

  // onClear(): void {
  //   this.vehicle = null;
  //   this.timeStamp = null; 
  //   this.latitude = null;
  //   this.longitude = null;
  // }

  onSearchClear() {
    this.searchKey = "";
    this.applyFilter();
  }

  applyFilter() {
    this.listData.filter = this.searchKey.trim().toLowerCase();
  }

  onCreate() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = "50%";
    this.dialog.open(AddPlacementComponent,dialogConfig);
  }

  onEdit(row){
    // this.service.populateForm(row);
    // const dialogConfig = new MatDialogConfig();
    // dialogConfig.disableClose = true;
    // dialogConfig.autoFocus = true;
    // dialogConfig.width = "60%";
    // this.dialog.open(EmployeeComponent,dialogConfig);
  }

  onDelete($key){
    if(confirm('Are you sure to delete this record ?')){
    // this.service.deleteEmployee($key);
    // this.notificationService.warn('! Deleted successfully');
    }
  }

}
