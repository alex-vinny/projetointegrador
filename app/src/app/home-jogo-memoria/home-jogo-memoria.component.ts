import { Component, OnInit, Output } from '@angular/core';
import { Categoria } from 'src/models/categoria';
import { CategoriaService } from 'src/services/categoria.service';

@Component({
  selector: 'app-home-jogo-memoria',
  templateUrl: './home-jogo-memoria.component.html',
  styleUrls: ['./home-jogo-memoria.component.css']
})
export class HomeJogoMemoriaComponent implements OnInit {
  showBtnCadastrarPalavras: boolean;
  showModal = false;
  showStartGame = true;
  arrCategoria: Categoria[] = [];
  hasFlippedCard:boolean;
  lockBoard: boolean
  
  constructor(
    private categoriService: CategoriaService,
  ) { }

  ngOnInit() {
    const cards = document.querySelectorAll('.card');
    this.hasFlippedCard = false;
    this.lockBoard = false;
    // this.firstCard, secondCard;
    
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

  getImagens(){

  }

  // //função para virar carta
  // flipCard() {
  //     if (lockBoard) return;
  //     if (this === firstCard) return;

  //     this.classList.add('flip');

  //     if (!hasFlippedCard) {
  //         hasFlippedCard = true;
  //         firstCard = this;
  //         return;
  //     }

  //     secondCard = this;
  //     hasFlippedCard = false;

  //     checkForMatch();
  // }

  // //função que checa se as cartas são iguais
  // checkForMatch() {
  //     if (firstCard.dataset.card === secondCard.dataset.card) {
  //         disableCards();
  //         return;
  //     }

  //     unflipCards();
  // }

  // //função que desabilita as cartas
  // disableCards() {
  //     firstCard.removeEventListener('click', flipCard);
  //     secondCard.removeEventListener('click', flipCard);

  //     resetBoard();
  // }

  // //funcão que desvira as cartas
  // unflipCards() {
  //     lockBoard = true;

  //     setTimeout(() => {
  //         firstCard.classList.remove('flip');
  //         secondCard.classList.remove('flip');

  //         resetBoard();
  //     }, 1500);
  // }

  // //função que reseta o tabuleiro
  // resetBoard() {
  //     [hasFlippedCard, lockBoard] = [false, false];
  //     [firstCard, secondCard] = [null, null];
  // }

  // //função que embaralha as cartas
  // (shuffle() {
  //     cards.forEach((card) => {
  //         let randomPosition = Math.floor(Math.random() * 12);
  //         card.style.order = randomPosition;
  //     })
  // })();

  // //adiciona evento de clique na carta
  // cards.forEach((card) => {
  //     card.addEventListener('click', flipCard);
  // });




}
