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
  totalCount = 0;
  currentPage = 1;
  readonly pageSize = 5;
  loading = true;
  error = false;

  constructor(private videoGameService: VideoGameService) {}

  ngOnInit(): void {
    this.loadPage();
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.loadPage();
  }

  private loadPage(): void {
    this.loading = true;
    this.videoGameService.getPage(this.currentPage, this.pageSize).subscribe({
      next: (result) => {
        this.games = result.items;
        this.totalCount = result.totalCount;
        this.loading = false;
      },
      error: () => {
        this.error = true;
        this.loading = false;
      }
    });
  }
}
