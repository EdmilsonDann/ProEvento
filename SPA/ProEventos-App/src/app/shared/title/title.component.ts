import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-title',
  templateUrl: './title.component.html',
  styleUrls: ['./title.component.scss']
})
export class TitleComponent implements OnInit {

// Variável publica para mostrar no Titulo
//Aqui nao precisa colocar public, porque um input ja é algo considerado pelo angular .. que ja deve ser alguma coisa publica.
  @Input() titulo : string = '';
  @Input() iconClass = 'fa fa-user';
  @Input() subtitulo = 'Desde 2021';
  @Input() botaoListar = false;

  constructor(private router: Router) { }

  ngOnInit() {
  }

  listar(): void{
     this.router.navigate([`/${this.titulo.toLocaleLowerCase()}/lista`]);
  }

}
