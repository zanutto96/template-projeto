import { Component, OnInit, Input, Output, EventEmitter, forwardRef, ChangeDetectorRef } from '@angular/core';
import { Observable, of } from 'rxjs';

import { ControlValueAccessor, NG_VALUE_ACCESSOR, FormControl } from '@angular/forms';
import { MatSelectChange } from '@angular/material/select';
import { ApiService } from '../../services/api.service';

@Component({
    selector: 'custom-select',
    template: `
      <mat-form-field appearance="outline" class="w-100">
        <mat-label>{{ placeholder }}</mat-label>
        <mat-select
          [formControl]="selectControl"
          [multiple]="multiple"
          [disabled]="disabled"
          (selectionChange)="onSelectionChange($event)"
          (openedChange)="onOpenedChange($event)">
          <mat-option *ngIf="loading">Buscando...</mat-option>
          <mat-option *ngIf="!loading && items.length === 0">Nenhum registro encontrado</mat-option>
          <mat-option
            *ngFor="let item of items"
            [value]="multiple ? item[tableIdFieldName] : item">
            {{ item[tableLabelFieldName] }}
          </mat-option>
        </mat-select>
      </mat-form-field>`,
    standalone: false,
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => CustomSelectComponent),
            multi: true
        }
    ]
})
export class CustomSelectComponent implements OnInit, ControlValueAccessor {

    selectControl = new FormControl();
    items: any[] = [];
    items$!: Observable<any[]>;
    @Input() list: any[] = [];
    @Input() _value: string | string[] | undefined;
    @Input() tableName: string = '';
    @Input() id: string = '';
    @Input() searchTerm: string = '';
    @Input() searchFieldName: string = 'nome';
    @Input() tableLabelFieldName: string = 'nome';
    @Input() tableIdFieldName: string = 'id';
    @Input() placeholder: string = 'Selecione';
    @Input() appendTo: string = 'body';
    @Input() loading: boolean = false;
    @Input() multiple: boolean = false;
    @Input() disabled: boolean = false;
    @Output() change: EventEmitter<any> = new EventEmitter();

    private _filter: any = {};

    get value() {
        return this._value;
    }

    set value(value) {
        this._value = value;
        if (this.multiple && Array.isArray(value)) {
            this.selectControl.setValue(value);
        } else {
            this.selectControl.setValue(value);
        }
        this.propagateChange(value);
    }

    get filter() {
        return this._filter;
    }

    @Input() set filter(value) {
        this._filter = value;
        this.getItems({ term: '' });
        this.changeDetector.detectChanges();
    }

    constructor(
        private api: ApiService,
        private changeDetector: ChangeDetectorRef
    ) {}

    propagateChange = (_: any) => { };

    writeValue(obj: any): void {
        this.value = obj;
    }
    registerOnChange(fn: any): void {
        this.propagateChange = fn;
    }
    registerOnTouched(fn: any): void {
        this.selectControl.valueChanges.subscribe(fn);
    }

    setDisabledState?(isDisabled: boolean): void {
        if (isDisabled) {
            this.selectControl.disable();
        } else {
            this.selectControl.enable();
        }
    }

    ngOnInit() {
        if (this.tableName) {
            let filters: any = { ...this.filter };

            if (this.searchFieldName && this.searchTerm) {
                filters[this.searchFieldName] = this.searchTerm;
            }

            this.items$ = this.api.getDataItem(this.tableName, filters);
            this.items$.subscribe(data => {
                this.items = data;
            });
        } else {
            this.items = this.list;
        }
    }

    onSelectionChange(event: MatSelectChange) {
        const value = event.value;
        if (value !== undefined) {
            if (this.multiple) {
                this.value = value;
            } else {
                this.value = value[this.tableIdFieldName];
            }
            this.propagateChange(this.value);
            this.change.emit(this.value);
        } else {
            this.value = undefined;
            this.propagateChange(undefined);
            this.change.emit(undefined);
        }
    }

    onOpenedChange(isOpen: boolean) {
        if (isOpen && this.tableName) {
            this.getItems({ term: '' });
        }
    }

    onClear() {
        this.value = undefined;
        this.selectControl.setValue(null);
        this.onSelectionChange({ value: null } as MatSelectChange);
    }

    getItems(term: any) {
        if (this.tableName) {
            this.loading = true;
            this.searchTerm = term.term;
            let filters: any = { ...this.filter };

            if (this.searchFieldName && this.searchTerm) {
                filters[this.searchFieldName] = this.searchTerm;
            }

            this.api.getDataItem(this.tableName, filters).subscribe(
                (data) => {
                    this.items = data;
                    this.changeDetector.detectChanges();
                },
                (error) => {
                    console.error('Erro ao obter dados:', error);
                },
                () => {
                    setTimeout(() => this.loading = false, 1000);
                }
            );
        }
    }
}
