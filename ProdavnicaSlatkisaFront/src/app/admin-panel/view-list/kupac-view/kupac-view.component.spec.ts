import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KupacViewComponent } from './kupac-view.component';

describe('KupacViewComponent', () => {
  let component: KupacViewComponent;
  let fixture: ComponentFixture<KupacViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [KupacViewComponent]
    });
    fixture = TestBed.createComponent(KupacViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
