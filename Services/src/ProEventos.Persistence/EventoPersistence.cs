using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class EventoPersistence : IEventoPersistence
    {
        //Injetar o contexto
        private readonly ProEventosContext _context;
        public EventoPersistence(ProEventosContext _context)
        {
            this._context = _context;
            this._context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes)
        {
             //Coloco o Include, para que retorne a cada evento os Lotes e Redes sociais, refente aquele evento
            IQueryable<Evento> query = _context.Eventos.AsNoTracking()
                .Include( e => e.Lotes)
                .Include(e => e.RedesSociais);

            //Aqui o usuario define se ele quer incluir em eventos também os Palestrantes.. que estarao presentes no evento
            //sendo afirmativo inclua os PalestrantesEventos e entao inclua dos que estiverem vinculados dentro 
            //dessa entidade .. traga os paletrantes
            if(includePalestrantes)
            {
                query = query
                    .Include(e => e.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query = query.OrderBy(e => e.Id).Where(e => e.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            //Coloco o Include, para que retorne a cada evento os Lotes e Redes sociais, refente aquele evento
            IQueryable<Evento> query = _context.Eventos.AsNoTracking()
                .Include( e => e.Lotes)
                .Include(e => e.RedesSociais);

            //Aqui o usuario define se ele quer incluir em eventos também os Palestrantes.. que estarao presentes no evento
            //sendo afirmativo inclua os PalestrantesEventos e entao inclua dos que estiverem vinculados dentro 
            //dessa entidade .. traga os paletrantes
            if(includePalestrantes)
            {
                query = query
                    .Include(e => e.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query = query.OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes)
        {
            
            //Coloco o Include, para que retorne a cada evento os Lotes e Redes sociais, refente aquele evento
            IQueryable<Evento> query = _context.Eventos.AsNoTracking()
                .Include( e => e.Lotes)
                .Include(e => e.RedesSociais);

            //Aqui o usuario define se ele quer incluir em eventos também os Palestrantes.. que estarao presentes no evento
            //sendo afirmativo inclua os PalestrantesEventos e entao inclua dos que estiverem vinculados dentro 
            //dessa entidade .. traga os paletrantes
            if(includePalestrantes)
            {
                query = query
                    .Include(e => e.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query = query.OrderBy(e => e.Id).Where(e => e.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }
             
    }
}