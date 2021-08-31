using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alura.ListaLeitura.Modelos;
using Alura.ListaLeitura.Persistencia;
using Microsoft.AspNetCore.Mvc;
using lista = Alura.ListaLeitura.Modelos.ListaLeitura;

namespace Alura.WebAPI.WebApp.Api
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ListasLeituraController : ControllerBase
    {
        private readonly IRepository<Livro> _repo ;
       

        public ListasLeituraController(IRepository<Livro> repository)
        {
            _repo = repository;

        }
        //alteração select
        private lista CriaLista(TipoListaLeitura tipo) 
        {
            return new lista
            {
                Tipo = tipo.ParaString(),
                Livros = _repo.All
                .Where(l => l.Lista == tipo)
                .Select(l => l.ToApiTeste())
                .ToList() // to list converter em númeravel
            };
        }

        [HttpGet]
        public IActionResult TodasListas()
        {
            lista paraLer = CriaLista(TipoListaLeitura.ParaLer);
            lista lendo   = CriaLista(TipoListaLeitura.Lendo);
            lista lidos   = CriaLista(TipoListaLeitura.Lidos); 

            var colecao = new List<lista> { paraLer, lendo, lidos };
            return Ok(colecao); // retorna a coleção de livro
           
        }

        [HttpGet(" {tipo} ")]
        public IActionResult Recuperar(TipoListaLeitura tipo) 
        {
            var lista = CriaLista(tipo);
            return Ok(lista);
        }

      
    }
}
