import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import {HttpClient} from '@angular/common/http';
import {ConfigService} from './config.service';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = `${this.config.api}/auth/`;
  private decodedToken: any;
   constructor(
     public jwtHelper: JwtHelperService,
     private http: HttpClient,
     private config: ConfigService
     ) { }

  public isAuthenticated(): boolean {
    const token = localStorage.getItem('token');
    // Check whether the token is expired and return
    // true or false
    return !this.jwtHelper.isTokenExpired(token);
  }

  register(model: any) {
    return this.http.post(`${this.baseUrl}register`, model);
  }

  login(model: any) {
    return this.http.post(`${this.baseUrl}login`, model)
      .pipe(
      map((response: any) => {
        const user = response;
        if(user) {
          localStorage.setItem('token', user.token);
          localStorage.setItem('username', user.username);
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          console.log(this.decodedToken);
        }
      }));
  }
}
