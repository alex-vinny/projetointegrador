import { CartaService } from './../../services/carta.service';
import { Component, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { interval } from 'rxjs/internal/observable/interval';
import { Subscription } from 'rxjs/internal/Subscription';
import { Carta } from 'src/models/Carta';
import { Categoria } from 'src/models/categoria';
import { CategoriaService } from 'src/services/categoria.service';

@Component({
  selector: 'app-home-jogo-memoria',
  templateUrl: './home-jogo-memoria.component.html',
  styleUrls: ['./home-jogo-memoria.component.css']
})
export class HomeJogoMemoriaComponent implements OnInit {
  showBtnCadastrarPalavras: boolean;
  showModal = true;
  showStartGame = true;
  arrCategoria: Categoria[] = [];
  hasFlippedCard:boolean;
  lockBoard: boolean;
  arrInfoPontuacao: any;

  TEMPO = 0;
  cartas: Array<Carta> = [];  
  primeiraCarta: Carta;
  segundaCarta: Carta;
  primeiraCartaVirada: boolean = true;
  travarCartas:boolean = false;
  jogadas: number = 0;  
  tempoInicial: number = this.TEMPO;
  tempoRestante: number = this.TEMPO;
  pontuacao: string = '';
  totalAcerto: number = 0;
  contagem: Subscription;
  fimDeJogo: boolean = false;
  fimDeJogoMsg: string = '';
  qtdSelect: string = '10';
  categoriaSelect: string = '';
  imgTextFlip: string = '';
  
  
  constructor(
    private categoriService: CategoriaService,
    private alerts: ToastrService,
    private cartaService: CartaService,
  ) { } 
  
  ngOnInit(): void {          
    // this.cartas = CARTAS.concat(CARTAS.map(carta => ({...carta})));
    this.showModal = true;
    this.getAllCategorias();
  }
  
  ngOnDestroy(): void {
    this.pararContagem();
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
        console.log(error);
      }
    )
  }

  iniciarContagem(): void { 
    const geradorNumeros = interval(1000);

    this.contagem = geradorNumeros.subscribe( x => {
      this.tempoRestante = this.tempoInicial - x;
      if(this.tempoRestante < 0) {        
        this.gameOver(false);
      }
    });
  }
 
  pararContagem(): void {    
    this.contagem.unsubscribe();
  }
 
  gameOver(ganhou: boolean): void {
    this.pararContagem(); 
    
    if (ganhou){
      // this.fimDeJogoMsg = 'Você ganhou! =D';
      this.alerts.success("Você ganhou!!!",'Parabéns!', {
        positionClass: 'toast-top-full-width',
        timeOut: 8000
      })

    }
    else {
      this.fimDeJogoMsg = 'Você perdeu... =(';
      this.alerts.warning('Não foi dessa vez!','Que pena!', {
        positionClass: 'toast-top-full-width',
        timeOut: 8000
      })
    }

    this.fimDeJogo = true;
    this.travarCartas = true;
    this.showStartGame = true;
  }
 
  iniciarJogo() {
    // this.cartas = [
    //   { id: 1, imagem: "../../assets/img/bulbasaur.jpg", estaVirada: false, descricao: 'Testes', descricaoImg: 'Descrição imgagem', categoria: 'categoria' },
    //   { id: 1, imagem: "../../assets/img/bulbasaur.jpg", estaVirada: false, descricao: 'Testes', descricaoImg: 'Descrição imgagem', categoria: 'categoria' },
    //   { id: 2, imagem: "../../assets/img/charmander.jpg", estaVirada: false, descricao: 'Testes', descricaoImg: 'Descrição imgagem', categoria: 'categoria' },
    //   { id: 2, imagem: "../../assets/img/charmander.jpg", estaVirada: false, descricao: 'Testes', descricaoImg: 'Descrição imgagem', categoria: 'categoria' },
    //   { id: 3, imagem: "../../assets/img/clefairy.jpg", estaVirada: false, descricao: 'Testes', descricaoImg: 'Descrição imgagem', categoria: 'categoria' },
    //   { id: 3, imagem: "../../assets/img/clefairy.jpg", estaVirada: false, descricao: 'Testes', descricaoImg: 'Descrição imgagem', categoria: 'categoria' },
    //   { id: 4, imagem: "../../assets/img/eevee.jpg", estaVirada: false, descricao: 'Testes', descricaoImg: 'Descrição imgagem', categoria: 'categoria' },
    //   { id: 4, imagem: "../../assets/img/eevee.jpg", estaVirada: false, descricao: 'Testes', descricaoImg: 'Descrição imgagem', categoria: 'categoria' },
    //   { id: 5, imagem: "../../assets/img/growlithe.jpg", estaVirada: false, descricao: 'Testes', descricaoImg: 'Descrição imgagem', categoria: 'categoria' },
    //   { id: 5, imagem: "../../assets/img/growlithe.jpg", estaVirada: false, descricao: 'Testes', descricaoImg: 'Descrição imgagem', categoria: 'categoria' },   
    //   { id: 6, imagem: "../../assets/img/jigglypuff.jpg", estaVirada: false, descricao: 'Testes', descricaoImg: 'Descrição imgagem', categoria: 'categoria' },
    //   { id: 6, imagem: "../../assets/img/jigglypuff.jpg", estaVirada: false, descricao: 'Testes', descricaoImg: 'Descrição imgagem', categoria: 'categoria' },
    //   { id: 7, imagem: "../../assets/img/meowth.jpg", estaVirada: false, descricao: 'Testes', descricaoImg: 'Descrição imgagem', categoria: 'categoria' },
    //   { id: 7, imagem: "../../assets/img/meowth.jpg", estaVirada: false, descricao: 'Testes', descricaoImg: 'Descrição imgagem', categoria: 'categoria' },
    //   { id: 8, imagem: "../../assets/img/oddish.jpg", estaVirada: false, descricao: 'Testes', descricaoImg: 'Descrição imgagem', categoria: 'categoria' },
    //   { id: 8, imagem: "../../assets/img/oddish.jpg", estaVirada: false, descricao: 'Testes', descricaoImg: 'Descrição imgagem', categoria: 'categoria' },
    //   { id: 9, imagem: "../../assets/img/pikachu.jpg", estaVirada: false, descricao: 'Testes', descricaoImg: 'Descrição imgagem', categoria: 'categoria' },
    //   { id: 9, imagem: "../../assets/img/pikachu.jpg", estaVirada: false, descricao: 'Testes', descricaoImg: 'Descrição imgagem', categoria: 'categoria' },
    //   { id: 10, imagem: "../../assets/img/psyduck.jpg", estaVirada: false, descricao: 'Testes', descricaoImg: 'Descrição imgagem', categoria: 'categoria' },
    //   { id: 10, imagem: "../../assets/img/psyduck.jpg", estaVirada: false, descricao: 'Testes', descricaoImg: 'Descrição imgagem', categoria: 'categoria' },
    //   { id: 11, imagem: "../../assets/img/squirtle.jpg", estaVirada: false, descricao: 'Testes', descricaoImg: 'Descrição imgagem', categoria: 'categoria' },
    //   { id: 11, imagem: "../../assets/img/squirtle.jpg", estaVirada: false, descricao: 'Testes', descricaoImg: 'Descrição imgagem', categoria: 'categoria' },
    //   { id: 12, imagem: "../../assets/img/vulpix.jpg", estaVirada: false, descricao: 'Testes', descricaoImg: 'Descrição imgagem', categoria: 'categoria' },
    //   { id: 12, imagem: "../../assets/img/vulpix.jpg", estaVirada: false, descricao: 'Testes', descricaoImg: 'Descrição imgagem', categoria: 'categoria' }
    // ] 
    
    
    this.cartaService.getAllCartasByCategoriaQtd(this.categoriaSelect, parseInt(this.qtdSelect)).subscribe(
      (response: Carta[]) => {
        this.cartas = response;        
        // for (const item in response) {
        //   this.cartas.push(response[item])
        // }
        // console.log('Cartas:', response);
      
        this.showStartGame = false;
        this.TEMPO = 600;//parseInt(this.qtdSelect) * 20;
        this.tempoInicial = this.TEMPO;
        this.tempoRestante = this.TEMPO;
        // this.pararContagem();    
        this.travarCartas = true;
        this.jogadas = 0;
        this.fimDeJogo = false;     
        this.cartas.forEach(carta => carta.estaVirada = false);
        this.primeiraCartaVirada = true;
        
        setTimeout(() => {      
          this.embaralhaCartas();       
          this.travarCartas = false;
          this.iniciarContagem();
          this.alerts.info("Jogo iniciado!!!",'Oba!', {
            positionClass: 'toast-top-full-width',
            timeOut: 8000
          })
        }, 500);
      },
      error => {
        this.showModal = false;
        this.alerts.error("Houve um erro ao iniciar o jogo",'Atenção', {
          positionClass: 'toast-top-full-width',
          timeOut: 8000
        })
      }
    )
  }

  finalizarJogo(){    
    this.pararContagem();
    this.fimDeJogo = true;
    this.travarCartas = true;
    this.showStartGame = true;

    this.alerts.warning("Jogo Finalizado!!!",'Atenção!', {
      positionClass: 'toast-top-full-width',
      timeOut: 8000
    })
  }

  embaralhaCartas(): void {
    const qtdItens = this.cartas.length;

    for (let i = qtdItens - 1; i >= 0; i--) {
      let j = Math.floor(Math.random() * qtdItens);
      [this.cartas[i], this.cartas[j]] = [this.cartas[j], this.cartas[i]];
    }
  }

  viraCarta(carta: Carta): void {
    
    this.imgTextFlip = carta.descricao;
    
    if(this.fimDeJogo)
      return

    if(carta.estaVirada)
      return;
    
    carta.estaVirada = true;    

    if(this.primeiraCartaVirada) {
      this.jogadas++;
      this.primeiraCarta = carta;
      this.primeiraCartaVirada = false;
    }
    else {            
      this.primeiraCartaVirada = true;
      this.segundaCarta = carta;
      this.travarCartas = true;
      this.comparaCartas();
    }
  }

  desviraCartas(): void {
    
    setTimeout(() => {
      if(!this.fimDeJogo) {
        this.primeiraCarta.estaVirada = false;
        this.segundaCarta.estaVirada = false;
        this.travarCartas = false;
        this.imgTextFlip = '';
      }
    }, 1500);
  }

  comparaCartas(): void {
    if(this.primeiraCarta.id === this.segundaCarta.id) {  
      this.travarCartas = false;        
      this.atualizarPontuacao(+10);
      this.totalAcerto++;

      if(this.cartas.some(carta => carta.estaVirada === false)) {        
        return;
      }
      else {
        this.gameOver(true);
        return;
      }
    }
    
    this.atualizarPontuacao(-10);
    
    this.desviraCartas();
  }

  atualizarPontuacao(pontos: number): void {
    this.tempoInicial += pontos;

    if(pontos > 0)
      this.pontuacao = `+${pontos} segundos!`;
    else
      this.pontuacao = `${pontos} segundos!`;

    setTimeout(() => this.pontuacao = '', 2000);
  }


}
