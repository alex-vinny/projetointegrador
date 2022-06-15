import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Carta } from 'src/models/Carta';

@Injectable({
  providedIn: 'root'
})
export class CartaService {
  url = environment.urlWebService + '/api/Categorias'
  constructor(
    private http: HttpClient
  ) { }

  getAllCartas(): Observable<Carta[]>{
    return this.http.get<Carta[]>(this.url);
  }

  getAllCartasByCategoriaQtd(categoria: string, qtd: number): Observable<Carta[]>{
    return this.http.get<Carta[]>(this.url + '/' + categoria + '/' + qtd);
  }
}
