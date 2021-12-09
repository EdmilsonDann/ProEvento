using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class PalestrantePersistence : IPalestrantePersistence
    {
        //Injetar o contexto
        private readonly ProEventosContext _context;
        public PalestrantePersistence(ProEventosContext _context)
        {
            this._context = _context;

        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos)
        {
             //Coloco o Include, para que retorne a cada palestrante as Redes sociais, refente aquele palestrante
            IQueryable<Palestrante> query = _context.Palestrantes          
                .Include(p => p.RedesSociais);

            //Aqui o usuario define se ele quer incluir em palestrantes também os Eventos.. que o palestrante participa
            //sendo afirmativo inclua os PalestrantesEventos e entao inclua dos que estiverem vinculados dentro 
            //dessa entidade .. traga os eventos
            if(includeEventos)
            {
                query = query
                    .Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            query = query.OrderBy(p => p.Id).Where(p => p.Id == palestranteId);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos)
        {
              //Coloco o Include, para que retorne a cada palestrante as Redes sociais, refente aquele palestrante
            IQueryable<Palestrante> query = _context.Palestrantes          
                .Include(p => p.RedesSociais);

            //Aqui o usuario define se ele quer incluir em palestrantes também os Eventos.. que o palestrante participa
            //sendo afirmativo inclua os PalestrantesEventos e entao inclua dos que estiverem vinculados dentro 
            //dessa entidade .. traga os eventos
            if(includeEventos)
            {
                query = query
                    .Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            query = query.OrderBy(p => p.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos)
        {
               //Coloco o Include, para que retorne a cada palestrante as Redes sociais, refente aquele palestrante
            IQueryable<Palestrante> query = _context.Palestrantes          
                .Include(p => p.RedesSociais);

            //Aqui o usuario define se ele quer incluir em palestrantes também os Eventos.. que o palestrante participa
            //sendo afirmativo inclua os PalestrantesEventos e entao inclua dos que estiverem vinculados dentro 
            //dessa entidade .. traga os eventos
            if(includeEventos)
            {
                query = query
                    .Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            query = query.OrderBy(p => p.Id).Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }   
    }
}