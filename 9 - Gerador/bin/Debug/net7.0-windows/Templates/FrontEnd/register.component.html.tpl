<form ##EntityLowerCase#Form="ngForm" [formGroup]="form" (ngSubmit)="onSave(#EntityLowerCase#Form)">
  <h4 mat-dialog-title>Register</h4>
  <mat-dialog-content class="mat-typography modal-pages">
    #EntityHtmlMaterialProperties#
  </mat-dialog-content>

  <mat-dialog-actions align="end" class="btn-dialog-bottom">
    <button type="button" mat-raised-button (click)="onNoClick()" color="basic">
    <svg xmlns="http://www.w3.org/2000/svg"
                 height="24px"
                 viewBox="0 0 24 24"
                 fill="currentColor"
                 class="w-6 h-6">
                <path d="M0 0h24v24H0V0z"
                      fill="none"
                      opacity=".87" />
                <path d="M12 2C6.47 2 2 6.47 2 12s4.47 10 10 10 10-4.47 10-10S17.53 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8zm3.59-13L12 10.59 8.41 7 7 8.41 10.59 12 7 15.59 8.41 17 12 13.41 15.59 17 17 15.59 13.41 12 17 8.41z" />
            </svg>
            Cancelar
            </button>
    <button type="submit" mat-raised-button color="primary" [disabled]="form.valid ? false : true">
      <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor"
        class="w-6 h-6">
        <path stroke-linecap="round" stroke-linejoin="round"
          d="M9 12.75L11.25 15 15 9.75M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
      </svg>

      Salvar
    </button>
  </mat-dialog-actions>
</form>

