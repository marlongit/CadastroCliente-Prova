import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Cliente } from '../model/cliente';

@Injectable({
  providedIn: 'root'
})
export class ClienteServiceService {

  constructor(private http: HttpClient) { }

  url = 'https://localhost:44357/api/cliente';

  getAll(): Observable<Cliente[]> {
    return this.http.get<Cliente[]>(this.url).pipe();
  }

  get(id) {
    return this.http.get<Cliente>(this.url + '/' + id).pipe();
  }

  post(cliente): Observable<Cliente> {
    return this.http.post<Cliente>(this.url, cliente).pipe();
  }

  delete(cliente: Cliente) {
    return this.http.delete(this.url + '/' + cliente.id).pipe();
  }

  put(cliente) {
    return this.http.put(this.url, cliente).pipe();
  }
}
