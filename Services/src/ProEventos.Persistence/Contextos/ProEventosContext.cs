using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contextos
{
    public class ProEventosContext : DbContext
    {
        public ProEventosContext(DbContextOptions<ProEventosContext> options) : base(options){}   

        //Muitos para Muitos
        public DbSet<Evento> Eventos {get; set;}
        
        //Muitos para Muitos
        public DbSet<Palestrante> Palestrantes {get; set;}

        public DbSet<Lote> Lotes {get; set;}
        public DbSet<PalestranteEvento> PalestrantesEventos {get; set;}
        public DbSet<RedeSocial> RedeSociais {get; set;}

        //Amarra a tabela de muitos para muitos
        //Aqui ele faz um associacao, essa classe quando for criada no banco de dados será a classe de junçao entre os eventos
        // e os palestrantes-
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<PalestranteEvento>().HasKey(PE => new { PE.EventoId, PE.PalestranteId});  

            //*******  Processo para deletar em cascata.. objetos com mais uma chave estrangeira  *******************
            //Para deletar todas as amarraçoes faço o item abaixo
            //Indico ao modelBuilder que ele possui uma entidade Evento e dentro deal
            // posso ter varias redes sociais e cada RedeSocial tera .. um Evento
            // Sendo assim quando voce deletar um Evento delete as redes sociais dele também, fazendo um efeito cascata.
            modelBuilder.Entity<Evento>()
                .HasMany(e => e.RedesSociais)
                .WithOne(rs => rs.Evento)          
                .OnDelete(DeleteBehavior.Cascade);

            //e faço o mesmo raciocinio para a outra chave paslestrante
             modelBuilder.Entity<Palestrante>()
                .HasMany(e => e.RedesSociais)
                .WithOne(rs => rs.Palestrante)          
                .OnDelete(DeleteBehavior.Cascade);
            // *********************************************************************************************************
        }
    }
}