import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetSpeedComponent } from './get-speed.component';

describe('GetSpeedComponent', () => {
  let component: GetSpeedComponent;
  let fixture: ComponentFixture<GetSpeedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GetSpeedComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GetSpeedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
