import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TipproizvodaViewComponent } from './tipproizvoda-view.component';

describe('TipproizvodaViewComponent', () => {
  let component: TipproizvodaViewComponent;
  let fixture: ComponentFixture<TipproizvodaViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TipproizvodaViewComponent]
    });
    fixture = TestBed.createComponent(TipproizvodaViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
