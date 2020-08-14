import { Insurance } from './insurance';

export interface CustomerInsurance {
  id: number;
  name: string;
  insurances: Insurance[];
}
