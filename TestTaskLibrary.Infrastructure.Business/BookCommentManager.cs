using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Domain.Interfaces;

namespace TestTaskLibrary.Infrastructure.Business
{
    class BookCommentManager
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

        public bool AddReview(User user, int bookId, int raiting, string comment)
        {
            if(user != null)
            {
                var info = addInfoRepository.BookAdditionalInfos.FirstOrDefault(a => a.BookId == bookId);
                info.RatingComments.Add(new BookRatingComment() { User = user, Rating = raiting, Content = comment, });
                addInfoRepository.Update(info);
                return true;
            }
            return false;
        }
    }
}
