import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CoffeeSelectComponent } from "src/app/coffee-select/coffee-select.component";
import { CoffeeListComponent } from "src/app/coffee-list/coffee-list.component";


const routes: Routes = [
  { path: '', component: CoffeeSelectComponent },
  { path: 'list', component: CoffeeListComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
