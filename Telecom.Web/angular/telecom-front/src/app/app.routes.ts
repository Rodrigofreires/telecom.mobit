import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from '../pages/home/home.component';
import { PainelContratosComponent } from '../pages/painel-contratos/painel-contratos.component';
import { PainelFaturasComponent } from '../pages/painel-faturas/painel-faturas.component';
import { PainelOperadorasComponent } from '../pages/painel-operadoras/painel-operadoras.component';
import { DashboardComponent } from '../shared/dashboard/dashboard.component';


export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'contratos', component: PainelContratosComponent },
    { path: 'faturas', component: PainelFaturasComponent },
    { path: 'operadoras', component: PainelOperadorasComponent },
    { path: '**', redirectTo: '' },
    { path: 'dashboard', component: DashboardComponent}
    
  ];