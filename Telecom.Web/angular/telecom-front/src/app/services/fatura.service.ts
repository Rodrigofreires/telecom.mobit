import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FaturaResponse } from '../entities/Response/FaturaResponse';
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class FaturaService {

   private baseUrl = `${environment.apiUrl}/Fatura`;

  constructor(private http: HttpClient) {}

  getFaturas(): Observable<FaturaResponse[]> {
    return this.http.get<FaturaResponse[]>(`${this.baseUrl}/listar`);
  }

  criarFatura(fatura: FaturaResponse): Observable<FaturaResponse[]> {
    return this.http.post<FaturaResponse[]>(`${this.baseUrl}/criar`, fatura);
  }

  editarFatura(fatura: FaturaResponse): Observable<FaturaResponse> {
    return this.http.put<FaturaResponse>(`${this.baseUrl}/editar`, fatura);
  }

  deletarFatura(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/deletar/${id}`);
  }

  getTotalGastoMensal(operadoraId: number, ano: number, mes: number): Observable<number> {
    return this.http.get<number>(
      `${this.baseUrl}/total-gastos-operadora?operadoraId=${operadoraId}&ano=${ano}&mes=${mes}`
    );
  }
  

}
