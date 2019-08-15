import { Component, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';

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
    { name: 'Affogato', code: 'AF' },
    { name: 'Americano', code: 'AM' },
    { name: 'Short Black / Espresso', code: 'SB' },
    { name: 'Long Black', code: 'LB' },
    { name: 'Chai Latte', code: 'CL' }
  ];

  cupSize = [
    { name: 'Regular', code: 'R' },
    { name: 'Large', code: 'L' }
  ];

  milkType = [
    { name: 'Trim', code: 'T' },
    { name: 'Lite Blue', code: 'LB' },
    { name: 'Full Cream', code: 'FC' },
    { name: 'Soy', code: 'S' },
    { name: 'Almond', code: 'A' },
    { name: 'Rice', code: 'R' }
  ];

  extras = [
    { name: 'Extra Shot', code: 'ES', count: 0 },
    { name: 'Vanilla Shot', code: 'V', count: 0 },
    { name: 'Caramel Shot', code: 'CA', count: 0 },
    { name: 'Chocolate Shot', code: 'CH', count: 0 },
  ]

  selectedDrink;

  constructor(private oauthService: OAuthService) { }

  ngOnInit() { }

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
