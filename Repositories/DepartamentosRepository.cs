using ApiJerarquia.Models.DTOs;
using ApiJerarquia.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace ApiJerarquia.Repositories
{
    public class DepartamentosRepository:Repository<Departamentos>
    {
        private readonly ItesrcneActividadesContext _context;
        public DepartamentosRepository(ItesrcneActividadesContext Context) : base(Context)
        {
            _context=Context;
        }

        
        public override IEnumerable<Departamentos> GetAll()
        {
            return _context.Departamentos
                .Include(x => x.InverseIdSuperiorNavigation)
                .OrderBy(x => x.Nombre);
        }
        public override Departamentos? Get(object id)
        {
           if(id==null || !int.TryParse(id.ToString(),
               out int departamentoId))
            {
                return null;
            }
           return _context.Departamentos
                .Include(x=>x.IdSuperiorNavigation)
                .FirstOrDefault(x=>x.Id==departamentoId);
        }

    }
}
