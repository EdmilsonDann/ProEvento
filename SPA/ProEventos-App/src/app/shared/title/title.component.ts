import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-title',
  templateUrl: './title.component.html',
  styleUrls: ['./title.component.scss']
})
export class TitleComponent implements OnInit {

// Variável publica para mostrar no Titulo
//Aqui nao precisa colocar public, porque um input ja é algo considerado pelo angular .. que ja deve ser alguma coisa publica.
  @Input() titulo : string = '';

  constructor() { }

  ngOnInit() {
  }

}
