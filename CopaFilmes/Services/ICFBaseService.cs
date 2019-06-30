using System.Collections.Generic;
using CopaFilmes.API.Utils;

namespace CopaFilmes.API.Services
{
    public interface ICFBaseService
    {
        IEnumerable<CFError> Errors { get; }

        void AddError(string message);
        void AddError(string field, string message);
        bool HasError();
    }

}