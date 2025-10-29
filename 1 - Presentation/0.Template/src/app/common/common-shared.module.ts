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


@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    MatIconModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule
  ],
  declarations: [
    CustomSelectComponent,
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
    CustomSelectComponent,
    UploadCustomComponent,
    ImgSpinnerComponent,
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
  ]
})

export class CommonSharedModule {

}
