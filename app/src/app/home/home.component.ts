import { UtilService } from './../../services/util.service';
import { SessaoService } from './../../services/sessao.service';
import { PalavraService } from './../../services/palavra.service';
import { CategoriaService } from './../../services/categoria.service';
import { Categoria } from './../../models/categoria';
import { UsuarioService } from './../../services/usuario.service';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Usuario } from 'src/models/usuario';
import { of } from 'rxjs';
import { Palavra } from 'src/models/palavra';
import { ToastrService } from 'ngx-toastr';
//import { clg } from './../../../node_modules/crossword-layout-generator/src/layout_generator.js';
declare var require: any;
// declare var clg: any;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  usuario: Usuario;
  user: any;
  qtdPalavras = 8;
  arrPalavras: Palavra[] = [];
  inputJson: any[] = [];
  outputHtml: any;
  outputJson: any;
  arrCategoria: Categoria[] = [];
  loaded = false;
  showModal = true;
  tableResp: any[] = [];
  arrValid: any[] = [];
  showStartGame = true;
  idSessao: number;

  constructor(
    private router: Router,
    private usuarioService: UsuarioService,
    private categoriService: CategoriaService,
    private palavraService: PalavraService,
    private alerts: ToastrService,
    private sessaoService: SessaoService,
    private utilService: UtilService
  ) { }

  ngOnInit() { 
    if(this.usuario == undefined){
      this.user = sessionStorage.getItem('usuario');
      this.usuario = JSON.parse(this.user);
    }
    // console.log('Usuario: ', this.usuario);
    this.authVerificacao();
    // this.getAllCategorias();
  }

  // getAllCategorias(){
  //   this.categoriService.getAllCategorias().subscribe(
  //     (response: Categoria[]) => {
  //       this.arrCategoria = response;
  //       //console.  log('Categorias:', response);
  //       this.showModal = false;
  //     },
  //     error => {
  //       this.showModal = false;
  //       console.log(error);
  //     }
  //   )
  // }

  authVerificacao(){
    if(sessionStorage.getItem('auth') != 'true'){
      this.router.navigate(['auth']);      
    }
    else {
      this.showModal = false;
    }
  }

  navToPalavraCruada(){
    this.router.navigate(['home-palavra-cruzada']);
  }

  navToJogoMemoria(){
    this.router.navigate(['home-jogo-memoria']);
  }

  


  

}
