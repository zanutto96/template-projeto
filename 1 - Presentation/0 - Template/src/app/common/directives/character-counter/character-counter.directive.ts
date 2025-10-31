import { Directive, ElementRef, HostListener, Input, OnDestroy, OnInit, Renderer2 } from '@angular/core';

@Directive({
  selector: '[characterCounter]',
  standalone: false
})
export class CharacterCounterDirective implements OnInit, OnDestroy {
  @Input() counterClass: string = 'text-right text-xs mt-1'; // Classes CSS personalizáveis

  private counterElement: HTMLElement;
  private parentElement: HTMLElement;
  private maxLength: number;

  constructor(
    private el: ElementRef,
    private renderer: Renderer2
  ) {}

  ngOnInit(): void {
    // Obter maxlength do elemento
    const element = this.el.nativeElement;
    this.maxLength = element.maxLength > 0 ? element.maxLength : 256; // Usa maxLength do elemento ou 256 como padrão
    
    this.createCounterElement();
    this.updateCounter();
  }

  ngOnDestroy(): void {
    if (this.counterElement && this.parentElement) {
      this.renderer.removeChild(this.parentElement, this.counterElement);
    }
  }

  @HostListener('input', ['$event'])
  @HostListener('ngModelChange', ['$event'])
  onInput(event: any): void {
    this.updateCounter();
  }

  private createCounterElement(): void {
    // Criar elemento do contador
    this.counterElement = this.renderer.createElement('div');
    
    // Adicionar classes CSS
    const classes = this.counterClass.split(' ');
    classes.forEach(cls => {
      this.renderer.addClass(this.counterElement, cls);
    });

    // Encontrar o elemento pai apropriado (mat-form-field ou parent direto)
    this.parentElement = this.findParentFormField() || this.el.nativeElement.parentElement;

    // Inserir o contador após o elemento de input
    if (this.parentElement) {
      this.renderer.appendChild(this.parentElement, this.counterElement);
    }
  }

  private findParentFormField(): HTMLElement | null {
    let parent = this.el.nativeElement.parentElement;
    
    // Procurar por mat-form-field até 5 níveis acima
    for (let i = 0; i < 5 && parent; i++) {
      if (parent.tagName.toLowerCase() === 'mat-form-field' || 
          parent.classList.contains('mat-form-field')) {
        return parent;
      }
      parent = parent.parentElement;
    }
    
    return null;
  }

  private updateCounter(): void {
    if (!this.counterElement) return;

    const element = this.el.nativeElement;
    const currentLength = element.value ? element.value.length : 0;
    const maxLength = this.maxLength;

    // Atualizar texto do contador
    const counterText = `${currentLength}/${maxLength} caracteres`;
    this.renderer.setProperty(this.counterElement, 'textContent', counterText);

    // Remover todas as classes de alerta primeiro
    this.renderer.removeClass(this.counterElement, 'text-warning');
    this.renderer.removeClass(this.counterElement, 'text-danger');

    // Adicionar classe apropriada baseada na porcentagem
    if (currentLength >= maxLength) {
      // 100%: vermelho
      this.renderer.addClass(this.counterElement, 'text-danger');
    } else if (currentLength >= maxLength * 0.9) {
      // 90%+: laranja
      this.renderer.addClass(this.counterElement, 'text-warning');
    }
    // Abaixo de 90%: sem classe (texto padrão)
  }
}
