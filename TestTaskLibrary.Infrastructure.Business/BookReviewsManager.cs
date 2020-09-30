using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Interfaces.Managers;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Domain.Interfaces;

namespace TestTaskLibrary.Infrastructure.Business
{
    public class BookReviewsManager : IBookReviewsManager
    {
        IBooksRepository booksRepository;
        IBookAdditionalInfosRepository addInfoRepository;
        private readonly IBookReviewsRepository reviewsRepository;

        public BookReviewsManager(IBooksRepository booksRepository, IBookAdditionalInfosRepository addInfoRepository, IBookReviewsRepository reviewsRepository)
        {
            this.booksRepository = booksRepository;
            this.addInfoRepository = addInfoRepository;
            this.reviewsRepository = reviewsRepository;
        }

        public async Task<int> AddReviewAsync(int userId, int bookId, int rating, string content)
        {
            var existReview = await reviewsRepository.GetAll.FirstOrDefaultAsync(r => r.UserId == userId && r.BookAdditionalInfo.BookId == bookId);
            if (existReview == null)
            {
                var newReview = new BookReview() { UserId = userId, Rating = rating, Content = content, BookAdditionalInfoId = bookId };
                await reviewsRepository.Create(newReview);
                return newReview.Id;
            }
            return default;
        }

        public async Task<int> DeleteReviewAsync(int userId, int reviewId)
        {
            var review = await reviewsRepository.GetAll.FirstOrDefaultAsync();
            if(review.UserId == userId)
            {
                await reviewsRepository.Delete(reviewId);
                return reviewId;
            }
            return default;
        }
    }
}
