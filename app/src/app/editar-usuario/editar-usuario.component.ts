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
  showModal = true;

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
      email: new FormControl({value: '', disabled: true}),
      senha: new FormControl('', [Validators.required, Validators.minLength(6)]),
      palavraSecreta: new FormControl('', Validators.required),
      dicaSecreta: new FormControl('', Validators.required)
    })
  }


  getInfoUser(){
    this.showModal = true;
    this.user = sessionStorage.getItem('usuario');
    this.usuarioGet = JSON.parse(this.user);

    console.log('UsuarioGet: ', this.usuarioGet);

    this.registerForm.get('nome')?.setValue(this.usuarioGet.nome);
    this.registerForm.get('perfil')?.setValue(this.usuarioGet.perfil.codigo.toString());
    this.registerForm.get('email')?.setValue(this.usuarioGet.email);
    // this.registerForm.get('palavraSecreta')?.setValue(this.usuarioGet.palavraSecreta);
    // this.registerForm.get('dicaSecreta')?.setValue(this.usuarioGet.dicaSecreta);
    this.showModal = false;
  }

  updateUser(){
    this.showModal = true;
    var userUpdate = {
      nome: this.registerForm.get('nome')?.value,
      senha: this.registerForm.get('senha')?.value,
      dicaSecreta: this.registerForm.get('dicaSecreta')?.value,
      palavraSecreta: this.registerForm.get('palavraSecreta')?.value,
    }
    var email = this.registerForm.get('email')?.value;
   
    console.log('Usuário: ', userUpdate);
    
    this.usuarioService.putUsuario(email, userUpdate).subscribe(
      (novoUsuario: any) => {
        //console.log('UsuarioCadastrado', novoUsuario);
        this.showModal = false;
        this.alerts.success("Usuário editado com sucesso. Aguarde...",'Editado com sucesso!', {
          positionClass: 'toast-top-full-width',
          timeOut: 8000
        })

        this.registerForm.reset();

        setTimeout(() => {
          this.router.navigate(['login'])
        }, 5000);

      }, error => {
        console.log('Error: ', error);
        this.showModal = false;
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
