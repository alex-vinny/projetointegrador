import { PontuacaoService } from './../../services/pontuacao.service';
import { Pontuacao } from './../../models/pontuacao';
import { UtilService } from './../../services/util.service';
import { SessaoService } from './../../services/sessao.service';
import { PalavraService } from './../../services/palavra.service';
import { CategoriaService } from './../../services/categoria.service';
import { Categoria } from './../../models/categoria';
import { UsuarioService } from './../../services/usuario.service';
import { Router } from '@angular/router';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Usuario } from 'src/models/usuario';
import { of } from 'rxjs';
import { Palavra } from 'src/models/palavra';
import { ToastrService } from 'ngx-toastr';
//import { clg } from './../../../node_modules/crossword-layout-generator/src/layout_generator.js';
declare var require: any;
// declare var clg: any;    

@Component({
  selector: 'app-home-palavra-cruzada',
  templateUrl: './home-palavra-cruzada.component.html',
  styleUrls: ['./home-palavra-cruzada.component.css']
})
export class HomePalavraCruzadaComponent implements OnInit {

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
  showBtnCadastrarPalav = true;
  pontuacaoJogo: Pontuacao;
  idJogo: number

  constructor(
    private router: Router,
    private usuarioService: UsuarioService,
    private categoriService: CategoriaService,
    private palavraService: PalavraService,
    private alerts: ToastrService,
    private sessaoService: SessaoService,
    private utilService: UtilService,
    private pontuacaoService: PontuacaoService
  ) { }

  ngOnInit() { 
    if(this.usuario == undefined){
      this.user = sessionStorage.getItem('usuario');
      this.usuario = JSON.parse(this.user);
    }
    // console.log('Usuario: ', this.usuario);
    this.authVerificacao();
    this.getAllCategorias();
    this.addToTest();
  }

  @Output() testChanged: EventEmitter<boolean> = new EventEmitter();

  addToTest() {
    //insere no array
    this.testChanged.emit(true);
  }

  getAllCategorias(){
    this.categoriService.getAllCategorias().subscribe(
      (response: Categoria[]) => {
        this.arrCategoria = response;
        //console.  log('Categorias:', response);
        this.showModal = false;
      },
      error => {
        this.showModal = false;
        this.alerts.error("Houve um erro ao pegar as categorias do jogo",'Atenção', {
          positionClass: 'toast-top-full-width',
          timeOut: 8000
        })
      }
    )
  }

  authVerificacao(){
    if(sessionStorage.getItem('auth') != 'true'){
      this.router.navigate(['auth']);
    }
    else {
    }
  }

  clearByChangeCart(){
    this.loaded = false;
    var objHtml = document.getElementById('table-cross') as HTMLElement
    objHtml.innerHTML = '';
    this.arrPalavras = [];
    this.inputJson = [];
    this.showStartGame = true;
  }

  iniciarJogo(){
    this.pontuacaoJogo = {
      qtdItems: 0,
      qtdPontos: 0,
      qtdErros: 0,
      tipoJogoId: 1,
      tipoJogo: "REFLECTERE",
      email: this.usuario.email,
      usuario: this.usuario.id,
      dataJogo: new Date()
    }
    this.pontuacaoService.getIdPontuacao(this.pontuacaoJogo).subscribe(
      (response: any) => {
        this.idJogo = response.id;
        this.getPalavras();
    },error => {
      this.alerts.error("Houve um erro ao iniciar o jogo",'Atenção', {
        positionClass: 'toast-top-full-width',
        timeOut: 8000
      })      
    })
  }

  getPalavras(){
    this.showStartGame = false;
    this.showModal = true;
    this.arrPalavras = [];
    this.loaded = false;
    this.inputJson = [];    
    
    let objPalavra = document.getElementById('categoria') as HTMLInputElement

    if(objPalavra.value != ''){
      this.getPalavraByCategoriaQtd(objPalavra.value, this.qtdPalavras);
    }
    else {
      this.getPalavrasByQtd(this.qtdPalavras);
    }    
    this.postSessao(this.usuario.email);
  } 

