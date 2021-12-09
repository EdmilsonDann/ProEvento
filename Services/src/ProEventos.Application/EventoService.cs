using System;
using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersistence _geralPersistence;
        private readonly IEventoPersistence _eventoPersistence;
        public EventoService(IGeralPersistence _geralPersistence, IEventoPersistence _eventoPersistence)
        {
            this._eventoPersistence = _eventoPersistence;
            this._geralPersistence = _geralPersistence;

        }

        public async Task<Evento> AddEventos(Evento model)
        {
            //Qualquer problema.. vou fazer o tratamento nesta camada..nao na de repository.
            try
            {
                 _geralPersistence.Add<Evento>(model);

                 if(await _geralPersistence.SaveChangeAsync())
                 {
                     return await _eventoPersistence.GetEventoByIdAsync(model.Id, false);
                 }

                 return null;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(eventoId, false);

                if(evento == null ) return null;

                model.Id = evento.Id;

                 _geralPersistence.Update(model);

                 if(await _geralPersistence.SaveChangeAsync())
                 {
                     return await _eventoPersistence.GetEventoByIdAsync(model.Id, false);
                 }

                 return null;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
             try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(eventoId, false);

                if(evento == null ) throw new Exception("Evento para delete não encontrado.");

                 _geralPersistence.Delete<Evento>(evento);

                 return await _geralPersistence.SaveChangeAsync();                 
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
               var eventos = await _eventoPersistence.GetAllEventosAsync(includePalestrantes);

               if(eventos == null) return null;

               return eventos;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
               var eventos = await _eventoPersistence.GetAllEventosByTemaAsync(tema, includePalestrantes);

               if(eventos == null) return null;

               return eventos;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
           try
            {
               var eventos = await _eventoPersistence.GetEventoByIdAsync(eventoId, includePalestrantes);

               if(eventos == null) return null;

               return eventos;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

       
    }
}