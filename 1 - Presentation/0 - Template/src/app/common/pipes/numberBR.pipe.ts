import { Pipe, PipeTransform, } from '@angular/core';

@Pipe({
  name: 'numberBR',
  standalone: false
})
export class NumberBR implements PipeTransform {

  constructor() {

  }

  transform(val: number, decimalPlaces?: any): any {
    if (val !== undefined && val !== null) {
      decimalPlaces = decimalPlaces ? decimalPlaces : 0;
      return val.toLocaleString("pt-BR", { minimumFractionDigits: decimalPlaces, maximumFractionDigits: decimalPlaces });
    } else {
      return '';
    }
  }
}
