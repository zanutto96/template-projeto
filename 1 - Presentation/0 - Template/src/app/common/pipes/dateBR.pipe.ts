import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'dateBR',
  standalone: false
})
export class DateBR implements PipeTransform {

  constructor() {

  }

  transform(val: any): any {
    if (val !== undefined && val !== null) {
      const dataString = val;
      const char = dataString.indexOf('/') !== -1 ? '/' : '-';
      const parts = dataString.split(char);

      if (char === '-') {
        const novaDataString = `${parts[2]}/${parts[1]}/${parts[0]}`;
        return novaDataString;
      }

      const novaDataString = `${parts[1]}/${parts[0]}/${parts[2]}`;
      return novaDataString;
    } else {
      return '';
    }
  }
}
