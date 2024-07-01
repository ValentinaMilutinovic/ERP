
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class ParampService { private tipProizvodaPrampSubject: Subject<string[]> = new Subject<string[]>();
  private typeFilterSubject: Subject<string[]> = new Subject<string[]>();

  tipProizvodaPramp$ = this.tipProizvodaPrampSubject.asObservable();


  setTypeFilter(types: string[]): void {
    this.tipProizvodaPrampSubject.next(types);
  }
}
