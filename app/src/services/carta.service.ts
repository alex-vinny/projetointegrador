import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Carta } from 'src/models/carta';

@Injectable({
  providedIn: 'root'
})
export class CartaService {
  url = environment.urlWebService + '/api/Imagens'
  constructor(
    private http: HttpClient
  ) { }

  getAllCartas(): Observable<Carta[]>{
    return this.http.get<Carta[]>(this.url);
  }

  getAllCartasByCategoriaQtd(categoria: string, qtd: number): Observable<Carta[]>{
    var urlFinal = '';
    if(categoria == ''){
      urlFinal = this.url + '?quantidade=' + qtd;
    }
    else {
      urlFinal = this.url + '?categoria=' + categoria +'&quantidade=' + qtd;
    }
    // return this.http.get<Carta[]>(this.url + '/' + categoria + '/' + qtd);
    return this.http.get<Carta[]>(urlFinal);
  }
}
