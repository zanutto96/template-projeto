<div class="flex flex-col flex-auto min-w-0 bg-card">
    <div
        class="relative flex flex-col sm:flex-row flex-0 sm:items-center sm:justify-between py-6 px-6 md:px-6 border-b">
        <div class="text-3xl font-extrabold tracking-tight">
             #Entity#
        </div>
        <div class="col-md-2">
            <button type="button" class="btn btn-primary w-100" (click)="onAdd()"
                *ngIf="currentView == 'showList'">Adicionar</button>
        </div>
    </div>
    <div class="container-fluid p-0 m-0" *ngIf="currentView == 'showList'">
        <#EntityLowerCase#-list (edit)="onEdit($event)"></#EntityLowerCase#-list>
    </div>

</div>
