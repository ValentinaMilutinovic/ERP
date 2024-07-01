import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewProizvodComponent } from './view-proizvod.component';

describe('ViewProizvodComponent', () => {
  let component: ViewProizvodComponent;
  let fixture: ComponentFixture<ViewProizvodComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ViewProizvodComponent]
    });
    fixture = TestBed.createComponent(ViewProizvodComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
