using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceDepartamentosRDSMSql.Models;
using ServiceDepartamentosRDSMSql.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceDepartamentosRDSMSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {
        RepositoryDepartamentos repo;

        public DepartamentosController(RepositoryDepartamentos repo)
        {
            this.repo = repo;
        }

        /// <summary>
        /// Lista de departamentos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Departamento> GetDepartamentos()
        {
            return this.repo.GetDepartamentos();
        }

        /// <summary>
        /// Buscar un departamento por su identificador
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>       
        [HttpGet("{id}")]
        public Departamento FindDepartamento(int id)
        {
            return this.repo.FindDepartamento(id);
        }

    }
}
