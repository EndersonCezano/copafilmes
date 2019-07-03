using CopaFilmes.API.Models;
using CopaFilmes.API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopaFilmes.API.Services
{
    public class BaseService : IBaseService
    {
        public IEnumerable<Error> Errors { get; private set; }

        public void AddError(string message)
        {
            AddError("Validation", message);
        }

        public void AddError(string field, string message)
        {
            var err = new Error
            {
                Field = field,
                Message = message
            };

            if (Errors == null)
            {
                Errors = new List<Error> { err };
            }
            else
            {
                Errors = Errors.Append(err);
            }
        }

        public bool HasError()
        {
            return (Errors?.Count() > 0);
        }
    }


    public static class ListErrorsExtension
    {
        public static string ToJsonMode(this IEnumerable<Error> errors)
        {
            return string.Join(",", errors.Select(err => err.ToJsonMode()).Cast<string>());
        }
    }
}