  postSessao(email: string){    

    var dt = new Date();
    var dtIncio = {
      "dataInicio": this.utilService.toISOLocalString(dt)
    }
    //2021-11-12T19:00:44.217Z
    this.sessaoService.postSessao(email, dtIncio).subscribe(
      (response: any) => {
        // console.log('Sessão:', response);
      },
      error => {
        this.showModal = false;
        this.alerts.error("Houve um erro ao iniciar a sessao do jogo",'Atenção', {
          positionClass: 'toast-top-full-width',
          timeOut: 8000
        })
      }
    )
  }


  getPalavrasByQtd(qtd: number){
    this.palavraService.getAllPalavrasByQtd(qtd).subscribe(
      (response: Palavra[]) => {
        this.arrPalavras = response;
        // console.log('AllPalavras: ', response);
        this.loaded = true;
        for (const palavra of response) {
          this.inputJson.push({
            'clue': palavra.dica,
            'answer': palavra.palavraSemAcento.toUpperCase()
          });    
        }
        this.preLayout();
        // console.log("PalavrasIndex: ", this.inputJson);
      },
      error => {
        this.loaded = false;
        this.showModal = false;
        this.alerts.error("Houve erro ao pegar as palavras por QTD",'Atenção', {
          positionClass: 'toast-top-full-width',
          timeOut: 8000
        })
      }
    )
  }

  getPalavraByCategoriaQtd(categoria: string, qtd: number){
    this.palavraService.getAllPalavrasByCategoriaQtd(categoria, qtd).subscribe(
      (response: Palavra[]) => {
        // console.log('AllPalavras: ', response);
        this.arrPalavras = response
        this.loaded = true;
        for (const palavra of response) {
          this.inputJson.push({
            'clue': palavra.dica,
            'answer': palavra.palavraSemAcento.toUpperCase()
          });    
        }

        this.preLayout();
        // console.log("PalavrasIndex: ", this.inputJson);
      },
      error => {
        this.showModal = false;
        this.loaded = false;
        this.alerts.error("Houve erro pegar palavras por qtd e categoria",'Atenção', {
          positionClass: 'toast-top-full-width',
          timeOut: 8000
        })
      }
    )

  }

  preLayout(){    
    var clg = require("crossword-layout-generator");
    var layout = clg.generateLayout(this.inputJson);
    var rows = layout.rows;
    var cols = layout.cols;
    var table = layout.table; // table as two-dimensional array
    this.outputHtml = layout.table_string; // table as plain text (with HTML line breaks)
    this.outputJson = layout.result;
    // console.log('OutputJson: ', this.outputJson);
    //console.log('OutputHTML: ', this.outputHtml);
    this.tableResp = table;
    // console.log('Table: ', this.tableResp);
    this.layoutHTML(table);
  }

  layoutHTML(table: any){
    var objHtml = document.getElementById('table-cross') as HTMLElement
    var tableBody = '';
    var widthTable = table[0].length * 40;
    for (let l = 0; l < table.length; l++) {
      tableBody += '<tr>'
      for (let c = 0; c < table[l].length; c++) {
        if(table[l][c].trim() == '-' || table[l][c].trim() == '' || table[l][c].trim() == ' '){
          tableBody += "<td class='td-size text-center'><input class='text-center input-format' disabled></td>";
        }
        else {
          //tableBody += "<td class='td-size text-center'><input class='text-center input-format' value='" + table[l][c] + "' maxlength='1'></td>";        
          tableBody += "<td class='td-size text-center'><input class='text-center input-format' value='' maxlength='1'></td>";        
        }
      }
      tableBody += '</tr>';      
    }
    objHtml.innerHTML = tableBody
    objHtml.style.width = widthTable + 'px';
    
    for (const item of this.outputJson) {
      this.setIndex(item.startx, item.starty, this.outputJson.indexOf(item));
    }
    this.showModal = false;
    //this.setMarcIndex();    
  }

  setIndex(x: number, y: number, index: number){
    var objTblHtml = document.getElementById('table-cross') as HTMLElement;
    // console.log('x: ', x, 'y: ', y, 'index: ', index);    
    
    try {
      var item = objTblHtml.children[0].childNodes[(y-1)].childNodes[(x-1)] as HTMLElement
      if(item.childElementCount > 1){
        // console.log('Item: ', item, 'contem: 2 elementros');
        item.innerHTML = "<span class='index-dicas-seg'>" + index +"</span>" + item.innerHTML;
      }
      else{ 
        item.innerHTML = "<span class='index-dicas'>" + index +"</span>" + item.innerHTML;
      }
      console.clear();      
    } catch (error) {
      this.getPalavras();
    }   

  }

