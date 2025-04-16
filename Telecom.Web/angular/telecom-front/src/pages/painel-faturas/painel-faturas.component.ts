import { Component, OnInit } from '@angular/core';
import { FaturaResponse } from '../../app/entities/Response/FaturaResponse';
import { FaturaService } from '../../app/services/fatura.service';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
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
import { MatTableModule } from '@angular/material/table';
import { OperadoraRequest } from '../../app/entities/Request/OperadorasRequest';
import { ContratoRequest } from '../../app/entities/Request/ContratoRequest';
import { ContratoService } from '../../app/services/contrato.service';
import { ContratoResponse } from '../../app/entities/Response/ContratoResponse';
import { OperadoraService } from '../../app/services/operadora.service';

@Component({
  standalone: true,
  selector: 'app-painel-faturas',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
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
    MatDialogModule,
    FormsModule,
  ],
  templateUrl: './painel-faturas.component.html',
  styleUrls: ['./painel-faturas.component.css']
})
export class PainelFaturasComponent implements OnInit {

  faturas: FaturaResponse[] = [];
  novaFatura: FaturaResponse = {
    id: 0,
    contratoId: 0,
    valorCobrado: 0,
    status: ''
  };

  contratosResponse: ContratoResponse[] = [];
  operadorasRequest: OperadoraRequest[] = [];
  modalAberto = false;
  displayedColumns: string[] = ['id', 'contratoId', 'nomeFilial', 'nomeOperadora', 'valorCobrado', 'status', 'acoes'];
  isFormVisible: boolean = false;
  faturaForm!: FormGroup;
  editando: boolean = false;
  idFaturaEditando?: number;
  totalGasto: number | null = null;
  form!: FormGroup;
  operadoras: OperadoraRequest[] = [];

  constructor(
    private faturaService: FaturaService,
    private contratoService: ContratoService,
    private operadoraService: OperadoraService,
    private fb: FormBuilder,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.carregarFaturas();
    this.carregarFiliais();
    this.carregarOperadoras();

    this.faturaForm = this.fb.group({
      contratoId: ['', Validators.required],
      valorCobrado: ['', [Validators.required, Validators.min(1)]],
      status: ['', Validators.required],
    });

    this.form = this.fb.group({
      operadoraId: [null, Validators.required],
      data: [null, Validators.required]
    });
  }

  carregarFaturas(): void {
    this.faturaService.getFaturas().subscribe({
      next: (data) => this.faturas = data,
      error: (err) => console.error('Erro ao carregar faturas', err)
    });
  }

  abrirFormulario(): void {
    this.isFormVisible = true;
    this.editando = false;
    this.faturaForm.reset();
  }

  onSubmit(): void {
    if (this.faturaForm.valid) {
      const fatura = this.faturaForm.value;

      if (this.editando && this.idFaturaEditando != null) {
        fatura.id = this.idFaturaEditando;

        this.faturaService.editarFatura(fatura).subscribe({
          next: () => {
            console.log('Fatura atualizada!');
            this.fecharFormulario();
            this.carregarFaturas();
          },
          error: err => console.error('Erro ao atualizar fatura', err)
        });
      } else {
        this.faturaService.criarFatura(fatura).subscribe({
          next: () => {
            console.log('Fatura criada com sucesso!');
            this.fecharFormulario();
            this.carregarFaturas();
          },
          error: err => console.error('Erro ao criar fatura', err)
        });
      }
    }
  }

  fecharFormulario(): void {
    this.isFormVisible = false;
  }

  editarFatura(fatura: FaturaResponse): void {
    this.isFormVisible = true;
    this.editando = true;
    this.idFaturaEditando = fatura.id;

    this.faturaForm.patchValue({
      contratoId: fatura.contratoId,
      valorCobrado: fatura.valorCobrado,
      status: fatura.status
    });
  }

  atualizarFatura(): void {
    if (this.faturaForm.valid) {
      this.faturaService.editarFatura(this.faturaForm.value).subscribe({
        next: (faturaAtualizada) => {
          const index = this.faturas.findIndex(f => f.id === faturaAtualizada.id);
          if (index !== -1) {
            this.faturas[index] = faturaAtualizada;
          }
          this.fecharFormulario();
        },
        error: (err) => console.error('Erro ao editar fatura', err)
      });
    }
  }

  deletarFatura(id: number): void {
    this.faturaService.deletarFatura(id).subscribe({
      next: () => {
        this.faturas = this.faturas.filter(f => f.id !== id);
      },
      error: (err) => console.error('Erro ao excluir fatura', err)
    });
  }

  carregarFiliais(): void {
    this.contratoService.listar().subscribe({
      next: (contratos: ContratoResponse[]) => {
        this.contratosResponse = contratos;
      },
      error: (err) => console.error('Erro ao carregar filiais', err)
    });
  }

  getNomeFilial(contratoId: number): string {
    const contrato = this.contratosResponse?.find(o => o.id === contratoId);
    return contrato ? contrato.nomeFilial : 'Filial não encontrada';
  }

  carregarOperadoras(): void {
    this.operadoraService.listarNomesOperadoras().subscribe({
      next: (operadoras: OperadoraRequest[]) => {
        this.operadorasRequest = operadoras;
        this.operadoras = operadoras;
      },
      error: (err) => console.error('Erro ao carregar operadoras', err)
    });
  }

  getNomeOperadora(operadoraId: number): string {
    const operadora = this.operadorasRequest?.find(o => o.id === operadoraId);
    return operadora ? operadora.nomeOperadora : 'Operadora não encontrada';
  }

  getNomeOperadoraPorContrato(contratoId: number): string {
    const contrato = this.contratosResponse?.find(c => c.id === contratoId);
    if (!contrato) return 'Contrato não encontrado';

    const operadora = this.operadorasRequest?.find(o => o.id === contrato.operadoraId);
    return operadora ? operadora.nomeOperadora : 'Operadora não encontrada';
  }

  fecharModal(): void {
    this.modalAberto = false;
  }

  buscarTotalGasto(): void {
    const operadoraId = this.form.get('operadoraId')?.value;
    const dataSelecionada = this.form.get('data')?.value;

    const ano = new Date(dataSelecionada).getFullYear();
    const mes = new Date(dataSelecionada).getMonth() + 1;

    this.faturaService.getTotalGastoMensal(operadoraId, ano, mes)
      .subscribe(total => {
        this.totalGasto = total;
      });
  }
}
