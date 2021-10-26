import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Palavra } from 'src/models/palavra';

@Injectable({
  providedIn: 'root'
})
export class PalavraService {
  url = environment.urlWebService + '/api/Palavras'
  constructor(
    private http: HttpClient,
  ) { }


  getAllPalavras(): Observable<Palavra[]>{
    return this.http.get<Palavra[]>(this.url);
  }

  postPalavra(palavra: Palavra){
    return this.http.post(this.url, palavra);
  }

  putPalavra(palavra: Palavra){
    return this.http.put(this.url, palavra);
  }

}
