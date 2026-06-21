import { Routes } from '@angular/router';
import { BrowseComponent } from './pages/browse/browse.component';

export const routes: Routes = [
  { path: '', component: BrowseComponent },
  { path: '**', redirectTo: '' }
];
