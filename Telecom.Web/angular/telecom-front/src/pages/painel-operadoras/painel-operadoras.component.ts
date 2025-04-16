import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { OperadoraServicoRequest } from '../../app/entities/Request/OperadoraServicoRequest';
import { OperadoraServicoResponse } from '../../app/entities/Response/OperadoraServicoResponse';
import { OperadoraService } from '../../app/services/operadora.service';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';

@Component({
  standalone: true,
  selector: 'app-painel-operadoras',
  imports: [
    CommonModule,
    MatTableModule,
    FormsModule,
    MatFormFieldModule,
    RouterModule,
    MatInputModule,
    MatSelectModule,

  ],
  templateUrl: './painel-operadoras.component.html',
  styleUrls: ['./painel-operadoras.component.css']
})
export class PainelOperadorasComponent implements OnInit {

  operadoras: OperadoraServicoResponse[] = [];
  selectedOperadora: OperadoraServicoRequest | null = null;
  isFormVisible = false;
  displayedColumns: string[] = ['nomeOperadora', 'tipoServico', 'contatoSuporte', 'actions'];

  dataSource = new MatTableDataSource<OperadoraServicoResponse>();

  // Dados do formulário
  nomeOperadora: string = '';
  tipoServico: string = '';
  contatoSuporte: string = '';

  constructor(
    private operadoraService: OperadoraService

  
  ) {}

  ngOnInit() {
    this.carregarOperadoras();
  }

  carregarOperadoras() {
    this.operadoraService.listarOperadoras().subscribe(
      (data: OperadoraServicoResponse[]) => {
        console.log('Operadoras carregadas:', data); 
        this.operadoras = data;
        this.dataSource.data = data; 
      },
      (error) => {
        console.error('Erro ao carregar operadoras', error);
      }
    );
  }
  

  abrirFormCriar() {
    this.selectedOperadora = null;
    this.isFormVisible = true;
  }

  editarOperadora(operadora: OperadoraServicoResponse) {
    this.selectedOperadora = {
      id: operadora.id,
      nomeOperadora: operadora.nomeOperadora,
      tipoServico: operadora.tipoServico,
      contatoSuporte: operadora.contatoSuporte
    };
    
    this.nomeOperadora = operadora.nomeOperadora;
    this.tipoServico = operadora.tipoServico;
    this.contatoSuporte = operadora.contatoSuporte;
    this.isFormVisible = true;
  }

  onSubmit() {
    const request: OperadoraServicoRequest = {
      id: this.selectedOperadora?.id,
      nomeOperadora: this.nomeOperadora,
      tipoServico: this.tipoServico,
      contatoSuporte: this.contatoSuporte
    };
  
    // Verifica se a operadora tem um id (para edição)
    if (this.selectedOperadora && this.selectedOperadora.id) {
      this.operadoraService.editarOperadora(request).subscribe(() => {
        this.carregarOperadoras();
        this.fecharForm();
      });
    } else {
      // Se não houver ID, então cria uma nova operadora
      this.operadoraService.criarOperadora(request).subscribe(() => {
        this.carregarOperadoras();
        this.fecharForm();
      });
    }
  }
  

  deletarOperadora(id: number) {
    this.operadoraService.deletarOperadora(id).subscribe(() => {
      this.carregarOperadoras();
    });
  }

  fecharForm() {
    this.isFormVisible = false;
    this.selectedOperadora = null;
    this.nomeOperadora = '';
    this.tipoServico = '';
    this.contatoSuporte = '';
  }
}
