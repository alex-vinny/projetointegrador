import { Usuario } from 'src/models/usuario';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UsuarioPost } from 'src/models/UsuarioPost';
import { UsuarioService } from 'src/services/usuario.service';

@Component({
  selector: 'app-editar-usuario',
  templateUrl: './editar-usuario.component.html',
  styleUrls: ['./editar-usuario.component.css']
})
export class EditarUsuarioComponent implements OnInit {
  registerForm: FormGroup;
  user: any;
  usuarioGet: Usuario;
  usuario: UsuarioPost;

  constructor(
    private alerts: ToastrService,
    private router: Router,
    private usuarioService: UsuarioService,
  ) { }

  ngOnInit(){
    this.authVerificacao();
  }

  authVerificacao(){
    if(sessionStorage.getItem('auth') != 'true'){
      this.router.navigate(['auth']);
    }
    else {
      this.validation();
      this.getInfoUser();
    }
  }
  
  
  
  validation(){
    this.registerForm = new FormGroup({
      nome: new FormControl('', Validators.required),
      perfil: new FormControl({value: '', disabled: true}),
      // idade: new FormControl('', ),
      // sexo: new FormControl('', ),
      email: new FormControl({value: '', disabled: true}),
      senha: new FormControl('', [Validators.required, Validators.minLength(6)]),
      palavraSecreta: new FormControl('', Validators.required),
      dicaSecreta: new FormControl('')
    })
  }


  getInfoUser(){
    this.user = sessionStorage.getItem('usuario');
    this.usuarioGet = JSON.parse(this.user);

    console.log('UsuarioGet: ', this.usuarioGet.usuario);

    this.registerForm.get('nome')?.setValue(this.usuarioGet.usuario.nome);
    this.registerForm.get('perfil')?.setValue(this.usuarioGet.usuario.perfil);
    this.registerForm.get('email')?.setValue(this.usuarioGet.usuario.email);
    // this.registerForm.get('palavraSecreta')?.setValue(this.usuarioGet.usuario.palavraSecreta);
    // this.registerForm.get('dicaSecreta')?.setValue(this.usuarioGet.usuario.dicaSecreta);


  }








  insertUser(){
    this.usuario = Object.assign({}, this.registerForm.value);
    var pPerfil = this.registerForm.get('perfil')?.value;
    this.usuario.perfil = parseInt(pPerfil);    
    console.log('Usuário: ', this.usuario);
    
    this.usuarioService.postUsuario(this.usuario).subscribe(
      (novoUsuario: any) => {
        //console.log('UsuarioCadastrado', novoUsuario);
        this.alerts.success("Usuário editado com sucesso. Aguarde...",'Editado com sucesso!', {
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
          this.alerts.error("Houve um erro na edição do usuário, tente novamente mais tarde",'Atenção!', {
            positionClass: 'toast-top-full-width',
            timeOut: 8000
          })
        }
        this.registerForm.reset();
      }
    )
  }
  
  back(){
    this.router.navigate(['home']);
  }

}
