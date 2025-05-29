import { Factura } from './factura.interface';

export interface FacturaResponse {
  isSuccess: boolean;
  result: Factura;
  message: string;
}
