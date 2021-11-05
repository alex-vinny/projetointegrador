import { FormGroup, FormControl, Validators } from '@angular/forms';
import { PalavraService } from './../../services/palavra.service';
import { UsuarioService } from './../../services/usuario.service';
import { Router } from '@angular/router';
import { Usuario } from './../../models/usuario';
import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import { Palavra } from 'src/models/palavra';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  userInfo: Usuario;
  imgPath = '';
  enableAcessar = true;
  msgErroEmail = false;
  msgErroPassword = false;
  registerForm: FormGroup;

  constructor(
    private alerts: ToastrService,
    private router: Router,
    private usuarioService: UsuarioService,
    private palavraService: PalavraService
  ) { }

  ngOnInit() {

    if(sessionStorage.getItem('usuario') != null && sessionStorage.getItem('usuario') != ''){
      this.router.navigate(['home']);
    }

    this.validation();
    

    // this.userInfo = {
    //   id: 0,
    //   email: 'carllos.pires@gmail.com',
    //   nome: 'Carlos Pires Junior',
    //   perfil: {
    //     codigo: 2,
    //     descricao: "Administrador"
    //   }
    // }
    //this.usuarioService.setUser(this.userInfo);
    //this.getUsers();
    
  }

  preLogin(){
    let objEmail = document.getElementById('email') as HTMLInputElement;
    let objPassword = document.getElementById('password') as HTMLInputElement;

    objEmail.value == '' ? this.msgErroEmail = true : this.msgErroEmail = false;
    objPassword.value == '' ? this.msgErroPassword = true : this.msgErroPassword = false;    

    if(objEmail.value != '' && objPassword.value != ''){
      this.enableAcessar = false;
    }
  }

  login(){
    let objEmail = document.getElementById('email') as HTMLInputElement;
    let objPassword = document.getElementById('password') as HTMLInputElement;

    objEmail.value == '' ? this.msgErroEmail = true : this.msgErroEmail = false;
    objPassword.value == '' ? this.msgErroPassword = true : this.msgErroPassword = false;    

    if(objEmail.value == '' || objPassword.value == ''){
      return;
    }
    else {
      this.getUsuario(objEmail.value, objPassword.value);
    } 
    
    
    
    
    
    
    // if(objEmail.value == this.userInfo.email){
    //   sessionStorage.setItem('usuario', JSON.stringify(this.userInfo));
    //   sessionStorage.setItem('auth', 'true');
    //   this.router.navigate(['home']);
    // }
    // else{
    //   this.alerts.warning("Usuário não cadastrado, favor realizar o cadastro do usuário clicando em 'Cadastre-se aqui'",'Atenção!', {
    //     positionClass: 'toast-top-full-width',
    //     timeOut: 6000
    //   })
    // }
  }

  getUsuario(usuario: string, senha: string){
    this.usuarioService.getUsuarioByEmailSenha(usuario, senha).subscribe(
      (response: Usuario) => {
        console.log('Usuarios:', response.usuario);
        sessionStorage.setItem('usuario', JSON.stringify(response));
        sessionStorage.setItem('auth', 'true');
        this.router.navigate(['home']);
        this.usuarioService.setUser(response)
      },
      error => {
        console.log(error);
        sessionStorage.removeItem('auth');
        sessionStorage.removeItem('usuario');
        if(error.status == 404){
          this.alerts.warning("Usuário não cadastrado, favor realizar o cadastro do usuário clicando em 'Cadastre-se aqui'",'Atenção!', {
            positionClass: 'toast-top-full-width',
            timeOut: 8000
          })
        }
        else {
          this.alerts.error("Ocorreu um erro ao tentar localizar o usuário",'Erro!', {
            positionClass: 'toast-top-full-width',
            timeOut: 8000
          })
        }
      }
    )
  }

  forgotPassword(){

  }

  registration(){
    this.router.navigate(['novo-usuario']);
  }

  // getUsers(){
  //   this.usuarioService.getAllUsuarios().subscribe(
  //     (response: Usuario[]) => {
  //       console.log('Usuarios:', response);
  //     },
  //     error => {
  //       console.log(error);

  //     }
  //   )
  // }

  validation(){
    this.registerForm = new FormGroup({
      email: new FormControl('', Validators.required),
      senha: new FormControl('', Validators.required)
    })
  }

}
