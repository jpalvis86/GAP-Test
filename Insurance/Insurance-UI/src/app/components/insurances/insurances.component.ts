import { Component, OnInit } from '@angular/core';

import { Insurance } from '../../models/insurance';
import { InsuranceService } from '../../services/insurance.service';

@Component({
  selector: 'app-insurances',
  templateUrl: './insurances.component.html',
  styleUrls: ['./insurances.component.scss'],
})
export class InsurancesComponent implements OnInit {
  constructor(private insuranceService: InsuranceService) {}

  insurances: Insurance[];
  ngOnInit(): void {
    this.insuranceService.getInsurances().subscribe((insurances) => {
      console.log(insurances);
      this.insurances = insurances;
    });
  }
}
