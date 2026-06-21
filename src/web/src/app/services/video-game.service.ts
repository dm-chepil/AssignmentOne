import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { VideoGame } from '../models/video-game';

export interface UpdateVideoGameRequest {
  name: string;
  description: string;
}

@Injectable({
  providedIn: 'root'
})
export class VideoGameService {
  private readonly apiUrl = '/api/video-games';

  constructor(private http: HttpClient) {}

  getAll(): Observable<VideoGame[]> {
    return this.http.get<VideoGame[]>(this.apiUrl);
  }

  getById(id: string): Observable<VideoGame> {
    return this.http.get<VideoGame>(`${this.apiUrl}/${id}`);
  }

  update(id: string, body: UpdateVideoGameRequest): Observable<VideoGame> {
    return this.http.put<VideoGame>(`${this.apiUrl}/${id}`, body);
  }
}
