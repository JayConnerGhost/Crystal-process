import { Component, OnInit } from '@angular/core';
import {AlertifyService} from '../services/alertify.service';
import {AuthService} from '../services/auth.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model: any = {};
  constructor(
    private alertify: AlertifyService,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
        this.alertify.success('successfully logged in ');
        this.router.navigate(['/home']);
      },
      error => {
        this.alertify.error('failed to login');
      });
  }

}
