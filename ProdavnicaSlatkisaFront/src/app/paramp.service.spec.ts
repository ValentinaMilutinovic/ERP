import { TestBed } from '@angular/core/testing';

import { ParampService } from './paramp.service';

describe('ParampService', () => {
  let service: ParampService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ParampService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
