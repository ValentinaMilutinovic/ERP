import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FilterssComponent } from './filterss.component';

describe('FilterssComponent', () => {
  let component: FilterssComponent;
  let fixture: ComponentFixture<FilterssComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FilterssComponent]
    });
    fixture = TestBed.createComponent(FilterssComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
