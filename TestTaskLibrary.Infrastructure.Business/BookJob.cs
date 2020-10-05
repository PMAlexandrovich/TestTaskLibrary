using Microsoft.EntityFrameworkCore;
using Quartz;
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
    public class BookJob : IJob
    {
        private readonly ILibraryManager libraryManager;
        private readonly IBooksRepository booksRepository;

        public BookJob(ILibraryManager libraryManager, IBooksRepository booksRepository)
        {
            this.libraryManager = libraryManager;
            this.booksRepository = booksRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var booksForUnbook = booksRepository.GetAll
                .Include(b => b.CurrentBookStatus)
                    .ThenInclude(s => s.User)
                .Where(b => b.CurrentBookStatus.Status == Status.Booked && b.CurrentBookStatus.StatusSetAt + TimeSpan.FromMinutes(2) < DateTime.Now).ToList();

            foreach (var book in booksForUnbook)
            {
                await libraryManager.UnbookAsync(book.CurrentBookStatus.User, book.Id);
            }
        }
    }
}
