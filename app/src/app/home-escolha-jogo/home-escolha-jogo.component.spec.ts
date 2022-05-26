import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeEscolhaJogoComponent } from './home-escolha-jogo.component';

describe('HomeEscolhaJogoComponent', () => {
  let component: HomeEscolhaJogoComponent;
  let fixture: ComponentFixture<HomeEscolhaJogoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HomeEscolhaJogoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeEscolhaJogoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
