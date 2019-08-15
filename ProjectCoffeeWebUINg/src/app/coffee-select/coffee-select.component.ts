import { Component, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { OAuthParseService } from '../services/oauth-parse-service';
import { ApiService } from '../services/api-service';

@Component({
  selector: 'app-coffee-select',
  templateUrl: './coffee-select.component.html',
  styleUrls: ['./coffee-select.component.scss']
})
export class CoffeeSelectComponent implements OnInit {

  coffeeList = [
    { name: 'No Drink', code: 'N' },
    { name: 'Hot Chocolate', code: 'HC' },
    { name: 'Flat White', code: 'FW' },
    { name: 'Latte', code: 'LT' },
    { name: 'Mocha', code: 'MH' },
    { name: 'Vienna', code: 'AF' },
    { name: 'Macciato', code: 'AM' },
    { name: 'Short Black', code: 'SB' },
    { name: 'Long Black', code: 'LB' },
    { name: 'Chai Latte', code: 'CL' },
    { name: 'Cappuccino', code: 'CP' }
  ];

  cupSize = [
    { name: 'Regular', code: 'R' },
    { name: 'Large', code: 'L' }
  ];

  milkType = [
    { name: 'Normal', code: 'T' },
    { name: 'Trim', code: 'T' },
    { name: 'Soy', code: 'S' }
    
  ];

  extras = [
    { name: 'Extra Shot', code: 'ES', count: 0 },
    { name: 'Vanilla', code: 'V', count: 0 },
    { name: 'Caramel', code: 'CA', count: 0 },
    { name: 'Hazelnut', code: 'CH', count: 0 },
    { name: 'Honey, Lemon & Ginger', code: 'HLG', count: 0 },
    { name: 'Sugar', code: 'S', count: 0 }
  ]

  selectedDrink;

  constructor(private oauthService: OAuthService,
    private parseService: OAuthParseService,
    private apiService: ApiService) { }

  ngOnInit() {
    var userId = this.parseService.getSubject();
    this.apiService.getUser(userId).subscribe(data => {

    });
   }

  name = this.parseService.getName();

  public incrementExtra(code) {
    for(var i = 0; i < this.extras.length; i++) {
      if(this.extras[i].code != code) continue;
      this.extras[i].count += 1;
    }
  }

  public decrementExtra(code) {
    for(var i = 0; i < this.extras.length; i++) {
      if(this.extras[i].code != code) continue;
      if(this.extras[i].count == 0) continue;
      this.extras[i].count -= 1;
    }
  }

}
