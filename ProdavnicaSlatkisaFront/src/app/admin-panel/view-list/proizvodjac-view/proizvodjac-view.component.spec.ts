import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProizvodjacViewComponent } from './proizvodjac-view.component';

describe('ProizvodjacViewComponent', () => {
  let component: ProizvodjacViewComponent;
  let fixture: ComponentFixture<ProizvodjacViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProizvodjacViewComponent]
    });
    fixture = TestBed.createComponent(ProizvodjacViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
