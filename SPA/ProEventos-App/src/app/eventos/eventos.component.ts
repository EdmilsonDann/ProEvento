import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {


  public eventos: any;

  //criada essa propriedade privada com nome http, para receber o  HTTPCLIENT
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    //Qualquer método criado, deve ter sua chamada colocada
    // dento do método ngOninit, pois ele é chamado antes de iniciar nossa aplicação.
    //caso contrário, nossa metodo novo nao ira responder
    this.getEventos();
  }

  public getEventos():void{
      this.http.get('https://localhost:5001/api/eventos').subscribe(
          response => this.eventos = response,
          error => console.log(error)
      );
  }

}
