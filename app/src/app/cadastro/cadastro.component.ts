import { PalavraService } from './../../services/palavra.service';
import { Router } from '@angular/router';
import { Palavra } from './../../models/palavra';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-cadastro',
  templateUrl: './cadastro.component.html',
  styleUrls: ['./cadastro.component.css']
})
export class CadastroComponent implements OnInit {
  arrPalavras: Palavra[] = [];
  newItem = true;
  palavraSelect: Palavra;

  constructor(
    private alerts: ToastrService,
    private router: Router,
    private palavraService: PalavraService
  ) { }

  ngOnInit() {
    this.authVerificacao();
    // this.arrPalavras = [
    //   {
    //     id: 0,
    //     valor: 'teste',
    //     dica: 'teste1',
    //     categoria: 'Português',
    //     serieEscolar: 'Quinta Série',
    //     responsavel: 'carllos.pires@gmail.com'
    //   },
    //   {
    //     id: 1,
    //     valor: 'teste',
    //     dica: 'teste2',
    //     categoria: 'Português',
    //     serieEscolar: 'Sexta Série',
    //     responsavel: 'carllos.pires@gmail.com'
    //   },
    //   {
    //     id: 3,
    //     valor: 'teste',
    //     dica: 'teste3',
    //     categoria: 'História',
    //     serieEscolar: 'Oitava Série',
    //     responsavel: 'carllos.pires@gmail.com'
    //   },
    // ]
  }
  
  authVerificacao(){
    if(sessionStorage.getItem('auth') != 'true'){
      this.router.navigate(['auth']);
    }
  }

  getPalavras(){
    this.palavraService.getAllPalavras().subscribe(
      (response: Palavra[]) => {
        console.log('Palavras:', response);
        this.arrPalavras = response;
      },
      error => {
        console.log(error)
      }
    )
  }

  addPalavra(){
    let palavraForm: Palavra;
    let objPalavra = document.getElementById('palavra') as HTMLInputElement
    let objDica = document.getElementById('dica') as HTMLInputElement
    let objCategoria = document.getElementById('categoria') as HTMLInputElement
    let objSerieEscolar = document.getElementById('serie-escolar') as HTMLInputElement

    let id = Math.random();
    
    palavraForm = {
      id: id,
      valor: objPalavra.value,
      valorSemAcento: objPalavra.value, 
      dica: objDica.value,
      dicaSemAcento: objDica.value,
      categoria: objCategoria.value,
      serieEscolar: objSerieEscolar.value,
      //responsavel: 'carllos.pires@gmail.com' // colocar noma usuário
    }
    this.arrPalavras.push(palavraForm);
        
    this.newItem = true;

    palavraForm = {
      id: 0,
      valor: '',
      valorSemAcento: '',
      dica: '',
      dicaSemAcento: '',
      categoria: '',
      serieEscolar: '',
      //responsavel: ''
    } 

    this.alerts.success("Palavra adicionada.",'Sucesso!', {
      positionClass: 'toast-top-full-width',
      timeOut: 2000
    })

    console.log('ArrPalavras: ', this.arrPalavras);
    this.clearForm();
  }

  populaForm(){
    this.newItem = false;
    console.log('ItemSelect: ', this.palavraSelect);

    let objPalavra = document.getElementById('palavra') as HTMLInputElement
    let objDica = document.getElementById('dica') as HTMLInputElement
    let objCategoria = document.getElementById('categoria') as HTMLInputElement
    let objSerieEscolar = document.getElementById('serie-escolar') as HTMLInputElement

    // objPalavra.value = this.palavraSelect.palavra;
    // objDica.value = this.palavraSelect.dica;
    // objCategoria.value = this.palavraSelect.categoria;
    // objSerieEscolar.value = this.palavraSelect.serieEscolar;

  }

  editPalavra(){
    
    for (let i = 0; i < this.arrPalavras.length; i++) {
      // if(this.arrPalavras[i].id == this.palavraSelect.id)
      // console.log('Item :', this.arrPalavras[i].id);
      // else {

      // }
      
    }

    this.newItem = true;

    this.alerts.success("Informações salvas.",'Sucesso!', {
      positionClass: 'toast-top-full-width',
      timeOut: 2000
    });

    console.log('ArrPalavras: ', this.arrPalavras)
    this.clearForm();
  }

  deletePalavra(objPalvra: Palavra){
    const iPalavra = this.arrPalavras.indexOf(objPalvra);
    if(iPalavra > -1){
      this.arrPalavras.splice(iPalavra, 1);
    }

    this.alerts.success("Palavra removida.",'Sucesso!', {
      positionClass: 'toast-top-full-width',
      timeOut: 2000
    });
    console.log('ArrPalavras: ', this.arrPalavras)
    this.clearForm();
  }

  clearForm(){
    let objPalavra = document.getElementById('palavra') as HTMLInputElement
    let objDica = document.getElementById('dica') as HTMLInputElement
    let objCategoria = document.getElementById('categoria') as HTMLInputElement
    let objSerieEscolar = document.getElementById('serie-escolar') as HTMLInputElement

    objPalavra.value = '';
    objDica.value = '';
    objCategoria.value = '';
    objSerieEscolar.value = '';

    this.newItem = true;
  }

}
