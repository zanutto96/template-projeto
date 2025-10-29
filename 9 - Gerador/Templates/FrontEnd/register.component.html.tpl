<div class="register-modal-container">
  <!-- Modal Header -->
  <div class="modal-header-custom">
    <h2 class="modal-title">
      {{ model.#EntityRepositoryPrimaryKeyFront# ? 'Editar' : 'Novo' }} #Entity#
    </h2>
    <button type="button"
            class="btn-close-custom"
            (click)="onNoClick()"
            aria-label="Fechar">
      <i class="fas fa-times"></i>
    </button>
  </div>

  <!-- Modal Body -->
  <div class="modal-body-custom">
    <form [formGroup]="form" (ngSubmit)="onSave()" class="register-form">

      <!-- Form Section -->
      <div class="form-section">
        #EntityHtmlFormFields#
      </div>

      <!-- Form Actions -->
      <div class="form-actions">
        <button type="button"
                class="btn btn-outline-secondary"
                (click)="onNoClick()">
          <i class="fas fa-times"></i>
          Cancelar
        </button>

        <button type="submit"
                class="btn btn-primary"
                [disabled]="form.invalid">
          <i class="fas fa-save"></i>
          {{ model.#EntityRepositoryPrimaryKeyFront# ? 'Atualizar' : 'Salvar' }}
        </button>
      </div>
    </form>
  </div>
</div>

