import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Pontuacao } from 'src/models/pontuacao';

@Injectable({
  providedIn: 'root'
})
export class PontuacaoService {
  url = environment.urlWebService + '/api/Pontuacoes';
  pontuacao: Pontuacao;
  constructor(
    private http: HttpClient
  ) { }

  getIdPontuacao (infoPontuacao: Pontuacao): Observable<any>{
    return this.http.post(this.url, infoPontuacao);
  }

  putPontuacao(id: number, pontuacao: any){
    return this.http.put(this.url + '/' + id, pontuacao);
  }

}
