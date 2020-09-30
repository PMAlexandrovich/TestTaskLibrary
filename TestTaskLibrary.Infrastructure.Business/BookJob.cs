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
        readonly IBooksRepository booksRepository;

        public BookJob(ILibraryManager libraryManager, IBooksRepository booksRepository)
        {
            this.libraryManager = libraryManager;
            this.booksRepository = booksRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            booksRepository.GetAll.Where(b => b.CurrentBookStatus.Status == Status.Booked && b.CurrentBookStatus.TimeOfEndBook < DateTime.Now).ToList().ForEach(async b => await libraryManager.UnbookAsync(b.Id));
        }
    }
}
