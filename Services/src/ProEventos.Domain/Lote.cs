using System;

namespace ProEventos.Domain
{
    public class Lote
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public int Quantidade { get; set; }

        //isso é uma convenção para o entity framework, reconhecer essa propriedade como foreign key
        public int EventoId { get; set; }
        public Evento Evento { get; set; }
    }
}