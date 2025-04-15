import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { BaseChartDirective } from 'ng2-charts';

// Importando os elementos e controladores necessários do Chart.js
import { Chart as ChartJS, ArcElement, CategoryScale, LinearScale, BarElement, Tooltip, Legend, PieController, BarController } from 'chart.js';

// Registrando os controladores necessários para os gráficos
ChartJS.register(ArcElement, CategoryScale, LinearScale, BarElement, Tooltip, Legend, PieController, BarController);

@Component({
  standalone: true,
  selector: 'app-dashboard',
  imports: [
    MatCardModule,
    CommonModule,
    BaseChartDirective,
  ],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent {
  // Dados fictícios temporários - pode substituir pela API depois
  faturaStatus = {
    paga: 45,
    pendente: 25,
    atrasada: 15
  };

  faturasMensais = {
    meses: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
    emitidas: [100, 120, 90, 130, 110, 150, 180, 170, 160, 190, 200, 220],
    pagas: [90, 100, 80, 120, 100, 130, 160, 150, 140, 180, 190, 210]
  };

  // Cards
  totalFaturas = 2000;
  valorTotal = 450000;

  // Gráfico de pizza (Status das faturas)
  pieChartData = {
    labels: ['Pagas', 'Pendentes', 'Atrasadas'],
    datasets: [
      {
        data: [45, 25, 15],
        backgroundColor: ['#4caf50', '#ffeb3b', '#f44336']
      }
    ]
  };

  // Gráfico de barras (Emitidas vs Pagas)
  barChartData = {
    labels: this.faturasMensais.meses,
    datasets: [
      {
        label: 'Emitidas',
        data: this.faturasMensais.emitidas,
        backgroundColor: '#1976d2'
      },
      {
        label: 'Pagas',
        data: this.faturasMensais.pagas,
        backgroundColor: '#4caf50'
      }
    ]
  };
}
