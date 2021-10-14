import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { Usuario } from 'src/models/usuario';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  url = 'https://projeto-integrador-turma-002.herokuapp.com/api/Alunos';

  constructor(
    private http: HttpClient,
  ) { }

  getUsuarioByEmail(email: string): Observable<Usuario>{

    return this.http.get<Usuario>(this.url + '/' + email);
  }

  getUsuarios(): Observable<Usuario[]>{
    const headers = new Headers();
    headers.append('Access-Control-Allow-Origin', '*');
    headers.set('Access-Control-Allow-Origin','true');
    headers.append('Accept','*/*');
    headers.append('Accept-Encoding','gzip, deflate, br');
    headers.append('Connection','keep-alive');

    return this.http.get<Usuario[]>(this.url);
  }

}
