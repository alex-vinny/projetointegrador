import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { Usuario } from 'src/models/usuario';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  url = 'https://projeto-integrador-turma-002.herokuapp.com/api';

  constructor(
    private http: HttpClient,
  ) { }

  getUsuarioByEmail(email: string): Observable<Usuario[]>{
    return this.http.get<Usuario[]>(this.url + '/Alunos/' + email);
  }

}
