import { Component } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { AngularFireAuth } from '@angular/fire/auth';
import { Router } from '@angular/router';

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
    public fireAuth: AngularFireAuth,
    private breakpointObserver: BreakpointObserver,
    private router: Router
  ) {}

  logOut(): void {
    this.fireAuth.signOut();
    this.router.navigate(['/login']);
  }
}
