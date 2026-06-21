import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { VideoGame } from '../models/video-game';

@Injectable({
  providedIn: 'root'
})
export class VideoGameService {
  private readonly apiUrl = '/api/video-games';

  constructor(private http: HttpClient) {}

  getAll(): Observable<VideoGame[]> {
    return this.http.get<VideoGame[]>(this.apiUrl);
  }
}
