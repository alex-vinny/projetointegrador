import { UsuarioService } from './../../services/usuario.service';
import { Component, OnInit } from '@angular/core';
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

  constructor(
    private alerts: ToastrService,
    private usuarioService: UsuarioService
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
  }

}
