<div class="card border-0 rounded-0">
  <app-generic-table
    [data]="list"
    [config]="tableConfig"
    (actionClicked)="onActionClicked($event)"
    (pageChanged)="onPageChanged($event)">
  </app-generic-table>
</div>

