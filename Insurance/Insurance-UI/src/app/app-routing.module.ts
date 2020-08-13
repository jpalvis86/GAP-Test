import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { CustomersComponent } from './components/customers/customers.component';
import { InsurancesComponent } from './components/insurances/insurances.component';

const routes: Routes = [
  { path: 'customers', component: CustomersComponent },
  { path: 'insurances', component: InsurancesComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
