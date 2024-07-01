import { TestBed } from '@angular/core/testing';

import { CartsrService } from './cartsr.service';

describe('CartsrService', () => {
  let service: CartsrService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CartsrService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
