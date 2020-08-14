import { Component } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { AngularFireAuth } from '@angular/fire/auth';

@Component({
  selector: 'app-navshell',
  templateUrl: './navshell.component.html',
  styleUrls: ['./navshell.component.scss'],
})
export class NavshellComponent {
  isHandset$: Observable<boolean> = this.breakpointObserver
    .observe([Breakpoints.Handset])
    .pipe(
      map((result) => result.matches),
      shareReplay()
    );

  constructor(
    private breakpointObserver: BreakpointObserver,
    public fireAuth: AngularFireAuth
  ) {}
}
