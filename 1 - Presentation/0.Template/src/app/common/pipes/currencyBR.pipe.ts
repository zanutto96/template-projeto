import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'currencyBR',
      standalone: false
})
export class CurrencyBR implements PipeTransform {

  constructor() {

  }

  transform(val: number | string): any {
    if (val !== undefined && val !== null) {
      if (typeof (val) == 'string') val = parseFloat(val)
      return val.toLocaleString("pt-BR", { style: 'currency', currency: 'BRL' });
    } else {
      return '';
    }
  }
}
