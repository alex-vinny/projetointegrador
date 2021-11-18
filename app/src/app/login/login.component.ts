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
  showModal = false;

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

  login(){
    this.showModal = true;
    let objEmail = document.getElementById('email') as HTMLInputElement;
    let objPassword = document.getElementById('password') as HTMLInputElement; 

    this.usuarioService.getUsuarioByEmailSenha(objEmail.value, objPassword.value).subscribe(
      (response: Usuario) => {
        console.log('Usuarios:', response);
        sessionStorage.setItem('usuario', JSON.stringify(response));
        sessionStorage.setItem('auth', 'true');
        this.router.navigate(['home']);
        this.usuarioService.setUser(response)
      },
      error => {
        this.showModal = false;
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
