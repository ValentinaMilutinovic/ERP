import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TipproizvodaUpdateComponent } from './tipproizvoda-update.component';

describe('TipproizvodaUpdateComponent', () => {
  let component: TipproizvodaUpdateComponent;
  let fixture: ComponentFixture<TipproizvodaUpdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TipproizvodaUpdateComponent]
    });
    fixture = TestBed.createComponent(TipproizvodaUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
