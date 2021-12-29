import { Router } from '@angular/router';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Evento } from '@app/models/Evento';
import { EventoService } from '@app/services/evento.service';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.scss']
})
export class EventoListaComponent implements OnInit {

  modalRef?: BsModalRef;
  public eventos: Evento[] = [];

  // 4º passo Crio essa propriedade para receber os meus itens filtrados
  public eventosFiltrados: Evento[] = [];


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
  public filtrarEventos(filtrarPor: string): Evento[]{
    filtrarPor = filtrarPor.toLocaleLowerCase(); //coloco esse método para converter tudo para minusculo

    // vou usar esse método filter que é do próprio javascript .. para filtrar dentro da minha propriedade as palavras do filtro
    // basta usar o || para ir buscando nas propriedades da entidade que se deseja buscar
    return this.eventos.filter(
      (evento: any) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor)  !== -1  ||
                       evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }


  //criada essa propriedade privada com nome http, para receber o  HTTPCLIENT
  //constructor(private http: HttpClient) { }   Estava assim .. mas no curso foi modificado depois

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
    ) { }

  ngOnInit(): void {
    //Carrega o spinner, ao inicializar a página
    this.spinner.show();

    //Qualquer método criado, deve ter sua chamada colocada
    // dento do método ngOninit, pois ele é chamado antes de iniciar nossa aplicação.
    //caso contrário, nossa metodo novo nao ira responder
    this.getEventos();
  }

  public AlterarStatus(): void{
    this.exibeImagem = !this.exibeImagem;
  }

  public getEventos():void{
      this.eventoService.getEventos().subscribe({
        next: (eventos: Evento[]) => {
          this.eventos = eventos;
          this.eventosFiltrados = this.eventos;
        },
        error:(error: any) => {
          this.spinner.hide();
          this.toastr.error('Erro ao Carregar os Eventos', 'Erro');
        },
        complete: () => this.spinner.hide()
      });
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();
    this.toastr.success('Apagado', 'Registro foi excluido com sucesso!');
  }

  decline(): void {
    this.modalRef?.hide();
  }

  detalheEvento(id: number): void{
    this.router.navigate([`eventos/detalhe/${id}`]);
  }

}
