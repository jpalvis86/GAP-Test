import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
} from '@angular/router';
import { AngularFireAuth } from '@angular/fire/auth';
import { NotificationService } from '../services/notification.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private fireAuth: AngularFireAuth,
    private notification: NotificationService
  ) {}

  async canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Promise<boolean> {
    const user = await this.fireAuth.currentUser;
    const isUserLoggedIn = !!user;

    if (!isUserLoggedIn) {
      this.notification.showLoginNotification();
    }

    return isUserLoggedIn;
  }
}
