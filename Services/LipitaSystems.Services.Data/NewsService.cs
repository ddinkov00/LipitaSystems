using System;
using System.Collections.Generic;
using System.Linq;
using LipitaSystems.Data.Common.Repositories;
using LipitaSystems.Data.Models;
using LipitaSystems.Services.Data.Contracts;

namespace LipitaSystems.Services.Data
{
    public class NewsService : INewsService
    {
        private readonly IDeletableEntityRepository<News> newsRepository;

        public NewsService(IDeletableEntityRepository<News> newsRepository)
        {
            this.newsRepository = newsRepository;
        }

        public IEnumerable<News> GetLastNews(int count)
        {
            return this.newsRepository.All()
                                      .OrderByDescending(x => x.CreatedOn)
                                      .Take(count)
                                      .ToList();
        }
    }
}
