import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'split',
  standalone: false
})
export class SplitPipe implements PipeTransform {
  transform(value: string, separator: string = ',', index?: number): string[] | string {
    if (!value) return index !== undefined ? '' : [];
    const parts = value.split(separator);

    return index !== undefined ? parts[index] ?? '' : parts;
  }
}
