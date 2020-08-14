import { Component, OnInit } from '@angular/core';

import { Insurance } from '../../models/insurance';
import { InsuranceService } from '../../services/insurance.service';

import { ConfirmationService } from 'primeng/api';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-insurances',
  templateUrl: './insurances.component.html',
  styleUrls: ['./insurances.component.scss'],
  providers: [MessageService, ConfirmationService],
})
export class InsurancesComponent implements OnInit {
  insurances: Insurance[];
  insurance: any;

  selectedInsurances: Insurance[];

  submitted: boolean;
  insuranceDialog: boolean;

  constructor(
    private insuranceService: InsuranceService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnInit(): void {
    this.insuranceService.getInsurances().subscribe((insurances) => {
      this.insurances = insurances;
    });
  }

  openNew(): void {
    this.insurance = {};
    this.submitted = false;
    this.insuranceDialog = true;
  }

  deleteSelectedInsurances(): void {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete the selected insurances?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.insurances = this.insurances.filter(
          (val) => !this.selectedInsurances.includes(val)
        );
        this.selectedInsurances = null;
        this.messageService.add({
          severity: 'success',
          summary: 'Successful',
          detail: 'Insurances Deleted',
          life: 3000,
        });
      },
    });
  }

  editInsurance(insurance: Insurance): void {
    this.insurance = { ...insurance };
    this.insuranceDialog = true;
  }

  deleteInsurance(insurance: Insurance): void {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete ' + insurance.name + '?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.insurances = this.insurances.filter(
          (val) => val.id !== insurance.id
        );
        this.insurance = {};
        this.messageService.add({
          severity: 'success',
          summary: 'Successful',
          detail: 'Insurance Deleted',
          life: 3000,
        });
      },
    });
  }

  hideDialog(): void {
    this.insuranceDialog = false;
    this.submitted = false;
  }

  saveInsurance(): void {
    this.submitted = true;

    if (this.insurance.name.trim()) {
      if (this.insurance.id) {
        this.messageService.add({
          severity: 'success',
          summary: 'Successful',
          detail: 'Insurance Updated',
          life: 3000,
        });
      } else {
        this.insurance.image = 'insurance-placeholder.svg';
        this.insurances.push(this.insurance);
        this.messageService.add({
          severity: 'success',
          summary: 'Successful',
          detail: 'Insurance Created',
          life: 3000,
        });
      }

      this.insurances = [...this.insurances];
      this.insuranceDialog = false;
      this.insurance = {};
    }
  }
}
