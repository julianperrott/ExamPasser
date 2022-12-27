import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ListAllModel } from './GetSingleModel';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private readonly baseUrl: string;

  constructor(
    private readonly http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.baseUrl = baseUrl;
  }

  public getAll<T>(url: string): Observable<T[]> {
    return this.http.get<T[]>(this.formatEndpoint(url));
  }

  public listAll<T>(url: string): Observable<ListAllModel<T>> {
    return this.http.get<ListAllModel<T>>(this.formatEndpoint(url));
  }

  public getMedia<T>(url: string): Observable<Blob> {
    return this.http.get(this.formatEndpoint(url), { responseType: 'blob' });
  }

  public post<T>(url: string, item: T): Observable<any> {
    return this.http.post<T>(this.formatEndpoint(url), item);
  }

  public put<T>(url: string, item: T): Observable<any> {
    return this.http.put<T>(this.formatEndpoint(url), item);
  }

  public delete(url: string): Observable<any> {
    return this.http.delete(this.formatEndpoint(url));
  }

  private formatEndpoint(url: string): string {
    return `${this.baseUrl}${url}`;
  }

  public buildQueryString(parameters: any): string {
    return Object.keys(parameters)
      .filter((key) => parameters[key])
      .map((key) => `${key}=${parameters[key]}`)
      .join('&');
  }
}
