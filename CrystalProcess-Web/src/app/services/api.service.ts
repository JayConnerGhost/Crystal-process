import { Injectable } from '@angular/core';
import {ConfigService} from './config.service';
import {HttpClient} from '@angular/common/http';
import {config} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(
    private configService: ConfigService,
    private httpClient: HttpClient
  ) { }

  getStages() {
    return this.httpClient.get<stage[]>(`${this.configService.api}/stages`);
  }
}
