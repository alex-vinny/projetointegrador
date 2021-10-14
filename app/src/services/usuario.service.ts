import { Usuario } from 'src/models/usuario';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  userLogado: Usuario;
  constructor() { }

  setUser(ddsUsuario: Usuario){
    this.userLogado = ddsUsuario;
  }

  getUser(){
    return this.userLogado;
  }
}
