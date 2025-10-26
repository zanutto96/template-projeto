<div class="card border-0 rounded-0 table-responsive">
 
    <table mat-table matSort [dataSource]="dataSource" class="border-b-2 border-gray-600" *ngIf="dataSource" (matSortChange)="onSort($event)" ##Entity#Table>

      #EntityHtmlTableHeaderFilterMaterialProperties#
      
      <ng-container matColumnDef="filtro_buttons">
        <th mat-header-cell *matHeaderCellDef class="bg-slate-50"> </th>
      </ng-container>

      #EntityHtmlTableHeaderMaterialProperties#

      <ng-container matColumnDef="buttons">
        <th mat-header-cell *matHeaderCellDef class="bg-slate-100 text-sm"> </th>
        <td mat-cell *matCellDef="let element" class="text-nowrap fixed-buttons">
          <button class="btn btn-danger btn-sm mx-1" (click)="onDelete(element.#EntityRepositoryPrimaryKeyFront#)">
            <i class="fas fa-trash"></i>
          </button>
          <button class="btn btn-primary btn-sm mx-1" (click)="onEdit(element.#EntityRepositoryPrimaryKeyFront#)">
            <i class="fas fa-pencil"></i>
          </button>
        </td>
      </ng-container>

      <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
      <tr mat-header-row *matHeaderRowDef="filterColumns"></tr>
      <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
    </table>
</div>
<make-pagination [vm]="vm" (pageChanged)="onPageChanged($event)"></make-pagination>

