import { Routes } from '@angular/router';
import { Home } from './components/home/home';
import { Endereco } from './components/endereco/endereco';


export const routes: Routes = [
  { path: '', component: Home },
  {path: 'endereco', component: Endereco}
];