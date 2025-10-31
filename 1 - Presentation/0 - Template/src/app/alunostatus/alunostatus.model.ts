import { BaseFilter } from '../common/models/base-filter.model';

export interface AlunoStatus extends BaseFilter {
  alunoStatusId: number;
  descricao: string | null;

}
