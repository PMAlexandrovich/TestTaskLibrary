using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Interfaces.Managers;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Infrastructure.Business
{
    public class BookReviewsManager : IBookReviewsManager
    {
        private readonly IGenericRepository<BookReview> reviewsRepository;

        public BookReviewsManager(IGenericRepository<BookReview> reviewsRepository)
        {
            this.reviewsRepository = reviewsRepository;
        }

        public async Task<int> AddReviewAsync(int userId, int bookId, int rating, string content)
        {
            var existReview = await reviewsRepository.GetAll().FirstOrDefaultAsync(r => r.UserId == userId && r.BookAdditionalInfo.BookId == bookId);
            if (existReview == null)
            {
                var newReview = new BookReview() { UserId = userId, Rating = rating, Content = content, BookAdditionalInfoId = bookId };
                await reviewsRepository.AddAsync(newReview);
                return newReview.Id;
            }
            return default;
        }

        public async Task<int> DeleteReviewAsync(int userId, int reviewId)
        {
            var review = await reviewsRepository.GetAll().FirstOrDefaultAsync(r => r.Id == reviewId);
            if(review.UserId == userId)
            {
                await reviewsRepository.RemoveAsync(review);
                return reviewId;
            }
            return default;
        }

        public async Task<int> EditReviewAsync(int userId, int reviewId, int newRating, string newContent)
        {
            var review = await reviewsRepository.GetAll().FirstOrDefaultAsync(r => r.Id == reviewId);
            if (review.UserId == userId)
            {
                review.Rating = newRating;
                review.Content = newContent;
                await reviewsRepository.UpdateAsync(review);
                return reviewId;
            }
            return default;
        }
    }
}
