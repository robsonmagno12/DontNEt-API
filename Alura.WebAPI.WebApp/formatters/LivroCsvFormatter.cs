using Alura.ListaLeitura.Modelos;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace Alura.WebAPI.WebApp.formatters
{
    public class LivroCsvFormatter : TextOutputFormatter
    {

        public LivroCsvFormatter() 
        {
            var textCsvMidiaType = MediaTypeHeaderValue.Parse("text/csv");
            var appCsvMediaType = MediaTypeHeaderValue.Parse("application/csv");
            SupportedMediaTypes.Add(textCsvMidiaType);
            SupportedMediaTypes.Add(appCsvMediaType);
            SupportedEncodings.Add(Encoding.UTF8);
        
        }

        protected override bool CanWriteType(Type type)
        {
            return type == typeof(LivroApiTeste);
        }
        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var LivroEmCsv = "";

            //instanciando o livro e pegando para alterar para CSV
            // proteger para alterar todo o corpo livro.
            if (context.Object is LivroApiTeste)
            {
                var livro = context.Object as LivroApiTeste; // instacio o objeto e converteu para livroapi

                LivroEmCsv = $"{livro.Titulo}; {livro.Subtitulo}; {livro.Autor}; {livro.Lista}";
            }



           using (var escritor = context.WriterFactory(context.HttpContext.Response.Body, selectedEncoding)) // pegando instancia do context
            {
               return escritor.WriteAsync(LivroEmCsv);

            }//escritor.Close(); char escritor para fechar 
        }
    }
}
