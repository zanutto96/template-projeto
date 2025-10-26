import { Component, EventEmitter, Inject, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { #Entity#Service } from '../#EntityLowerCase#.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: '#EntityLowerCase#-register',
  templateUrl: './#EntityLowerCase#-register.component.html',
  styleUrls: ['./#EntityLowerCase#-register.component.scss']
})
export class #Entity#RegisterComponent implements OnInit {

  @Input() id: any = 0;
  @Output() response = new EventEmitter();

  public form: FormGroup;
  public model: any = {};
  public showSelect = false;

  constructor(private service: #Entity#Service,  public dialogRef: MatDialogRef<#Entity#RegisterComponent>,
    @Inject(MAT_DIALOG_DATA) public data) {
    this.form = new FormGroup(this.getFormControls());
  }

  ngOnInit(): void {
    this.getData();
  }

  getFormControls(moreFormControls?: any) {
    var formControls = Object.assign({
      #EntityFormControls#
    }, moreFormControls || {});
    return formControls;
  }

  onCancel() {
    this.response.emit();
  }

  onSave(form: any) { 
    if (this.model.#EntityRepositoryPrimaryKeyFront#) {
      this.service.savePartial('#Entity#', this.model).subscribe((data : any) => {
        this.dialogRef.close(true);
      });
    } else {
      this.service.save('#Entity#', this.model).subscribe((data: any) => {
        this.dialogRef.close(true);
      });
    }
  }

  getData() {
    var filters = {
      #EntityRepositoryPrimaryKey#: this.data.id
      }
    if (filters.#EntityRepositoryPrimaryKey# > 0) {
      this.service.get('#Entity#', 'getById', filters).subscribe((result: any) => {
        this.model = result.data; 
        this.showSelect = true;
      });
    } else {
        this.showSelect = true;
    }
  }

  onNoClick(): void {
    this.dialogRef.close(false);
  }


}
