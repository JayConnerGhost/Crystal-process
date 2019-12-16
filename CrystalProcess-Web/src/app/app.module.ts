import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { MaterialModule} from './material/material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { FlexLayoutModule } from '@angular/flex-layout';
import {RouterModule} from '@angular/router';
import {RoutingModule} from './routing/routing.module';
import { SecurityComponent } from './security/security.component';
import { JwtHelperService, JWT_OPTIONS  } from '@auth0/angular-jwt';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { FormsModule } from '@angular/forms';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import { WallComponent } from './wall/wall.component';
import { LaneComponent } from './lane/lane.component';
import {AuthInterceptor} from './AuthIntercepter';
// noinspection AngularInvalidImportedOrDeclaredSymbol
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    SecurityComponent,
    LoginComponent,
    RegisterComponent,
    WallComponent,
    LaneComponent,

  ],
  imports: [
    BrowserModule,
    MaterialModule,
    BrowserAnimationsModule,
    NgbModule,
    FlexLayoutModule,
    RouterModule,
    RoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    {
      provide: JWT_OPTIONS, useValue: JWT_OPTIONS
    },
    JwtHelperService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
