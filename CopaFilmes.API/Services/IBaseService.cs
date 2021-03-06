﻿using System.Collections.Generic;
using CopaFilmes.API.Utils;

namespace CopaFilmes.API.Services
{
    public interface IBaseService
    {
        IEnumerable<Error> Errors { get; }

        void AddError(string message);
        void AddError(string field, string message);
        bool HasError();
    }

}