import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProizvodjacUpdateComponent } from './proizvodjac-update.component';

describe('ProizvodjacUpdateComponent', () => {
  let component: ProizvodjacUpdateComponent;
  let fixture: ComponentFixture<ProizvodjacUpdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProizvodjacUpdateComponent]
    });
    fixture = TestBed.createComponent(ProizvodjacUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
