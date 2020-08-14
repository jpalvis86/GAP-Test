import { Component, OnInit } from '@angular/core';

import { Insurance } from '../../models/insurance';
import { InsuranceService } from '../../services/insurance.service';

import { ConfirmationService, MessageService, SelectItem } from 'primeng/api';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-insurances',
  templateUrl: './insurances.component.html',
  styleUrls: ['./insurances.component.scss'],
  providers: [MessageService, ConfirmationService, DatePipe],
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

  selectedDate = this.datepipe.transform(new Date(), 'yyyy-MM-dd');
  selectedInsurances: Insurance[];

  submitted: boolean;
  insuranceDialog: boolean;

  constructor(
    private insuranceService: InsuranceService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    public datepipe: DatePipe
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

    this.selectedDate = this.datepipe.transform(
      this.insurance.startDate,
      'yyyy-MM-dd'
    );
    this.insurance.coverageRate *= 100;
    this.selectedRisk = {
      label: this.insurance.risk,
      value: this.insurance.risk,
    };

    this.selectedCoverageTypes = this.insurance.coverageTypes.map((c) => ({
      label: c,
      value: c,
    }));

    this.insuranceDialog = true;
  }

  deleteInsurance(insurance: Insurance): void {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete ' + insurance.name + '?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.insuranceService.deleteInsurance(insurance.id).subscribe(
          () => {
            this.refreshInsurances();
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

        this.insuranceService.updateInsurance(this.insurance).subscribe(
          () => {
            this.refreshInsurances();
            this.messageService.add({
              severity: 'success',
              summary: 'Successful',
              detail: 'Insurance Updated',
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
      } else {
        this.formatValuesBeforeSending();

        this.insuranceService.addInsurance(this.insurance).subscribe(
          () => {
            this.refreshInsurances();
            this.messageService.add({
              severity: 'success',
              summary: 'Successful',
              detail: 'Insurance Created',
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

    this.insurance.startDate = new Date(this.selectedDate);
    this.insurance.coverageRate /= 100;
  }
}
