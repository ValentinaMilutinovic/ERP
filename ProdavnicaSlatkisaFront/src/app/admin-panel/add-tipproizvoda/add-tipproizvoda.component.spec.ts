import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTipproizvodaComponent } from './add-tipproizvoda.component';

describe('AddTipproizvodaComponent', () => {
  let component: AddTipproizvodaComponent;
  let fixture: ComponentFixture<AddTipproizvodaComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddTipproizvodaComponent]
    });
    fixture = TestBed.createComponent(AddTipproizvodaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
