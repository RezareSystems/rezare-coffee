import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api-service';
import { User } from '../models/user';

@Component({
  selector: 'app-coffee-list',
  templateUrl: './coffee-list.component.html',
  styleUrls: ['./coffee-list.component.scss']
})
export class CoffeeListComponent implements OnInit {

  allUsers: User[] = [];

  constructor(private apiService: ApiService) { }

  ngOnInit() {
  }

  getAll() {
    this.apiService.getAllUsers().subscribe(data => {
      this.allUsers = data;
    });
  }

}
