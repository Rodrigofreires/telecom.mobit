import { ContratoResponse } from "./ContratoResponse";
import { OperadoraServicoResponse } from "./OperadoraServicoResponse";

export interface FaturaResponse {
DataVencimento: string,
DataEmissao: string,
id: number,
contratoId: number,
valorCobrado: number,
status: string,
Contrato? : ContratoResponse,
Operadora?: OperadoraServicoResponse,

}
  