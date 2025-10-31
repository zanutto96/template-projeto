import { Component, Input, HostBinding, PLATFORM_ID, Inject, ChangeDetectorRef } from '@angular/core';
import { isPlatformServer } from '@angular/common';
import { CommonModule } from '@angular/common';

import { AppShellConfig } from '../config/app-shell.config';

@Component({
  selector: 'cc-image-shell',
  templateUrl: './image-shell.component.html',
  styleUrls: ['./image-shell.component.scss'],
  standalone: true,
  imports: [CommonModule],
  
})
export class ImageShellComponent {
  // To debug shell styles, change configuration in the assets/app-shell.config.json file
  private debugMode = (AppShellConfig.settings && AppShellConfig.settings.debug) ? AppShellConfig.settings.debug : false;

  // tslint:disable-next-line:variable-name
  private _src = '';
  // tslint:disable-next-line:variable-name
  _alt = '';
  // tslint:disable-next-line:variable-name
  _mode = '';

  @HostBinding('class.img-loaded') imageLoaded = false;

  @HostBinding('style.backgroundImage') backgroundImage: string;

  @HostBinding('attr.mode')
  @Input()
  set mode(val: string) {
    console.log('Setting mode to:', val);
    this._mode = (val !== undefined && val !== null) ? val : '';
  }
  get mode(): string {
    return this._mode;
  }

  @Input()
  set src(val: string) {
    console.log('Valor recebido para src:', val);
    this._src = (val !== undefined && val !== null) ? val : '';
    console.log('_src set to:', this._src);
    this.cdr.detectChanges();

    if (this._mode === 'cover') {
      // Unset the background-image
      this.backgroundImage = 'unset';
    }

    // Show loading indicator
    // When using SSR (Server Side Rendering), avoid the loading animation while the image resource is being loaded
    if (isPlatformServer(this.platformId)) {
      this.imageLoaded = true;
    } else {
      this.imageLoaded = false;
    }
  }
  get src(): string {
    return this._src;
  }

  @Input()
  set alt(val: string) {
    this._alt = (val !== undefined && val !== null) ? val : '';
  }

  constructor(
    @Inject(PLATFORM_ID) private platformId,
    private cdr: ChangeDetectorRef
  ) {
    console.log('ImageShellComponent constructor called');
  }

  _imageLoaded() {
    this.imageLoaded = true;
    console.log('Imagem carregada:', this._src);
    // If it's a cover image then set the background-image property accordingly
    if (this._mode === 'cover') {
      this.backgroundImage = 'url(' + this._src + ')';
    }
    this.cdr.detectChanges();
  }
}
