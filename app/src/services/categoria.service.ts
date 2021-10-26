import { environment } from './../environments/environment';
import { Categoria } from './../models/categoria';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoriaService {
  url = environment.urlWebService + '/api/Categorias'
  constructor(
    private http: HttpClient
  ) { }

  getAllCategorias(): Observable<Categoria[]>{
    return this.http.get<Categoria[]>(this.url);
  }
}
