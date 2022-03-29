import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { PlacementComponent } from './placement/placement.component';

import { MaterialModule } from './material/material.module';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AddPlacementComponent } from './add-placement/add-placement.component';
import { GetSpeedComponent } from './get-speed/get-speed.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    PlacementComponent,
    AddPlacementComponent,
    GetSpeedComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    MaterialModule,
    BrowserAnimationsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'placement', component: PlacementComponent },
      { path: 'get-speed', component: GetSpeedComponent }
    ])
  ],
  providers: [AddPlacementComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
