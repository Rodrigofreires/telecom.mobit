export interface ContratoRequest {
    id: number;
    nomeFilial: string;
    operadoraId: number;
    planoContratado: string;
    dataVencimento: string;
    valorMensal: number;
    status: boolean;
    dataInicio?: string;
  }
  