import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { CoffeeSelectComponent } from "src/app/coffee-select/coffee-select.component";


const routes: Routes = [
  { path: 'login', component: LoginComponent },
   { path: 'select', component: CoffeeSelectComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
