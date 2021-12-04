import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {


  public eventos: any = [];

  // 4º passo Crio essa propriedade para receber os meus itens filtrados
  public eventosFiltrados: any = [];


  widthImg = 150;    //pode ser direto também.. sem declarar o tipo number
  marginImg: number = 2;
  exibeImagem = true;  // exibeImagem: boolean = true; ->  Tanto faz..

  //1º passo - crio essa propriedade privada
  private _filtroLista: string = ''; //_filtroLista = '';

  // 2º passo - crio essa propriedade publica GET.. como no C# get; set; .. é um método no bem da verdade..mas tratado como propriedade
  public get filtroLista():string{
    return this._filtroLista;
  }

  // 5º passo - crio essa propriedade SET -- toda a propriedade SET precisa ter um valor
  public set filtroLista(value: string){
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ?
     this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  // 3º passo - crio esse método que será chamado no SET
  public filtrarEventos(filtrarPor: string): any{
    filtrarPor = filtrarPor.toLocaleLowerCase(); //coloco esse método para converter tudo para minusculo

    // vou usar esse método filter que é do próprio javascript .. para filtrar dentro da minha propriedade as palavras do filtro
    // basta usar o || para ir buscando nas propriedades da entidade que se deseja buscar
    return this.eventos.filter(
      (evento: any) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor)  !== -1  ||
                       evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }


  //criada essa propriedade privada com nome http, para receber o  HTTPCLIENT
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    //Qualquer método criado, deve ter sua chamada colocada
    // dento do método ngOninit, pois ele é chamado antes de iniciar nossa aplicação.
    //caso contrário, nossa metodo novo nao ira responder
    this.getEventos();
  }

  public AlterarStatus(){
    this.exibeImagem = !this.exibeImagem;
  }

  public getEventos():void{
      this.http.get('https://localhost:5001/api/eventos').subscribe(
          response => this.eventosFiltrados = this.eventos = response,
          error => console.log(error)
      );
  }

}
