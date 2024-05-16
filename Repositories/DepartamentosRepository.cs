using ApiJerarquia.Models.DTOs;
using ApiJerarquia.Models.Entities;

namespace ApiJerarquia.Repositories
{
    public class DepartamentosRepository:Repository<Departamentos>
    {
        private readonly ItesrcneActividadesContext context;
        public DepartamentosRepository(ItesrcneActividadesContext Context) : base(Context)
        {
            context=Context;
        }
        public Departamentos?Get(string email)
        {
            return context.Departamentos.Where(x => x.Username == email).FirstOrDefault();
        }
        public IEnumerable<DepartamentoDTO>GetDepartamentos()
        {
            return context.Departamentos.OrderBy
                (x => x.Nombre)
                .Select(d => new DepartamentoDTO
                {
                    Id=d.Id,
                    
                });
                
        }

    }
}
