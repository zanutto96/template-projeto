<form [formGroup]="form" (ngSubmit)="onSave()" class="p-4">
  <div class="modal-header border-0 pb-0">
    <h4 class="modal-title">{{ model.#EntityRepositoryPrimaryKeyFront# ? 'Editar' : 'Cadastrar' }} #Entity#</h4>
    <button type="button" class="btn-close" (click)="onNoClick()" aria-label="Close"></button>
  </div>

  <div class="modal-body">
    <div class="row g-3">
      #EntityHtmlFormFields#
    </div>
  </div>

  <div class="modal-footer border-0 pt-0">
    <button type="button" class="btn btn-outline-secondary" (click)="onNoClick()">
      <i class="fas fa-times me-2"></i>Cancelar
    </button>
    <button type="submit" class="btn btn-primary" [disabled]="form.invalid">
      <i class="fas fa-save me-2"></i>Salvar
    </button>
  </div>
</form>

