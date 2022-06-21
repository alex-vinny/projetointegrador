import { UsuarioPost } from './../../models/UsuarioPost';
import { Usuario } from 'src/models/usuario';
import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UsuarioService } from 'src/services/usuario.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { i18nMetaToJSDoc } from '@angular/compiler/src/render3/view/i18n/meta';

@Component({
  selector: 'app-novo-usuario',
  templateUrl: './novo-usuario.component.html',
  styleUrls: ['./novo-usuario.component.css']
})
export class NovoUsuarioComponent implements OnInit {
  registerForm: FormGroup;
  usuario: UsuarioPost;
  
  
  constructor(
    private alerts: ToastrService,
    private router: Router,
    private usuarioService: UsuarioService,
  ) { }

  ngOnInit() {
    this.validation();
  }

  validation(){
    this.registerForm = new FormGroup({
      nome: new FormControl('', Validators.required),
      perfil: new FormControl('', Validators.required),
      // idade: new FormControl('', ),
      // sexo: new FormControl('', ),
      email: new FormControl('', Validators.required),
      senha: new FormControl('', [Validators.required, Validators.minLength(6)]),
      palavraSecreta: new FormControl('', Validators.required),
      dicaSecreta: new FormControl('')
    })
  }

  insertUser(){
    this.usuario = Object.assign({}, this.registerForm.value);
    var pPerfil = this.registerForm.get('perfil')?.value;
    this.usuario.perfil = parseInt(pPerfil);
    
    // console.log('Usuário: ', this.usuario);
    
    this.usuarioService.postUsuario(this.usuario).subscribe(
      (novoUsuario: any) => {
        //console.log('UsuarioCadastrado', novoUsuario);
        this.alerts.success("Usuário cadastrado com sucesso. Vamos te direcionar para a tela de login, aguarde...",'Cadastro com sucesso!', {
          positionClass: 'toast-top-full-width',
          timeOut: 8000
        })

        setTimeout(() => {
          this.router.navigate(['login'])
        }, 5000);
        this.registerForm.reset();

      }, error => {
        console.log('Error: ', error);
        if(error.status == 422){
          this.alerts.warning("Já há um usuário cadastrado com o email informado",'Atenção!', {
            positionClass: 'toast-top-full-width',
            timeOut: 8000
          })
        }
        else {
          this.alerts.error("Ocorreu um erro ao tentar cadastrar o usuário, tente novamente mais tarde",'Atenção!', {
            positionClass: 'toast-top-full-width',
            timeOut: 8000
          })
        }
        this.registerForm.reset();
      }
    )
  }
  
  back(){
    this.router.navigate(['login']);
  }

}
