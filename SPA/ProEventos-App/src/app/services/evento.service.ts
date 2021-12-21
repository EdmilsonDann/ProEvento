import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Evento } from '../models/Evento';

//item responsável por deixar disponivel, para qualquer componente que eu quiser injetar esse serviço eu posso
@Injectable()
export class EventoService {

 baseURL = 'https://localhost:5001/api/eventos';

constructor(private http: HttpClient) { }


  //Aqui estou encapsulando minhas chamadas, assim teremos apenas um lugar para ser chamado e um lugar para ser alterado
  //por que depois quem precisar chamar o getEvento.. pode usar
  //O método get do http.. retorna um observable, por isso coloca ali
 public getEventos(): Observable<Evento[]>{
    return this.http.get<Evento[]>(this.baseURL)
  }

   public getEventosByTema(tema: string): Observable<Evento[]>{
    return this.http.get<Evento[]>(`${this.baseURL}/tema/${tema}`)
  }
 public getEventoById(id: number): Observable<Evento>{
    return this.http.get<Evento>(`${this.baseURL}/${id}`)
  }
}
