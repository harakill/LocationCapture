import { Component, OnInit } from '@angular/core';
import { PlacementClient } from '../web-api-client';

@Component({
  selector: 'app-get-speed',
  templateUrl: './get-speed.component.html',
  styleUrls: ['./get-speed.component.css']
})
export class GetSpeedComponent implements OnInit {

  id1: number;
  id2: number;
  speedOfMovement: number;

  constructor(private plc: PlacementClient) { }

  ngOnInit(): void {
  }

  GetSpeedOfMovement(data): number {
    this.plc.getSpeedOfMovement(this.id1, this.id2).subscribe(result => {
        this.speedOfMovement = result
    }, error => console.error(error));
    return this.speedOfMovement
  }

  onClear(): void {
    this.id1 = null;
    this.id2 = null;
    this.speedOfMovement = null;
  }
}
