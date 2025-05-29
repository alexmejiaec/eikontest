import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FacturaResponse } from '../interfaces/factura-response.interface';
import { Observable } from 'rxjs';
import { CrearActualizarFactura } from '../interfaces/crearactualizarfactura.interface';

@Injectable({
  providedIn: 'root',
})
export class FacturasService {
  private baseUrl = 'http://localhost:7002/';
  private http = inject(HttpClient);
  constructor() {}

  getFacturas(): Observable<FacturaResponse> {
    return this.http.get<FacturaResponse>(`${this.baseUrl}api/Factura/`);
  }

  getFactura(id: string): Observable<FacturaResponse> {
    return this.http.get<FacturaResponse>(`${this.baseUrl}api/Factura/${id}`);
  }

  postFactura(
    nuevoEstudiante: CrearActualizarFactura
  ): Observable<FacturaResponse> {
    return this.http.post<FacturaResponse>(
      `${this.baseUrl}api/Factura/`,
      nuevoEstudiante
    );
  }
}
