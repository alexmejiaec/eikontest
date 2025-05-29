import { CrearActualizarFacturaDetalle } from './crearactualizardetfactura.interface';

export interface CrearActualizarFactura {
  numero: string;
  fecha: Date;
  cliente: string;
  detalle: CrearActualizarFacturaDetalle[];
}
