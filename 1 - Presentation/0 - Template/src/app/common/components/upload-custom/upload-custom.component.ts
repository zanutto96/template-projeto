import { ChangeDetectorRef, Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { NotificationsService } from '../../../layout/common/notifications/notifications.service';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'upload-custom',
  templateUrl: './upload-custom.component.html',
  styleUrls: ['./upload-custom.component.scss'],
  standalone: false
})
export class UploadCustomComponent implements OnInit, OnDestroy {

  @ViewChild('file', { static: false }) fileUpload: any;

  @Output() onChangeUploadExternal = new EventEmitter<any>();
  @Output() onChangeStartUpload = new EventEmitter<any>();
  @Output() onChangeEndUpload = new EventEmitter<any>();
  @Output() onChangeCancelUpload = new EventEmitter<any>();

  @Input() label: any;
  @Input() accept: any;
  @Input() ctrlName: any;
  @Input() model: any
  @Input() folder: any;
  @Input() enabledUploadExternal: boolean;
  @Input() rename: boolean;
  @Input() pasteArea: boolean;
  @Input() pasteAreaText: any;
  @Input() enableCopyLink: boolean;
  @Input() maxsize: number;
  @Input() mostrarInput: boolean = true;
  @Input() mostrarLabel: boolean = true;
  @Input() stylecss = 'max-width:200px; margin-top: 8px';
  @Input() form: any;
  @Input() elementId: string;
  @Input() IsMultiple: boolean = false;

  fileName: any;
  fileNameOld: any;
  downloadUri: any;
  fileUri: any;
  isImage: boolean;
  className: any;
  listFilesUploaded: any = [];
  loading: boolean = false;
  _notificationEmitter: any;

  constructor(
    private api: ApiService,
    private notificationsService: NotificationsService,
    private ref: ChangeDetectorRef
  ) {

    this.downloadUri = environment.downloadUrl;
    this.fileUri = this.downloadUri + "/" + this.folder + "/" + this.fileName;
    this.enabledUploadExternal = false;
    this.accept = "*.*";
    this.rename = true;
    this.pasteArea = false;
    this.isImage = false;
    this.enableCopyLink = false;
    this.pasteAreaText = "Arraste e solte arquivos ou cole PrintScreens de telas";
    this.maxsize = 2;
    this.elementId = "";
  }

  ngOnInit(): void {

    this.edit();

    //   this._notificationEmitter = GlobalEmitters.getNotificationEmitter().subscribe((not: any) => {

    //   if (not.event == "edit") {
    //     this.edit();
    //   }

    //   if (not.event == "init") {
    //     this.fileNameOld = null;
    //     this.fileName = null;
    //   }
    // })

    if (this.pasteArea) {

      let area = document.getElementById("upload-component-paste-area");

      //area.addEventListener("paste", (e) => this.handlePaste(e));
      //area.ondragover = () => { this.className = 'upload-component-paste-area'; return false; };
      //area.ondrop = (e) => { this.handleDrop(e) }

    }

  }

  edit() {
    this.fileNameOld = this.model[this.ctrlName];
    this.fileName = this.model[this.ctrlName];
    this.verifyFileName(this.fileName)
  }

  copyToClipboard(file: any) {
    var copyText = document.getElementById(file) as any;
    copyText.select();
    document.execCommand("Copy");
  }

  handleDrop(e: any) {
    e.preventDefault();
    e.dataTransfer.files
    this.uploadFileOnPaste(e.dataTransfer.files[0]);
  }

  handlePaste(e: any) {
    for (var i = 0; i < e.clipboardData.items.length; i++) {
      var item = e.clipboardData.items[i];
      this.uploadFileOnPaste(item.getAsFile());
    }
  }

  uploadFileOnPaste(file: any) {

    this.fileNameOld = file.name;

    if (this.enabledUploadExternal)
      this.uploadCustom(file, this.rename);
    else
      this.uploadDefault(file, this.rename);

  }

  ngAfterViewChecked(): void {
    this.ref.detectChanges();
  }

  onChange(event: any) {

    if (event.target.files.length == 0)
      return false;

    let file: File = event.target.files[0];
    this.fileNameOld = file.name;

    //if (this.maxsize) {
    //  if (file.size > (this.maxsize * 1024 * 1000)) {
    //    this.notificationsService.warning('A imagem é muito grande');

    //    event.target.files = [];
    //    return false;
    //  }
    //}

    if (this.enabledUploadExternal)
      return this.uploadCustom(file, this.rename);

    return this.uploadDefault(file, this.rename);
  }

  onChangeMultiple(event: any) {

    if (event.target.files.length == 0)
      return false;

    let file: File[] = event.target.files;

    return this.uploadDefaultMultiple(file, this.rename);
  }

  uploadCustom(file: File, rename: any) {
    this.onChangeUploadExternal.emit(file)
    this.model[this.ctrlName] = file.name;
    this.fileName = file.name;
    this.pasteArea = false;
    this.verifyFileType(file);
    return true;
  }

  verifyFileType(file: File) {
    this.isImage = false;
    if (file.type == "image/png") this.isImage = true;
    if (file.type == "image/jpeg") this.isImage = true;
    if (file.type == "image/gif") this.isImage = true;
  }

  verifyFileName(fileName: string) {
    if (fileName) {
      this.isImage = false;
      if (fileName.endsWith("png")) this.isImage = true;
      if (fileName.endsWith("jpg")) this.isImage = true;
      if (fileName.endsWith("gif")) this.isImage = true;
    }
  }

  uploadDefault(file: File, rename: boolean) {
    this.onChangeStartUpload.emit();
    this.loading = true;
    this.api.upload(file, this.folder, rename)
      .subscribe(result => {
        this.model[this.ctrlName] = result.data[0];
        this.fileName = result.data[0]
        this.pasteArea = false;
        this.verifyFileType(file);
        this.onChangeEndUpload.emit(result.data[0]);
        this.loading = false;
      });
    return true;
  }

  uploadDefaultMultiple(file: File[], rename: boolean) {
    this.onChangeStartUpload.emit();
    this.api.uploadMultiple(file, this.folder, rename).subscribe(result => {
      this.listFilesUploaded = [];
      if (result.length > 0) {
        for (let i = 0; i < result.length; i++) {
          this.listFilesUploaded.push(result[i]);
        }
      }
      this.onChangeEndUpload.emit(this.listFilesUploaded);
    });
    return true;
  }

  onDelete() {
    this.reset();
    this.onChangeCancelUpload.emit({ cancel: true });
  }

  reset() {
    this.fileUpload.nativeElement.value = '';
    this.model[this.ctrlName] = null;
    this.fileName = null;
    this.fileNameOld = null;
  }

  ngOnChanges() {
    this.ref.detectChanges();
    this.edit();
  }

  ngOnDestroy() {
    if (this._notificationEmitter)
      this._notificationEmitter.unsubscribe();
  }
}
