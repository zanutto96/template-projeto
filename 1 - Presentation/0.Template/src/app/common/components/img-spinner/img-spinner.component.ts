import { Component, Input, OnInit } from "@angular/core";
// import { IonImg, IonSpinner, ModalController } from '@ionic/angular';

@Component({
  selector: "app-img-spinner",
  templateUrl: "./img-spinner.component.html",
  styleUrls: ["./img-spinner.component.scss"],
  standalone: false
})
export class ImgSpinnerComponent implements OnInit {
  @Input() url: string = "";
  @Input() url2: string = "";
  @Input() alt: string = "";
  @Input() classCustom: string = "";
  @Input() name:
    | "bubbles"
    | "circles"
    | "circular"
    | "crescent"
    | "dots"
    | "lines"
    | "lines-sharp"
    | "lines-sharp-small"
    | "lines-small"
    | undefined = "lines";
  // @ViewChild(IonImg) image: IonImg;
  // @ViewChild(IonSpinner) spinner: IonSpinner;

  public showSpinner: boolean = false;
  public showError: boolean = false;
  public showError2: boolean = false;
  public showUrl2: boolean = false;
  constructor() { }

  ngOnInit(): void {
    this.onWillLoad(null);
    setTimeout(() => {
      // if (this.url == '' || this.url == null)
      // this.onDidLoad(null);
    }, 1000);
  }

  toggleSpinner(showSpinner: boolean = false) {
    this.showSpinner = showSpinner;
  }

  toggleError(error: boolean = false) {
    this.showError = error;
  }

  onError(event: any, i: number) {
    if (this.url2 && i == 0) {
      this.showUrl2 = true;
    } else {
      this.toggleSpinner(false);
      this.toggleError(true);
    }
  }
  onDidLoad(event: any) {
    this.toggleSpinner(false);
    this.toggleError(false);
  }
  onWillLoad(event: any) {
    this.toggleSpinner(true);
    this.toggleError(false);
  }

  onImageError(event: any, i: number): void {
    this.onError(event, i);
  }
}
