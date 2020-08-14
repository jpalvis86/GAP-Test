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
export class SupervisorGuard implements CanActivate {
  constructor(
    private fireAuth: AngularFireAuth,
    private notification: NotificationService
  ) {}

  async canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Promise<boolean> {
    const user = await this.fireAuth.currentUser;

    /* In a real-world application,
     * this validation should be done through an HTTP request
     * to retrieve the proper profile for each user
     */
    const isSupervisor = user.email === 'jpan_2009@hotmail.com';

    if (!isSupervisor) {
      this.notification.showProfileNotification();
    }

    return isSupervisor;
  }
}
