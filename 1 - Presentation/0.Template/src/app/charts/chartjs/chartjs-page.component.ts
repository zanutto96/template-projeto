import { Component, PLATFORM_ID, Inject } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { ChartConfiguration } from 'chart.js';

@Component({
  selector: 'app-chartjs-page',
  templateUrl: './chartjs-page.component.html',
  styleUrls: ['./chartjs-page.scss'],
  standalone: false
})
export class ChartsJsPageComponent {
  isBrowser: boolean;

  // Line Chart
  public lineChartData: ChartConfiguration['data'] = {
    datasets: [
      {
        data: [65, 59, 80, 81, 56, 55, 40],
        label: 'Transactions',
        borderColor: '#035388',
        backgroundColor: 'rgba(3,83,136,0.4)',
      },
    ],
    labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July']
  };
  
  public lineChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    plugins: {
      legend: {
        display: true
      }
    }
  };

  // Pie Chart
  public pieChartData: ChartConfiguration['data'] = {
    labels: ['In Store Sales', 'Website Sales', 'Email Sales'],
    datasets: [{
      data: [300, 500, 100],
      backgroundColor: ['#035388', '#40c3f7', '#b3ecff']
    }]
  };
  
  public pieChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    plugins: {
      legend: {
        position: 'right'
      }
    }
  };

  // Bar Chart
  public barChartData: ChartConfiguration['data'] = {
    labels: ['2012', '2013', '2014', '2015', '2016', '2017', '2018', '2019'],
    datasets: [
      {
        data: [165, 159, 180, 181, 156, 155, 140, 200],
        label: 'Product A',
        backgroundColor: '#035388',
        borderColor: '#035388'
      },
      {
        data: [128, 148, 140, 139, 186, 127, 190, 230],
        label: 'Product B',
        backgroundColor: '#40c3f7',
        borderColor: '#40c3f7'
      }
    ]
  };
  
  public barChartOptions: ChartConfiguration['options'] = {
    responsive: true
  };

  // Doughnut
  public doughnutChartData: ChartConfiguration['data'] = {
    labels: ['Mobile', 'Desktop', 'Tablet'],
    datasets: [{
      data: [250, 130, 50],
      backgroundColor: ['#035388', '#40c3f7', '#b3ecff']
    }]
  };
  
  public doughnutChartOptions: ChartConfiguration['options'] = {
    responsive: true
  };

  // Radar
  public radarChartData: ChartConfiguration['data'] = {
    labels: ['Coding', 'Designing', 'Testing', 'Refactoring', 'Meetings', 'Discovery'],
    datasets: [
      {
        data: [80, 59, 70, 30, 46, 15],
        label: 'Admin Template'
      },
      {
        data: [60, 48, 20, 19, 16, 50],
        label: 'Site Template'
      }
    ]
  };
  
  public radarChartOptions: ChartConfiguration['options'] = {
    responsive: true
  };

  constructor(
    @Inject(PLATFORM_ID) private platformId: object
  ) {
    this.isBrowser = isPlatformBrowser(platformId);
  }

}
