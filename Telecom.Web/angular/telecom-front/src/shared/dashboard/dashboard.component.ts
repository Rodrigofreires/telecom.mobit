import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { BaseChartDirective } from 'ng2-charts';
import { RouterModule } from '@angular/router';
import { DashboardService } from '../../app/services/dashboard.service';
import {
  Chart as ChartJS,
  ArcElement,
  CategoryScale,
  LinearScale,
  BarElement,
  Tooltip,
  Legend,
  PieController,
  BarController
} from 'chart.js';

// Registrando os controladores dos gráficos
ChartJS.register(
  ArcElement,
  CategoryScale,
  LinearScale,
  BarElement,
  Tooltip,
  Legend,
  PieController,
  BarController,
);

@Component({
  standalone: true,
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
  imports: [
    RouterModule,
    MatCardModule,
    CommonModule,
    BaseChartDirective,
  ],
})
export class DashboardComponent implements OnInit {

  // Indicadores
  totalFaturas: number = 0;
  valorTotal: number = 0;

  // Dados dos gráficos
  pieChartData: any;
  barChartData: any;

  constructor(private dashboardService: DashboardService) {}

  ngOnInit(): void {
    this.loadIndicadores();
    this.loadStatus();
    this.loadMensal();
  }

  loadIndicadores(): void {
    this.dashboardService.getIndicadores().subscribe(data => {
      this.totalFaturas = data.totalFaturas;
      this.valorTotal = data.valorTotal;
    });
  }

  loadStatus(): void {
    this.dashboardService.getStatus().subscribe(data => {
      this.pieChartData = {
        labels: ['Pagas', 'Pendentes', 'Atrasadas'],
        datasets: [
          {
            data: [data.pagas, data.pendentes, data.atrasadas],
            backgroundColor: ['#4caf50', '#ffeb3b', '#f44336']
          }
        ]
      };
    });
  }

  loadMensal(): void {
    this.dashboardService.getMensal().subscribe(data => {
      this.barChartData = {
        labels: data.meses,
        datasets: [
          {
            label: 'Emitidas',
            data: data.emitidas,
            backgroundColor: '#1976d2'
          },
          {
            label: 'Pagas',
            data: data.pagas,
            backgroundColor: '#4caf50'
          }
        ]
      };
    });
  }
}
