import { UsuarioService } from './../../services/usuario.service';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Usuario } from 'src/models/usuario';
import { of } from 'rxjs';
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
  inputJson: any;
  outputHtml: any;
  outputJson: any;

  constructor(
    private router: Router,
    private usuarioService: UsuarioService
  ) { }

  ngOnInit() { 
    if(this.usuario == undefined){
      this.user = sessionStorage.getItem('usuario');
      this.usuario = JSON.parse(this.user);
    }
    console.log('Usuario: ', this.usuario);
    this.authVerificacao();

    this.inputJson = [
      {"clue":"Como fazer o teste 1","answer":"teste1"},
      {"clue":"Como fazer o teste 2","answer":"teste2"},
      {"clue":"Como fazer o teste 3","answer":"teste3"},
      {"clue":"Como fazer o teste 4","answer":"teste4"},
      {"clue":"Como fazer o teste 5","answer":"fazer5"}
    ]

    this.preLayout();
  }


  authVerificacao(){
    if(sessionStorage.getItem('auth') != 'true'){
      this.router.navigate(['auth']);
    }
  }

  preLayout(){
    var clg = require("crossword-layout-generator");
    var layout = clg.generateLayout(this.inputJson);
    var rows = layout.rows;
    var cols = layout.cols;
    var table = layout.table; // table as two-dimensional array
    this.outputHtml = layout.table_string; // table as plain text (with HTML line breaks)
    this.outputJson = layout.result;
    console.log('OutputJson: ', this.outputJson);
    console.log('OutputHTML: ', this.outputHtml);
    console.log('Table: ', table);
    this.layoutHTML(table)
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
          tableBody += "<td class='td-size text-center'><input class='text-center input-format' value='" + table[l][c] + "'></td>";        
        }
      }
      tableBody += '</tr>';      
    }
    objHtml.innerHTML = tableBody
    objHtml.style.width = widthTable + 'px';
    
    for (const item of this.outputJson) {
      this.setIndex(item.startx, item.starty, this.outputJson.indexOf(item));
    }
  }

  setIndex(x: number, y: number, index: number){
    var objTblHtml = document.getElementById('table-cross') as HTMLElement;
    //var objTblBody = objTblHtml.children[0].childNodes;    
    console.log('x: ', x, 'y: ', y, 'index: ', index);    
    objTblHtml.children[0].childNodes[(y-1)].childNodes[(x-1)];  
  }

}
