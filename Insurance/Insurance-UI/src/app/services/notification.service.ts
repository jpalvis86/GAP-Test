import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  constructor(private router: Router, private snackBar: MatSnackBar) {}

  showLoginNotification(): any {
    this.snackBar.open(
      'You must be logged in to view this content',
      'Take me to Login',
      {
        duration: 5000,
      }
    );

    return this.snackBar._openedSnackBarRef
      .onAction()
      .pipe(tap((_) => this.router.navigate(['/login'])))
      .subscribe();
  }
}
