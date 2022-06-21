/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CartaService } from './carta.service';

describe('Service: Carta', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CartaService]
    });
  });

  it('should ...', inject([CartaService], (service: CartaService) => {
    expect(service).toBeTruthy();
  }));
});
