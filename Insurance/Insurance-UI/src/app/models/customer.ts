import { Insurance } from '../models/insurance';

export interface Customer {
  id: number;
  name: string;
  insurances: Insurance[];
}
