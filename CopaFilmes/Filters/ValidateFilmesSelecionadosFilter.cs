using CopaFilmes.API.Models;
using CopaFilmes.API.Services;
using CopaFilmes.API.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CopaFilmes.API.Filters
{
    public class ValidateFilmesSelecionadosFilter : TypeFilterAttribute
    {
        public ValidateFilmesSelecionadosFilter() : base(typeof(ValidateFilmesSelecionadosFilterImplementation))
        {
        }

        private class ValidateFilmesSelecionadosFilterImplementation : IAsyncActionFilter
        {
            private readonly IListaOficialFilmesService _authorRepository;

            public ValidateFilmesSelecionadosFilterImplementation(IListaOficialFilmesService authorRepository)
            {
                _authorRepository = authorRepository;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var request = context.ActionArguments.First().Value as FilmesSelecionadosRequest;
                if (request?.Selecao?.Count() != 8)
                {
                    context.Result = GenerateBadRequestResult(Resources.ErroModeloFilmesSelecionados, context.HttpContext.TraceIdentifier);
                    return;
                }

                var listaOriginal = await _authorRepository.Filmes;
                var intersecao = request.Selecao.Intersect(listaOriginal.Select(f => f.Id));

                if (intersecao?.Count() != 8)
                {
                    context.Result = GenerateBadRequestResult(Resources.ErroItemNaoExisteFilmesSelecionados, context.HttpContext.TraceIdentifier);
                    return;
                }

                await next();
            }

            private IActionResult GenerateBadRequestResult(string errorMessage, string traceId)
            {
                var err = new CFError
                {
                    Field = nameof(FilmesSelecionadosRequest.Selecao),
                    Message = errorMessage
                };

                var json = CFDefaultBadRequestError.CreateObjectError(err.ToJsonMode(), traceId);

                return new JsonResult(json)
                {
                    ContentType = "application/json",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }
    }
}
