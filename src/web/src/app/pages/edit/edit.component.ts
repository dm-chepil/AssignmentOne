import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { VideoGame } from '../../models/video-game';
import { VideoGameService } from '../../services/video-game.service';

@Component({
  selector: 'app-edit',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './edit.component.html'
})
export class EditComponent implements OnInit {
  game: VideoGame | null = null;
  name = '';
  description = '';
  loading = true;
  saving = false;
  loadError = false;
  saveError = false;
  saveSuccess = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private videoGameService: VideoGameService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id') ?? '';
    this.videoGameService.getById(id).subscribe({
      next: (game) => {
        this.game = game;
        this.name = game.name;
        this.description = game.description;
        this.loading = false;
      },
      error: () => {
        this.loadError = true;
        this.loading = false;
      }
    });
  }

  onSubmit(): void {
    if (!this.game) return;
    this.saving = true;
    this.saveError = false;
    this.saveSuccess = false;
    this.videoGameService.update(this.game.id, { name: this.name, description: this.description }).subscribe({
      next: () => {
        this.saving = false;
        this.saveSuccess = true;
      },
      error: () => {
        this.saving = false;
        this.saveError = true;
      }
    });
  }
}
