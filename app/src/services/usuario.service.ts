import { Usuario } from 'src/models/usuario';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { UsuarioPost } from 'src/models/UsuarioPost';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  url = environment.urlWebService + '/api/Usuarios';
  userLogado: Usuario;
  
  constructor(
    private http: HttpClient,
  ) { }

  getAllUsuarios(): Observable<Usuario[]>{
    return this.http.get<Usuario[]>(this.url);
  }

  getUsuarioByEmailSenha(email: string, senha: string): Observable<Usuario>{
    return this.http.get<Usuario>(this.url + '/' + email + '/' + senha);
  }

  postUsuario(usuario: UsuarioPost){
    return this.http.post(this.url, usuario);
  }

  putUsuario(usuario: Usuario){
    return this.http.put(this.url, usuario);
  }

  setUser(ddsUsuario: Usuario){
    this.userLogado = ddsUsuario;
  }

  getUser(){
    return this.userLogado;
  }


}
