export interface VideoGame {
  id: string;
  name: string;
  description: string;
  createDate: string;
}

export interface PagedResult<T> {
  items: T[];
  totalCount: number;
  page: number;
  pageSize: number;
  totalPages: number;
}
