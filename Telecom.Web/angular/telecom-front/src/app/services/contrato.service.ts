import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ContratoRequest } from '../entities/Request/ContratoRequest';
import { ContratoResponse } from '../entities/Response/ContratoResponse';

@Injectable({
  providedIn: 'root'
})
export class ContratoService {



  // URL base já com o controller "Contrato"
  private baseUrl = `${environment.apiUrl}/Contrato`;

  constructor(private http: HttpClient) {}

  listar(): Observable<ContratoResponse[]> {
    return this.http.get<ContratoResponse[]>(`${this.baseUrl}/listar`);
  }

  buscarPorId(id: number): Observable<ContratoResponse> {
    return this.http.get<ContratoResponse>(`${this.baseUrl}/buscar/${id}`);
  }

  criar(contrato: ContratoResponse): Observable<ContratoResponse> {
    return this.http.post<any>(`${this.baseUrl}/criar`, contrato);
  }

  editar(contrato: ContratoResponse): Observable<ContratoResponse> {
    return this.http.put<any>(`${this.baseUrl}/editar`, contrato);
  }

  deletar(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/deletar/${id}`);
  }


  atualizarStatus(id: number): Observable<any> {
    return this.http.put(`${this.baseUrl}/alternar-status/${id}`, {}); // <-- objeto vazio
  }


  
}
