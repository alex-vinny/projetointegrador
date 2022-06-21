import { UsuarioService } from './../services/usuario.service';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms'


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { CadastroComponent } from './cadastro/cadastro.component';
import { HomeComponent } from './home/home.component';
import { NovoUsuarioComponent } from './novo-usuario/novo-usuario.component';
import { AuthComponent } from './auth/auth.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { ReactiveFormsModule } from '@angular/forms';
import { EditarUsuarioComponent } from './editar-usuario/editar-usuario.component';
import { ModalComponent } from './modal/modal.component';
import { RankingComponent } from './ranking/ranking.component';
import { EsqueceuSenhaComponent } from './esqueceu-senha/esqueceu-senha.component';
import { HomeJogoMemoriaComponent } from './home-jogo-memoria/home-jogo-memoria.component';
import { HomePalavraCruzadaComponent } from './home-palavra-cruzada/home-palavra-cruzada.component';

@NgModule({
  declarations: [											
      AppComponent,
      LoginComponent,
      CadastroComponent,
      HomeComponent,
      NovoUsuarioComponent,
      AuthComponent,
      NavBarComponent,
      EditarUsuarioComponent,
      ModalComponent,
      RankingComponent,
      EsqueceuSenhaComponent,
      HomeJogoMemoriaComponent,
      HomePalavraCruzadaComponent
   ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      timeOut: 10000,
      positionClass: 'toast-top-full-width',
      progressBar: true
    }),
    TooltipModule.forRoot(),
    ReactiveFormsModule,
    FormsModule

  ],
  providers: [UsuarioService],
  bootstrap: [AppComponent]
  
})
export class AppModule { }
