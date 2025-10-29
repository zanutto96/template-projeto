import { Component, OnInit, ViewChild, AfterViewInit, OnDestroy, Inject, PLATFORM_ID, makeStateKey, TransferState } from '@angular/core';
import { isPlatformServer } from '@angular/common';
import { CdkPortalOutlet, Portal, ComponentPortal } from '@angular/cdk/portal';
import { Subscription } from 'rxjs';

import { LayoutsService } from '../layouts.service';
import { IAppLayout, availableLayouts, defaultLayout } from '../../../app-layouts';

const MOBILE_DEVICE = makeStateKey<string>('mobile-device');

@Component({
  selector: 'app-dynamic-layout',
  standalone: false,
  templateUrl: './dynamic-layout.component.html'
})
export class DynamicLayoutComponent implements OnInit, AfterViewInit, OnDestroy {
  // dynamicLayoutPortalOutlet is a reference to the <ng-template> in the view that's gonna render the different layouts
  @ViewChild(CdkPortalOutlet, {static: false}) dynamicLayoutPortalOutlet: CdkPortalOutlet;

  // dynamicLayoutPortal is a reference to the portal that we are gonna render in the dynamicLayoutPortalOutlet
  dynamicLayoutPortal: Portal<any>;

  // Subscription to the Dynamic Layout switching mechanism
  dynamicLayoutSwitchSubscription: Subscription;

  constructor(
    @Inject(PLATFORM_ID) private platformId: object,
    private state: TransferState,
    private layoutsService: LayoutsService
  ) {
    // Check if the user is requesting the app from a mobile device
    let mobileDevice = 'not-mobile';

    // In Angular 19, server-side rendering uses different mechanisms
    if (isPlatformServer(this.platformId)) {
      // Server-side logic can be added here if needed
      this.state.set(MOBILE_DEVICE, mobileDevice);

      console.log('we\'re rendering from the server.');
      console.log(`MOBILE_DEVICE: ${mobileDevice}`);
    } else {
      mobileDevice = this.state.get(MOBILE_DEVICE, mobileDevice);

      console.log('we\'re rendering from the browser.');
      console.log(`MOBILE_DEVICE from TransferState: ${mobileDevice}`);
    }

    if (mobileDevice !== 'not-mobile') {
      this.renderLayoutPortal('mobile');
    } else {
      this.renderLayoutPortal(defaultLayout.name);
    }
  }

  ngOnInit() { }

  ngOnDestroy() {
    this.dynamicLayoutSwitchSubscription.unsubscribe();
  }

  ngAfterViewInit(): void {
    this.dynamicLayoutSwitchSubscription = this.layoutsService.switchLayoutSubject.subscribe(
      (selectedLayoutName) => {
        this.renderLayoutPortal(selectedLayoutName);
      },
      (error) => {
        console.log('switchLayoutSubject [DynamicLayoutComponent] - error', error);
      },
      () => {
        console.log('switchLayoutSubject [DynamicLayoutComponent] - complete');
      }
    );
  }

  renderLayoutPortal(layoutName: string): void {
    // Check if the layout we want to render exists. We check against the list defined in the src/app-layouts.ts file
    const layoutToRender: IAppLayout = availableLayouts.find((layout: IAppLayout) => {
      return layout.name === layoutName;
    });

    if (layoutToRender) {
      this.dynamicLayoutPortal = new ComponentPortal(layoutToRender.component);
    }
  }
}
