import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { ContratoService } from '../../app/services/contrato.service';
import { ContratoRequest } from '../../app/entities/Request/ContratoRequest';
import { ContratoResponse } from '../../app/entities/Response/ContratoResponse';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { OperadoraRequest } from '../../app/entities/Request/OperadorasRequest';
import { OperadoraService } from '../../app/services/operadora.service';  // Importar o serviço da operadora

@Component({
  selector: 'app-painel-contratos',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatIconModule,
    MatListModule,
    MatButtonModule,
    MatDividerModule,
    MatTableModule,
    RouterModule,
    ReactiveFormsModule,
  ],
  templateUrl: './painel-contratos.component.html',
  styleUrls: ['./painel-contratos.component.css']
})
export class PainelContratosComponent implements OnInit {



  formulario!: FormGroup;
  dataSource = new MatTableDataSource<ContratoResponse>();
  contratosResponse: ContratoResponse[] = [];
  editando: boolean = false;
  contratoEditandoId: number | null = null;
  isFormVisible: boolean = false;
  
  operadoraRequest: OperadoraRequest[] = []; 

  displayedColumns: string[] = ['nomeFilial', 'nomeOperadora', 'planoContratado', 'valorMensal', 'dataInicio', 'dataVencimento', 'status', 'acoes'];

  constructor(
    private fb: FormBuilder,
    private contratoService: ContratoService,
    private operadoraService: OperadoraService  // Injetar o serviço de operadora
  ) {}

  ngOnInit(): void {
    this.inicializarFormulario();
    this.carregarContratos();
    this.carregarOperadoras();  // Carregar as operadoras
  }

  inicializarFormulario(): void {
    this.formulario = this.fb.group({
      nomeFilial: ['', Validators.required],
      operadoraId: [null, Validators.required],
      planoContratado: ['', Validators.required],
      valorMensal: [0, [Validators.required, Validators.min(0)]]
    });
  }

  carregarContratos(): void {
    this.contratoService.listar().subscribe({
      next: (dados: ContratoResponse[]) => {
        this.contratosResponse = dados;
        this.dataSource.data = dados;
      },
      error: (err) => console.error('Erro ao carregar contratos:', err)
    });
  }

  carregarOperadoras(): void {
    this.operadoraService.listarNomesOperadoras().subscribe({
      next: (operadoras: OperadoraRequest[]) => {
        this.operadoraRequest = operadoras;
      },
      error: (err) => console.error('Erro ao carregar operadoras:', err)
    });
  }
  
  abrirFormCriar(): void {
    this.isFormVisible = true;
    this.editando = false;
    this.formulario.reset();
  }

  editarContrato(contrato: ContratoResponse): void {
    this.isFormVisible = true;
    this.editando = true;
    this.contratoEditandoId = contrato.id || null;
    this.formulario.patchValue({
      nomeFilial: contrato.nomeFilial,
      operadoraId: contrato.operadoraId,
      planoContratado: contrato.planoContratado,
      valorMensal: contrato.valorMensal
    });
  }

  onSubmit(): void {
    if (this.formulario.invalid) return;
  
    const contrato: ContratoResponse = {
      id: 0, // O ID será gerado no caso de criação
      ...this.formulario.value
    };
  
    // Verifica se está editando ou criando
    if (this.editando && this.contratoEditandoId !== null) {
      contrato.id = this.contratoEditandoId;
  
      // Chama o serviço para editar
      this.contratoService.editar(contrato).subscribe({
        next: () => {
          this.resetarFormulario();
          this.carregarContratos(); // Atualiza a lista de contratos
        },
        error: (err) => console.error('Erro ao editar contrato:', err)
      });
    } else {
      // Chama o serviço para criar um novo contrato
      this.contratoService.criar(contrato).subscribe({
        next: () => {
          this.resetarFormulario();
          this.carregarContratos(); // Atualiza a lista de contratos
        },
        error: (err) => console.error('Erro ao criar contrato:', err)
      });
    }
  }

  resetarFormulario(): void {
    this.editando = false;
    this.contratoEditandoId = null;
    this.formulario.reset(); // Reseta o formulário
  }
  
  fecharFormulario(): void {
    this.isFormVisible = false;
    this.formulario.reset();
  }

  deletarContrato(contrato: ContratoResponse): void {
    if (confirm('Tem certeza que deseja deletar este contrato?')) {
      this.contratoService.deletar(contrato.id).subscribe({
        next: () => {
          this.carregarContratos();
        },
        error: (err) => console.error('Erro ao deletar contrato:', err)
      });
    }
  }


  getNomeOperadora(operadoraId: number): string {
    const operadora = this.operadoraRequest?.find(o => o.id === operadoraId);
    return operadora ? operadora.nomeOperadora : 'Operadora não encontrada';
  }
  
  toggleStatus(contrato: any): void {
    const novoStatus = !contrato.status;
  
    this.contratoService.atualizarStatus(contrato.id).subscribe({
      next: () => {
        contrato.status = novoStatus;
      },
      error: (erro) => {
        console.error('Erro ao atualizar status do contrato:', erro);
      }
    });
  }
  
  
  


}