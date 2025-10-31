import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatIconModule } from "@angular/material/icon";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatSelectModule } from "@angular/material/select";
import { MatInputModule } from "@angular/material/input";
import { CustomSelectComponent } from "./components/custom-select/custom-select.component";
import { ImgSpinnerComponent } from "./components/img-spinner/img-spinner.component";
import { LoadingComponent } from "./components/loading/loading.component";
import { MakePaginationComponent } from "./components/pagination/pagination.component";
import { UploadCustomComponent } from "./components/upload-custom/upload-custom.component";
import { CharacterCounterDirective } from "./directives/character-counter/character-counter.directive";
import { MaskMoedaInputDirective } from "./directives/maskMoedaInput.directive";
import { CurrencyBR } from "./pipes/currencyBR.pipe";
import { DateBR } from "./pipes/dateBR.pipe";
import { NumberBR } from "./pipes/numberBR.pipe";
import { PercentBR } from "./pipes/percentBR.pipe";
import { SafePipe } from "./pipes/safe.pipe";
import { SplitPipe } from "./pipes/split.pipe";
import { GenericTableModule } from "./components/generic-table/generic-table.module";
import { AlertTemplateComponent } from "./components/alert-template/alert-template.component";
import { NotificationTemplateComponent } from "./components/notification-template/notification-template.component";
import { ShellModule } from "../shell/shell.module";
import { FileUploaderDirective } from "./directives/file-uploader/file-uploader.directive";


@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    MatIconModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    CustomSelectComponent,
    GenericTableModule,
    ShellModule
  ],
  declarations: [
    AlertTemplateComponent,
    FileUploaderDirective,
    NotificationTemplateComponent,
    UploadCustomComponent,
    MakePaginationComponent,
    CurrencyBR,
    NumberBR,
    PercentBR,
    DateBR,
    SafePipe,
    LoadingComponent,
    MaskMoedaInputDirective,
    ImgSpinnerComponent,
    SplitPipe,
    CharacterCounterDirective,
  ],
  providers: [
  ],
  exports: [
    MatIconModule,
    AlertTemplateComponent,
    NotificationTemplateComponent,
    GenericTableModule,
    UploadCustomComponent,
    ImgSpinnerComponent,
    FileUploaderDirective,
    MakePaginationComponent,
    CurrencyBR,
    NumberBR,
    PercentBR,
    DateBR,
    SafePipe,
    LoadingComponent,
    MaskMoedaInputDirective,
    SplitPipe,
    CharacterCounterDirective,
    CustomSelectComponent
  ]
})

export class CommonSharedModule {

}
