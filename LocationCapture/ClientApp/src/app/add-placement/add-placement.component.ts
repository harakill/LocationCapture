import { Component, OnInit } from '@angular/core';
import { Placement, PlacementClient } from '../web-api-client';

@Component({
  selector: 'app-add-placement',
  templateUrl: './add-placement.component.html',
  styleUrls: ['./add-placement.component.css']
})
export class AddPlacementComponent implements OnInit {

  vehicle: string;
  latitude: number; 
  longitude: number;
  timeStamp: Date;

  constructor(private plc: PlacementClient) { }

  ngOnInit(): void {
  }

  onSave(data): void {
    let plct = Placement.fromJS({ 
      vehicle: this.vehicle, 
      timeStamp: Date.now(),
      latitude: this.latitude, 
      longitude: this.longitude 
    });
    
    this.plc.createPlacement(plct).subscribe(
      result => //this.onLoad(), 
      error => console.error(error));
  }

  onClear(): void {
    this.vehicle = null;
    this.timeStamp = null; 
    this.latitude = null;
    this.longitude = null;
  }
}
