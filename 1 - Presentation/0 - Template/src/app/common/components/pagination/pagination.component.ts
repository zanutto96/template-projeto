import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'make-pagination',
  template: `
      <div class="d-flex justify-content-end align-items-center mt-8 mb-8 w-100 gap-3 px-2 md:px-4"  *ngIf="vm && vm.summary">
      <ng-container  *ngIf=!disableSummary>
          <div class="pull-right">
            <span class="text-sm">Total de registros: </span> <span class="label label-primary text-base font-black">
            {{vm.summary.total}}
            </span>
          </div>
        </ng-container>
          <pagination class="mb-0" 
             (pageChanged)="onPageChanged($event)" 
              [itemsPerPage]="vm.summary.pageSize" 
              [totalItems]="vm.summary.total" 
              [maxSize]="5"
              previousText="Anterior"
              nextText="Próximo"
              [firstText]="'Primeira'"
              [lastText]="'Última'"
              [boundaryLinks]="true">
          </pagination>
        
       
      </div>`,
      standalone: false
})
export class MakePaginationComponent {

  @Input() vm: any;
  @Input() disableSummary: boolean = false;
  @Output() pageChanged = new EventEmitter<any>();

  initialPage: number;

  constructor() {

    this.initialPage = 1;
  }

  onPageChanged(e: any) {

    this.pageChanged.emit({
      PageIndex: e.page,
      PageSize: e.itemsPerPage,
    })
  }

}