  validAnswers(){
    this.arrPalavras
    this.tableResp;
    this.arrValid = [];
    var invalid = 0;

    for (const word of this.inputJson) {
      var objTblHtml = document.getElementById('table-cross') as HTMLElement;
      var validWord = '';
      var index = word.position - 1;

      if(word.orientation == 'across'){
        // console.log('word: ', word);
        for (let i = 0; i < word.answer.length; i++) {
          let x = (word.startx - 1) + i;
          let y = word.starty - 1;
          var pre = objTblHtml.children[0].childNodes[y].childNodes[x].lastChild as HTMLInputElement
          validWord += pre.value;
        }

        if(word.answer.replace(/ /g,"").toUpperCase() == validWord.replace(/ /g,"").toUpperCase()){
          this.arrValid.push({
            index: index,
            status: 'Ok'
          });
        }
        else {
          this.arrValid.push({
            index: index,
            status: 'Nok'
          });
        }
        // console.log ('Palavra Encontrada Cross: ', validWord);
      }
      else {
        // console.log('word: ', word);
        for (let i = 0; i < word.answer.length; i++) {
          let x = word.startx - 1;
          let y = (word.starty - 1) + i;
          var pre = objTblHtml.children[0].childNodes[y].childNodes[x].lastChild as HTMLInputElement
          validWord += pre.value;
        }
        // console.log ('Palavra Encontrada Down: ', validWord);

        if(word.answer.replace(/ /g,"").toUpperCase() == validWord.replace(/ /g,"").toUpperCase()){
          this.arrValid.push({
            index: index,
            status: 'Ok'
          });
        }
        else {
          this.arrValid.push({
            index: index,
            status: 'Nok'
          });
        }
      }
      // console.log('arrValid: ', this.arrValid);  
     
    }
                
    for (const i of this.arrValid) {
      let tr = document.getElementById('tr-' + i.index) as HTMLElement

      if(i.status == 'Ok'){
        tr.style.color = 'green';
        tr.style.fontWeight = '600';
      }
      else {
        invalid++;
        tr.style.color = 'red';
        tr.style.fontWeight = '600';
      }
    } 

    this.msgFinal(invalid);
    this.putSessao(invalid);    
  }

  msgFinal(invalid: number){
    if(invalid == 0){
      this.alerts.success("Você acertou todas as palavras.",'Parabens!', {
        positionClass: 'toast-top-full-width',
        timeOut: 10000
      })
    }
    else {
      this.alerts.info("Você acertou " + (this.qtdPalavras - invalid) + " das " + this.qtdPalavras + " selecionadas.",'Foi quase eeeiimm!', {
        positionClass: 'toast-top-full-width',
        timeOut: 10000
      })
    }
    this.showStartGame = true;
    this.finalizarJogo(invalid);
  }

  putSessao(qtdErros: number){
    var dt = new Date();
    var data = {
      "id": this.idSessao,
      "adicionarAcertos": this.qtdPalavras - qtdErros,
      "totalAcertos": 0
    }

    //2021-11-12T19:00:44.217Z
    this.sessaoService.postSessao(this.usuario.email, data).subscribe(
      (response: any) => {
        // console.log('Sessão Put:', response);
      },
      error => {
        this.showModal = false;
        this.alerts.error("Houve erro ao inserir a sessão do jogo",'Atenção', {
          positionClass: 'toast-top-full-width',
          timeOut: 8000
        })
      }
    )
  }

  finalizarJogo(qtdErros: number){  
    let pontuacaoFinal = {
      qtdItems: this.qtdPalavras,
      qtdPontos: this.qtdPalavras - qtdErros,
      qtdErros: qtdErros
    }

    this.pontuacaoService.putPontuacao(this.idJogo, pontuacaoFinal).subscribe(
      (response: any) => {
        this.alerts.warning("Jogo Finalizado!!!",'Atenção!', {
          positionClass: 'toast-top-full-width',
          timeOut: 8000
        })
    },error => {
      this.alerts.error("Houve um erro finalizar o jogo",'Atenção', {
        positionClass: 'toast-top-full-width',
        timeOut: 8000
      })      
    })
  }

}
