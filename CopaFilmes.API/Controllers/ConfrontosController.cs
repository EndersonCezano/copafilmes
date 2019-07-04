using System;
using System.Net;
using System.Threading.Tasks;
using CopaFilmes.API.Filters;
using CopaFilmes.API.Models;
using CopaFilmes.API.Services;
using CopaFilmes.API.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CopaFilmes.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConfrontosController : ControllerBase
    {
        private IListaOficialFilmesService _originalListService;
        private Lazy<IConfrontosService> _lazyConfrontoBusiness;
        private IConfrontosService _confrontoBusiness => _lazyConfrontoBusiness.Value;

        public ConfrontosController(IServiceProvider serviceProvider)
        {
            _originalListService = serviceProvider.GetRequiredService<IListaOficialFilmesService>();
            _lazyConfrontoBusiness = new Lazy<IConfrontosService>(() => serviceProvider.GetRequiredService<IConfrontosService>());
        }

        [HttpGet]
        public async Task<IActionResult> Filmes()
        {
            var filmes = await _originalListService.Filmes;
            return SetContentResult(filmes, _originalListService);
        }

        [HttpPost]
        [ValidateFilmesSelecionadosFilter]
        public async Task<IActionResult> GerarCampeonato([FromBody] FilmesSelecionadosRequest request)
        {
            var vencedores = await _confrontoBusiness.DefinirClassificacaoFinal(request);            
            return SetContentResult(vencedores, _confrontoBusiness);
        }

        private IActionResult SetContentResult(object content, IBaseService serviceInstance)
        {
            return (serviceInstance.HasError()) ? GenerateBadRequestResult(serviceInstance) : GenerateResult(content, HttpStatusCode.OK);
        }

        private IActionResult GenerateResult(object content, HttpStatusCode statusCode)
        {
            return new JsonResult(content)
            {
                ContentType = "application/json",
                StatusCode = (int)statusCode
            };
        }

        /// <summary>
        /// Gerar um erro igual ao padrão de erros de modelos (abaixo), assim o front-end não vai distinguir quando
        /// for erro de modelo por atributo (required/maxlength, por exemplo) ou erro tratado por validação
        /// de regra.
        /// 
        /// {
        ///     "errors": {
        ///         "Id": [
        ///             "O campo Id é obrigatório."
        ///         ]
        ///     },
        ///     "title": "One or more validation errors occurred.",
        ///     "status": 400,
        ///     "traceId": "0HLNSJSNBD4DU:00000001"
        /// }
        /// 
        /// </summary>
        /// <param name="serviceInstance"></param>
        /// <returns></returns>
        private IActionResult GenerateBadRequestResult(IBaseService serviceInstance)
        {
            var json = DefaultBadRequestError.CreateObjectError(serviceInstance.Errors, HttpContext.TraceIdentifier);
            return GenerateResult(json, HttpStatusCode.BadRequest);
        }
    }
}
