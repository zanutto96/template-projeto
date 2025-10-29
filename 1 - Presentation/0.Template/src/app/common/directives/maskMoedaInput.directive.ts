import { Directive, ElementRef, HostListener, Renderer2, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Directive({
  selector: '[appMaskMoedaInput]',
  standalone: false,
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => MaskMoedaInputDirective),
    multi: true
  }]
})
export class MaskMoedaInputDirective implements ControlValueAccessor {
  private onChange: (value: any) => void;
  private onTouched: () => void;

  constructor(private el: ElementRef, private renderer: Renderer2) {}

  writeValue(value: any): void {
    if (value !== undefined && value !== null) {
      const formatter = new Intl.NumberFormat('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
      this.renderer.setProperty(this.el.nativeElement, 'value', 'R$ ' + formatter.format(value / 100));
    } else {
      this.renderer.setProperty(this.el.nativeElement, 'value', '');
    }
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  @HostListener('input', ['$event']) onInput(event: any) {
    const formatter = new Intl.NumberFormat('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    const input = event.target;
    let value = input.value.replace(/\D+/g, '');
    if (value.length === 0) {
      this.onChange(null);
      return;
    }

    const maxDigits = parseInt(input.dataset.maxDigits);
    if (value.length > maxDigits) {
      value = value.substring(0, maxDigits);
    }

    this.onChange(parseInt(value));
    this.writeValue(value);
  }

  @HostListener('blur', ['$event']) onBlur(event: any) {
    if (this.onTouched) {
      this.onTouched();
    }
    const input = event.target;
    input.value = input.value.replace(/\D+/g, '');
    if (input.value.length === 0) return;
    const valueInCents = parseInt(input.value);
    input.value = 'R$ ' + (valueInCents / 100).toFixed(2);
  }
}
