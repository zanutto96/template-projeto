import { Pipe, PipeTransform, } from '@angular/core';

@Pipe({
  name: 'percentBR',
  standalone: false
})
export class PercentBR implements PipeTransform {

  constructor() {

  }

  transform(val: number, decimalPlaces?: any): any {
    if (val !== undefined && val !== null) {
      decimalPlaces = decimalPlaces ? decimalPlaces : 0;
      return val.toLocaleString("pt-BR", { minimumFractionDigits: decimalPlaces, maximumFractionDigits: decimalPlaces }) + ' %';
    } else {
      return '';
    }
  }
}
