import { TestBed } from '@angular/core/testing';

import { InsuranceserviceService } from './insuranceservice.service';

describe('InsuranceserviceService', () => {
  let service: InsuranceserviceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InsuranceserviceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
