<div class="card border-0 rounded-0">
  <div class="card-header">
    <h5 class="card-title mb-0">Lista de #Entity#</h5>
  </div>
  <div class="card-body">
    @if (loading) {
      <div class="text-center py-4">
        <div class="spinner-border text-primary" role="status">
          <span class="visually-hidden">Carregando...</span>
        </div>
        <p class="mt-2">Carregando #Entity#...</p>
      </div>
    } @else if (list && list.length > 0) {
      <app-generic-table
        [data]="list"
        [config]="tableConfig"
        (addClicked)="onAdd()"
        (actionClicked)="onActionClicked($event)"
        (pageChanged)="onPageChanged($event)">
      </app-generic-table>
    } @else {
      <div class="text-center py-4">
        <i class="fas fa-users fa-3x text-muted mb-3"></i>
        <h5 class="text-muted">Nenhum #Entity# encontrado</h5>
        <p class="text-muted">Clique em "Adicionar" para cadastrar o primeiro #Entity#.</p>
      </div>
    }
  </div>
</div>

