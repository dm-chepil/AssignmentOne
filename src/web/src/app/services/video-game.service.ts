import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PagedResult, VideoGame } from '../models/video-game';

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

  getPage(page: number, pageSize: number): Observable<PagedResult<VideoGame>> {
    const params = new HttpParams()
      .set('page', page)
      .set('pageSize', pageSize);
    return this.http.get<PagedResult<VideoGame>>(this.apiUrl, { params });
  }

  getById(id: string): Observable<VideoGame> {
    return this.http.get<VideoGame>(`${this.apiUrl}/${id}`);
  }

  update(id: string, body: UpdateVideoGameRequest): Observable<VideoGame> {
    return this.http.put<VideoGame>(`${this.apiUrl}/${id}`, body);
  }
}
