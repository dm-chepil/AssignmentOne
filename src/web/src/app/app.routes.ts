import { Routes } from '@angular/router';
import { BrowseComponent } from './pages/browse/browse.component';
import { EditComponent } from './pages/edit/edit.component';

export const routes: Routes = [
  { path: '', component: BrowseComponent },
  { path: 'video-games/:id/edit', component: EditComponent },
  { path: '**', redirectTo: '' }
];
