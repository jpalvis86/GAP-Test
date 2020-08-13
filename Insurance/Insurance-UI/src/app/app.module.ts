import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { InsurancesComponent } from './components/insurances/insurances.component';
import { CustomersComponent } from './components/customers/customers.component';
import { HomeComponent } from './components/home/home.component';

import { UserModule } from './modules/user/user.module';

@NgModule({
  declarations: [
    AppComponent,
    InsurancesComponent,
    CustomersComponent,
    HomeComponent,
  ],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule, UserModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
