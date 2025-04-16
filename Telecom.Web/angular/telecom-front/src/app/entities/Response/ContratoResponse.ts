export interface ContratoResponse {
  id: number;
  nomeFilial: string;
  operadoraId: number;
  planoContratado: string;
  valorMensal: number;
  status: boolean;
  dataInicio?: string;
  dataVencimento?: string;
}
