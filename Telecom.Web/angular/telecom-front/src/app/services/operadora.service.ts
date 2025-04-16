import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { OperadoraServicoRequest } from '../entities/Request/OperadoraServicoRequest';
import { OperadoraServicoResponse } from '../entities/Response/OperadoraServicoResponse';
import { environment } from '../../environments/environment'; 
import { OperadoraRequest } from '../entities/Request/OperadorasRequest';


@Injectable({
  providedIn: 'root'
})
export class OperadoraService {
  private apiUrl = environment.apiUrl

  constructor(private http: HttpClient) {}

  listarOperadoras(): Observable<OperadoraServicoResponse[]> {
    return this.http.get<OperadoraServicoResponse[]>(`${this.apiUrl}/OperadoraServico/listar`);
  }

  criarOperadora(operadora: OperadoraServicoRequest): Observable<OperadoraServicoResponse> {
    return this.http.post<OperadoraServicoResponse>(`${this.apiUrl}/OperadoraServico/criar`, operadora);
  }

  editarOperadora(operadora: OperadoraServicoRequest): Observable<any> {
    return this.http.put(`${this.apiUrl}/OperadoraServico/editar`, operadora);
  }

  deletarOperadora(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/OperadoraServico/deletar/${id}`);
  }

  listarNomesOperadoras(): Observable<OperadoraRequest[]> {
    return this.http.get<OperadoraRequest[]>(`${this.apiUrl}/OperadoraServico/listar-nome-operadoras`);
  }

}
