using CopaFilmes.API.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopaFilmes.API.Utils
{
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
    public static class DefaultBadRequestError
    {

        public static JObject CreateObjectError(IEnumerable<Error> errors, string traceId)
        {
            return CreateObjectError(errors.ToJsonMode(), traceId);
        }

        public static JObject CreateObjectError(string error, string traceId)
        {
            var fullError = $@"{{
                                    ""errors"": {{
                                        {error}
                                    }},
                                    ""title"": ""One or more validation errors occurred."",
                                    ""status"": 400,
                                    ""traceId"": ""{traceId}""
                               }}";

            return JObject.Parse(fullError);
        }
    }

}
