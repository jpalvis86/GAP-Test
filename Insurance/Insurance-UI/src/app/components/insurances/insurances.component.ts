import { Component, OnInit } from '@angular/core';

import { Insurance } from '../../models/insurance';
import { InsuranceService } from '../../services/insurance.service';

import { ConfirmationService } from 'primeng/api';
import { MessageService, SelectItem } from 'primeng/api';

@Component({
  selector: 'app-insurances',
  templateUrl: './insurances.component.html',
  styleUrls: ['./insurances.component.scss'],
  providers: [MessageService, ConfirmationService],
})
export class InsurancesComponent implements OnInit {
  insurances: Insurance[];
  insurance: any;

  selectedRisk: SelectItem;
  risks: SelectItem[] = [
    { label: 'Low', value: 'Low' },
    { label: 'Medium', value: 'Medium' },
    { label: 'MediumHigh', value: 'MediumHigh' },
    { label: 'High', value: 'High' },
  ];

  selectedCoverageTypes: SelectItem[];
  coverageTypes: SelectItem[] = [
    { label: 'Earthquake', value: 'Earthquake' },
    { label: 'Fire', value: 'Fire' },
    { label: 'Robbery', value: 'Robbery' },
    { label: 'Damage', value: 'Damage' },
    { label: 'Lost', value: 'Lost' },
  ];

  selectedInsurances: Insurance[];

  submitted: boolean;
  insuranceDialog: boolean;

  constructor(
    private insuranceService: InsuranceService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnInit(): void {
    this.refreshInsurances();
  }

  private refreshInsurances(): void {
    this.insuranceService.getInsurances().subscribe((insurances) => {
      this.insurances = insurances;
    });
  }

  openNew(): void {
    this.insurance = {};
    this.submitted = false;
    this.insuranceDialog = true;
  }

  editInsurance(insurance: Insurance): void {
    this.insurance = { ...insurance };
    this.selectedRisk = {
      label: this.insurance.risk,
      value: this.insurance.risk,
    };

    this.selectedCoverageTypes = this.insurance.coverageTypes.map((c) => ({
      label: c,
      value: c,
    }));

    console.log(this.insurance);
    this.insuranceDialog = true;
  }

  deleteInsurance(insurance: Insurance): void {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete ' + insurance.name + '?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        console.log(insurance);
        this.insuranceService.deleteInsurance(insurance.id).subscribe(
          () => {
            this.insurance = {};
            this.messageService.add({
              severity: 'success',
              summary: 'Successful',
              detail: 'Insurance Deleted',
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
        this.refreshInsurances();
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
        this.formatValuesBeforeSending();

        this.insuranceService.updateInsurance(this.insurance).subscribe(() => {
          this.refreshInsurances();
        });
        this.messageService.add({
          severity: 'success',
          summary: 'Successful',
          detail: 'Insurance Updated',
          life: 3000,
        });
      } else {
        this.formatValuesBeforeSending();

        this.insuranceService.addInsurance(this.insurance).subscribe(() => {
          this.refreshInsurances();
        });
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

  private formatValuesBeforeSending(): void {
    this.insurance.risk = this.selectedRisk.value;
    this.insurance.coverageTypes = this.selectedCoverageTypes.map(
      (c) => c.value
    );

    console.log(this.insurance.coverageTypes);
    this.insurance.coverageRate /= 100;
  }
}
