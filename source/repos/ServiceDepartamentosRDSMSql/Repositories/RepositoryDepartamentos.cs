using ServiceDepartamentosRDSMSql.Data;
using ServiceDepartamentosRDSMSql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceDepartamentosRDSMSql.Repositories
{
    public class RepositoryDepartamentos
    {
        DepartamentoContext context;

        public RepositoryDepartamentos(DepartamentoContext context)
        {
            this.context = context;
        }

        public List<Departamento> GetDepartamentos()
        {
            return this.context.Departamentos.ToList();
        }

        public Departamento FindDepartamento(int id)
        {
            return this.context.Departamentos
                .SingleOrDefault(x => x.IdDepartamento == id);
        }
    }
}
