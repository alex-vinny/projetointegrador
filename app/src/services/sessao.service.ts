import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SessaoService {
  url = environment.urlWebService + '/api/Sessoes'
  constructor(
    private http: HttpClient,
  ) { }

  postSessao(email: string, dtInicio: any){
    return this.http.post(this.url + '/' + email, dtInicio);
  }

  putSessao(email: string, sessao: any){
    return this.http.put(this.url + '/' + email, sessao);
  }
}
