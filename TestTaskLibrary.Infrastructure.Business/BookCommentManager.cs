using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Domain.Interfaces;

namespace TestTaskLibrary.Infrastructure.Business
{
    public class BookCommentManager
    {
        IBooksRepository booksRepository;
        IBookAdditionalInfosRepository addInfoRepository;
        IBookRatingCommentsRepository commentsRepository;

        public BookCommentManager(IBooksRepository booksRepository, IBookAdditionalInfosRepository addInfoRepository, IBookRatingCommentsRepository commentsRepository)
        {
            this.booksRepository = booksRepository;
            this.addInfoRepository = addInfoRepository;
            this.commentsRepository = commentsRepository;
        }

        public bool AddReview(string userId, int bookId, int raiting, string comment)
        {
            commentsRepository.Create(new BookRatingComment() { UserId = userId, Rating = raiting, Content = comment, BookAdditionalInfoId = bookId});
            return true;
        }
    }
}
