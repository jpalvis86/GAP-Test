import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NavshellComponent } from 'src/app/components/navshell/navshell.component';

const routes: Routes = [
  { path: '', component: NavshellComponent },
  {
    path: 'login',
    loadChildren: () => import('./user.module').then((m) => m.UserModule),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UserRoutingModule {}
