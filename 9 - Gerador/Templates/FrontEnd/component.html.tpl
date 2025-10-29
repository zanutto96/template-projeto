<div class="flex flex-col flex-auto min-w-0 bg-card">
    <div class="relative flex flex-col sm:flex-row flex-0 sm:items-center sm:justify-between py-6 px-6 md:px-6 border-b bg-white shadow-sm">
        <div class="text-3xl font-extrabold tracking-tight text-gray-900 mb-4 sm:mb-0">
            <i class="fas fa-list me-3 text-primary"></i>#Entity#
        </div>
        <div class="flex gap-2">
            <button type="button" class="btn btn-primary d-flex align-items-center" (click)="onAdd()"
                *ngIf="currentView == 'showList'">
                <i class="fas fa-plus me-2"></i>Adicionar
            </button>
        </div>
    </div>
    <div class="container-fluid p-0 m-0 bg-gray-50" *ngIf="currentView == 'showList'">
        <#EntityLowerCase#-list (edit)="onEdit($event)"></#EntityLowerCase#-list>
    </div>
</div>
