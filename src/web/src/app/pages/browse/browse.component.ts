import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { VideoGame } from '../../models/video-game';
import { VideoGameService } from '../../services/video-game.service';

@Component({
  selector: 'app-browse',
  standalone: true,
  imports: [CommonModule, RouterModule, NgbPaginationModule],
  templateUrl: './browse.component.html'
})
export class BrowseComponent implements OnInit {
  games: VideoGame[] = [];
  pagedGames: VideoGame[] = [];
  currentPage = 1;
  readonly pageSize = 10;
  loading = true;
  error = false;

  constructor(private videoGameService: VideoGameService) {}

  ngOnInit(): void {
    this.videoGameService.getAll().subscribe({
      next: (games) => {
        this.games = games;
        this.loading = false;
        this.updatePage();
      },
      error: () => {
        this.error = true;
        this.loading = false;
      }
    });
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.updatePage();
  }

  private updatePage(): void {
    const start = (this.currentPage - 1) * this.pageSize;
    this.pagedGames = this.games.slice(start, start + this.pageSize);
  }
}
