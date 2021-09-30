import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(
    private alerts: ToastrService
  ) { }

  ngOnInit() {
    this.alerts.error('Erro ao tentar identificar o usuário','Erro!', {
      positionClass: 'toast-top-full-width',
      timeOut: 6000
    })

    this.alerts.warning("Usuário não cadastrado,favor realizar o cadastro do usuário clicando em 'Cadastre-se aqui'",'Atenção!', {
      positionClass: 'toast-top-full-width',
      timeOut: 6000
    })
  }

}
