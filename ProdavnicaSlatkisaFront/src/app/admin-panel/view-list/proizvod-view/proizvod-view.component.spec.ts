import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProizvodViewComponent } from './proizvod-view.component';

describe('ProizvodViewComponent', () => {
  let component: ProizvodViewComponent;
  let fixture: ComponentFixture<ProizvodViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProizvodViewComponent]
    });
    fixture = TestBed.createComponent(ProizvodViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
