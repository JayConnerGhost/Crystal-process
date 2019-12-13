import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {
  api: any = 'http://localhost:63891/api';

  constructor() { }
}
