import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-novo-usuario',
  templateUrl: './novo-usuario.component.html',
  styleUrls: ['./novo-usuario.component.css']
})
export class NovoUsuarioComponent implements OnInit {

  constructor(
    private alerts: ToastrService
  ) { }

  ngOnInit() {
    this.alerts.error('Erro ao tentar salvar os dados do usuário','Atenção!', {
      positionClass: 'toast-top-full-width',
      timeOut: 2000
    })

    this.alerts.success("Usuário cadastrado com sucesso, vá para página de login e realize o login'",'Sucesso!', {
      positionClass: 'toast-top-full-width',
      timeOut: 2000
    })
  }

}
