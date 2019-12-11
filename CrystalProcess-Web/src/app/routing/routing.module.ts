import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from '../home/home.component';
import {SecurityComponent} from '../security/security.component';
import {AuthGuardService} from '../services/auth-guard.service';

const appRoutes: Routes = [
  { path: '',
    component: HomeComponent,
    canActivate: [AuthGuardService]
  },
  { path: 'home',
    component: HomeComponent,
    canActivate: [AuthGuardService] },
  { path: 'security', component: SecurityComponent}

  ];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: true } // <-- debugging purposes only
    )
  ]
})
export class RoutingModule { }
