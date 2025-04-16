import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  private baseUrl = `${environment.apiUrl}/Dashboard`;
  
  constructor(private http: HttpClient) {}

  // Indicadores principais: total de faturas + valor total
  getIndicadores(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/indicadores`);
  }

  // Distribuição por status: pagas, pendentes, atrasadas
  getStatus(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/status`);
  }

  // Faturas mensais: emitidas vs pagas por mês
  getMensal(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/mensal`);
  }
}

