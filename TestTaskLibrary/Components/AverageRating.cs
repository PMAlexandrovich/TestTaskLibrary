﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Components
{
    public class AverageRating : ViewComponent
    {
        private readonly IGenericRepository<BookReview> reviewsRepository;

        public AverageRating(IGenericRepository<BookReview> reviewsRepository)
        {
            this.reviewsRepository = reviewsRepository;
        }

        public string Invoke(int bookId)
        {
            var reviews = reviewsRepository.GetAll().Where(r => r.BookAdditionalInfoId == bookId).ToList();
            if(reviews.Count != 0)
            {
                var averageRating = reviews.Average(r => r.Rating);
                return $"Оценка: {averageRating:f1} / 5";
            }
            return "Нет оценок";
        }
    }
}
