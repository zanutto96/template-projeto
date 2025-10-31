import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { CoreModule } from './core/core.module';
import { HttpClientModule } from '@angular/common/http';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// Angular Material
import { MatNativeDateModule } from '@angular/material/core';

import { AuthenticationModule } from './authentication/authentication.module';

// echarts - lib: https://github.com/xieziyu/ngx-echarts
import { NgxEchartsModule } from 'ngx-echarts';
import * as echarts from 'echarts/core';
import { LineChart, BarChart, PieChart, GaugeChart } from 'echarts/charts';
import {
  TitleComponent,
  TooltipComponent,
  GridComponent,
  LegendComponent,
  ToolboxComponent,
  DatasetComponent
} from 'echarts/components';
import { CanvasRenderer } from 'echarts/renderers';

echarts.use([
  TitleComponent,
  TooltipComponent,
  GridComponent,
  LegendComponent,
  ToolboxComponent,
  DatasetComponent,
  LineChart,
  BarChart,
  PieChart,
  GaugeChart,
  CanvasRenderer
]);

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    CoreModule,
    AuthenticationModule,
    BrowserAnimationsModule,
    MatNativeDateModule,
    NgxEchartsModule.forRoot({
      echarts
    })
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
