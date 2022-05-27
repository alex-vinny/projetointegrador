import { Router } from '@angular/router';
import { UsuarioService } from './../../services/usuario.service';
import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Usuario } from 'src/models/usuario';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  userLogado: Usuario
  user: any;
  // @Input() showBtnCadastrarPalavra: boolean
  @Input() showBtnCadastrarPalavras: boolean = true;

  constructor(
    private alerts: ToastrService,
    private usuarioService: UsuarioService,
    private router: Router
  ) { }

  ngOnInit() {
    // this.alerts.success('Teste msg','Titulo',     {
    //   positionClass: 'toast-top-full-width',
    //   timeOut: 2000
    // })

    if(this.userLogado == undefined){
      this.user = sessionStorage.getItem('usuario');
      this.userLogado = JSON.parse(this.user);
    }

    console.log('Input: ', this.showBtnCadastrarPalavras)
  }

  logout(){
    sessionStorage.clear();
    this.router.navigate(['login']);
  }

  editaPerfil(){
    this.router.navigate(['editar-usuario']);
  }


}
