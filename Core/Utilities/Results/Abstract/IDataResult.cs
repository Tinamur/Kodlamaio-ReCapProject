using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results.Abstract
{
    //Ben bir I Result ım ve I Result da olan 
    //Property ler bende de vardır.
    public interface IDataResult<T> : IResult
    {
        T Data { get; }
    }
}
