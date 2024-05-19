using ApiJerarquia.Models.Entities;
using ApiJerarquia.Models.DTOs;
using Microsoft.EntityFrameworkCore;
namespace ApiJerarquia.Repositories
{
    public class ActividadesRepository : Repository<Actividades>
    {
        private readonly ItesrcneActividadesContext _context;
        public ActividadesRepository
            (ItesrcneActividadesContext context) : base(context)
        {
            _context = context;
        }
        //Referencia de la base de datos:
        //0 borrador, 1 publicado, 2 eliminado
        public IEnumerable<Actividades> GetActividades()
        {
            return _context.Actividades
                .Include(x => x.IdDepartamentoNavigation)
                .Where(x => x.Estado == 1);
        }
        public IEnumerable<Actividades>? GetActividadesEliminadas()
        {
            return Context.Actividades
                .Include(x=>x.IdDepartamentoNavigation)
                .Where(x=>x.Estado == 2);
        }
        public IEnumerable<Actividades>? GetBorradores()
        {
            return _context.Actividades
                .Include(x=>x.IdDepartamentoNavigation)
                .Where(x=>x.Estado == 0);
        }
        public override Actividades? Get(object id)
        {
            if (id== null || !int.TryParse(id.ToString(),out int actividadId))
            {
                return null;
            }
            return _context.Actividades
                .Include(x => x.IdDepartamentoNavigation)
                .FirstOrDefault(x => x.Id == actividadId);
                    
        }
        public IEnumerable<Actividades>? GetActividadesporDepartamento(int id)
        {
            return _context.Actividades
                .Include(x => x.IdDepartamentoNavigation)
                .Where(x => x.IdDepartamento == id);
        }
    }
}
