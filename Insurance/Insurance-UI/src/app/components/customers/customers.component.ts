import { Component, OnInit } from '@angular/core';

import { Insurance } from '../../models/insurance';
import { InsuranceService } from '../../services/insurance.service';

import { Customer } from '../../models/customer';
import { CustomerService } from '../../services/customer.service';

import { ConfirmationService, MessageService, SelectItem } from 'primeng/api';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss'],
  providers: [MessageService, ConfirmationService],
})
export class CustomersComponent implements OnInit {
  customer: any;
  customers: Customer[];

  insurances: SelectItem[];
  selectedInsurances: SelectItem[];

  submitted: boolean;
  customerDialog: boolean;

  constructor(
    private customerService: CustomerService,
    private insuranceService: InsuranceService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnInit(): void {
    this.refreshInsurances();
    this.refreshCustomers();
  }

  private refreshInsurances(): void {
    this.insuranceService.getInsurances().subscribe((insurances) => {
      this.insurances = insurances.map((i) => ({
        label: i.name,
        value: i.id,
      }));
    });
  }

  private refreshCustomers(): void {
    this.customerService.getCustomers().subscribe((customers) => {
      this.customers = customers;
      console.log(this.customers);
      this.selectedInsurances = [];
    });
  }

  editCustomer(customer: Customer): void {
    this.customerService
      .getCustomer(customer.id)
      .subscribe((customerResponse) => {
        this.customer = customerResponse;
        console.log(this.customer);
        this.selectedInsurances = this.customer.insurances.map((i) => ({
          label: i.name,
          value: i.id,
        }));
        console.log(this.selectedInsurances);
      });
    this.customerDialog = true;
  }

  hideDialog(): void {
    this.customerDialog = false;
    this.submitted = false;
  }

  saveCustomer(): void {
    this.submitted = true;

    this.customer.insurances = this.selectedInsurances.map((c) => c.value);

    if (this.customer.name.trim()) {
      if (this.customer.id) {
        this.customerService.updateCustomers(this.customer).subscribe(
          () => {
            this.refreshCustomers();
            this.refreshInsurances();
            this.messageService.add({
              severity: 'success',
              summary: 'Successful',
              detail: 'Customer Updated',
              life: 3000,
            });
          },
          (error) => {
            console.log(error);
            this.messageService.add({
              severity: 'error',
              summary: 'Error',
              detail: error.error,
              life: 5000,
            });
          }
        );
      }

      this.customers = [...this.customers];
      this.customerDialog = false;
      this.customer = {};
    }
  }
}
