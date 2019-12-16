import {Component, Input, OnInit} from '@angular/core';
import {ApiService} from '../services/api.service';

@Component({
  selector: 'app-wall',
  templateUrl: './wall.component.html',
  styleUrls: ['./wall.component.css']
})
export class WallComponent implements OnInit {
  stages: stage[];

  constructor(
    private apiService: ApiService
  ) { }

  ngOnInit() {
    this.loadStages();
  }

  private loadStages() {
    this.apiService.getStages().subscribe(rez => {
      this.stages = rez;
    });
  }

}
