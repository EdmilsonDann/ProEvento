namespace ProEventos.API.Model
{
    public class Evento
    {
        public int EventoId { get; set; }
        public string Local { get; set; }
        public string DataEvento { get; set; }
        public string Tema { get; set; }
        public string QudPessoas { get; set; }
        public string Lote { get; set; }
        public string ImagemURL { get; set; }
    }
}