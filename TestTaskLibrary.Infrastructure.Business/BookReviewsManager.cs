using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Domain.Interfaces;

namespace TestTaskLibrary.Infrastructure.Business
{
    public class BookReviewsManager
    {
        IBooksRepository booksRepository;
        IBookAdditionalInfosRepository addInfoRepository;
        IBookReviewsRepository reviewsRepository;

        public BookReviewsManager(IBooksRepository booksRepository, IBookAdditionalInfosRepository addInfoRepository, IBookReviewsRepository commentsRepository)
        {
            this.booksRepository = booksRepository;
            this.addInfoRepository = addInfoRepository;
            this.reviewsRepository = commentsRepository;
        }

        public bool AddReview(int userId, int bookId, int raiting, string comment)
        {
            reviewsRepository.Create(new BookReview() { UserId = userId, Rating = raiting, Content = comment, BookAdditionalInfoId = bookId});
            return true;
        }
    }
}
