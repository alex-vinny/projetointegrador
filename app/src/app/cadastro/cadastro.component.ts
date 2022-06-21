import { CategoriaService } from './../../services/categoria.service';
import { FormControl, FormGroup, Validators  } from '@angular/forms';
import { PalavraService } from './../../services/palavra.service';
import { Router } from '@angular/router';
import { Palavra } from './../../models/palavra';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Categoria } from 'src/models/categoria';

@Component({
  selector: 'app-cadastro',
  templateUrl: './cadastro.component.html',
  styleUrls: ['./cadastro.component.css']
})
export class CadastroComponent implements OnInit {
  registerForm: FormGroup;
  arrPalavras: Palavra[] = [];
  newItem = true;
  palavraSelect: Palavra;
  arrCategoria: Categoria[] = [];
  showModal = true;

  constructor(
    private alerts: ToastrService,
    private router: Router,
    private palavraService: PalavraService,
    private categoriService: CategoriaService
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
    else {
      this.validation();
      this.getPalavras();
      //this.palavraService.getAllPalavras
    }
  }

  getPalavras(){
    this.palavraService.getAllPalavras().subscribe(
      (response: Palavra[]) => {
        // console.log('Palavras:', response);
        this.arrPalavras = response;
        this.getAllCategorias();
      },
      error => {
        this.showModal = false;
        console.log(error)
      }
    )
  }

  getAllCategorias(){
    this.categoriService.getAllCategorias().subscribe(
      (response: Categoria[]) => {
        this.arrCategoria = response;
        this.showModal = false;
        //console.  log('Categorias:', response);
      },
      error => {
        this.showModal = false;
        console.log(error)
      }
    )
  }

  validation(){
    this.registerForm = new FormGroup({
      palavra: new FormControl('', Validators.required),
      dicaPalavra: new FormControl('', Validators.required),
      categoria: new FormControl('', Validators.required),
      serieEscolar: new FormControl('', Validators.required)
    })
  }

  addPalavra(){
    this.showModal = true;
    let objPalavra = document.getElementById('palavra') as HTMLInputElement
    let objDica = document.getElementById('dica') as HTMLInputElement
    let objCategoria = document.getElementById('categoria') as HTMLInputElement
    let objSerieEscolar = document.getElementById('serie-escolar') as HTMLInputElement

    var newPalavra = {
      palavra: objPalavra.value,
      categoria: objCategoria.value,
      dicaPalavra: objDica.value,
      serieEscolar: parseInt(objSerieEscolar.value)
    }

    // console.log('PalavraNew: ', newPalavra);

    this.palavraService.postPalavra(newPalavra).subscribe(
      (newPalavra: any) => {
        //console.log('UsuarioCadastrado', novoUsuario);
        this.showModal = false;
        this.alerts.success("Palavra cadastrada com sucesso.",'Cadastro com sucesso!', {
          positionClass: 'toast-top-full-width',
          timeOut: 8000
        })
        this.registerForm.reset();
        this.getPalavras();
      }, error => {
        this.showModal = false;

        if(error.status == 422){
          this.alerts.warning("Já há uma palavra cadastrada para os dados informados",'Atenção!', {
            positionClass: 'toast-top-full-width',
            timeOut: 8000
          })
        }
        else {
          this.alerts.error("Ocorreu um erro ao tentar cadastrar a palavra, tente novamente mais tarde",'Atenção!', {
            positionClass: 'toast-top-full-width',
            timeOut: 8000
          })
        }
        this.registerForm.reset();
      }
    )    
  }

  sendToForm(){
    this.showModal = true;
    this.newItem = false;
    // console.log('ItemSelect: ', this.palavraSelect);

    let objPalavra = document.getElementById('palavra') as HTMLInputElement
    let objDica = document.getElementById('dica') as HTMLInputElement
    let objCategoria = document.getElementById('categoria') as HTMLInputElement
    let objSerieEscolar = document.getElementById('serie-escolar') as HTMLInputElement

    this.registerForm.get('palavra')?.setValue(this.palavraSelect.palavra);
    this.registerForm.get('categoria')?.setValue(this.palavraSelect.categoria);
    this.registerForm.get('dicaPalavra')?.setValue(this.palavraSelect.dica);
    this.registerForm.get('serieEscolar')?.setValue(this.palavraSelect.serieEscolar);
    this.showModal = false;
  }

  saveEditPalavra(){
    this.showModal = true;
    var palavra =  this.registerForm.get('palavra')?.value;
    var categoria = this.registerForm.get('categoria')?.value;
    
    this.palavraService.putPalavra(palavra, categoria).subscribe(
      (newPalavra: any) => {
        this.showModal = false;
        //console.log('UsuarioCadastrado', novoUsuario);
        this.alerts.success("Palavra cadastrada com sucesso.",'Cadastro com sucesso!', {
          positionClass: 'toast-top-full-width',
          timeOut: 8000
        })
        this.registerForm.reset();
        this.getPalavras();
      }, error => {
        this.showModal = false;
        //console.log('Error: ', error);
        if(error.status == 422){
          this.alerts.warning("Já há uma palavra cadastrada para os dados informados",'Atenção!', {
            positionClass: 'toast-top-full-width',
            timeOut: 8000
          })
        }
        else {
          this.alerts.error("Ocorreu um erro ao tentar cadastrar a palavra, tente novamente mais tarde",'Atenção!', {
            positionClass: 'toast-top-full-width',
            timeOut: 8000
          })
        }
        this.registerForm.reset();
      }
    ) 

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
    // console.log('ArrPalavras: ', this.arrPalavras)
    this.clearForm();
  }

  clearForm(){
    this.registerForm.reset();
  }

}
