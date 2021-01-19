using System;
using System.Collections.Generic;
using LipitaSystems.Data.Models;

namespace LipitaSystems.Services.Data.Contracts
{
    public interface INewsService
    {
        IEnumerable<News> GetLastNews(int count);
    }
}
