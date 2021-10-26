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
  imgPath = ''
  constructor(
    private alerts: ToastrService,
    private router: Router,
    private usuarioService: UsuarioService,
    private palavraService: PalavraService
  ) { }

  ngOnInit() {
    this.imgPath = '/src/img/reflectare.png'
    this.userInfo = {
      id: 0,
      email: 'carllos.pires@gmail.com',
      nome: 'Carlos Pires Junior',
      senha: '123456',
      perfil: 'Professor',
      idade: 25,
      sexo: 'Masculino',
      serieEscolar: ''
    }

    this.usuarioService.setUser(this.userInfo);


    // this.alerts.error('Erro ao tentar identificar o usuário','Erro!', {
    //   positionClass: 'toast-top-full-width',
    //   timeOut: 6000
    // })

    // this.alerts.warning("Usuário não cadastrado,favor realizar o cadastro do usuário clicando em 'Cadastre-se aqui'",'Atenção!', {
    //   positionClass: 'toast-top-full-width',
    //   timeOut: 6000
    // })

    this.getUsers();
    
  }

  getUsers(){
    this.usuarioService.getAllUsuarios().subscribe(
      (response: Usuario[]) => {
        console.log('Usuarios:', response);
      },
      error => {
        console.log(error)
      }
    )
  }



  login(){
    let objEmail = document.getElementById('email') as HTMLInputElement

    if(objEmail.value == this.userInfo.email){
      sessionStorage.setItem('usuario', JSON.stringify(this.userInfo));
      sessionStorage.setItem('auth', 'true');
      this.router.navigate(['home']);
    }
    else{
      this.alerts.warning("Usuário não cadastrado, favor realizar o cadastro do usuário clicando em 'Cadastre-se aqui'",'Atenção!', {
      positionClass: 'toast-top-full-width',
      timeOut: 6000
    })
    }
  }

}
