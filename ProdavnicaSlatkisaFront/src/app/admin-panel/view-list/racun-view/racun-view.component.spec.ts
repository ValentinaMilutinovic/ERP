import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RacunViewComponent } from './racun-view.component';

describe('RacunViewComponent', () => {
  let component: RacunViewComponent;
  let fixture: ComponentFixture<RacunViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RacunViewComponent]
    });
    fixture = TestBed.createComponent(RacunViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
