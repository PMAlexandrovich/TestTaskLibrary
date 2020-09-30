using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskLibrary.Domain.Application.Interfaces.Managers
{
    public interface IBookReviewsManager
    {
        Task<int> AddReviewAsync(int userId, int bookId, int rating, string content);

        Task<int> DeleteReviewAsync(int userId, int reviewId);
    }
}
