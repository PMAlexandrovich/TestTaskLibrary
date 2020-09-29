﻿using Microsoft.EntityFrameworkCore;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Domain.Interfaces;

namespace TestTaskLibrary.Infrastructure.Business
{
    public class BookJob : IJob
    {
        LibraryManager libraryManager;
        IBooksRepository booksRepository;

        public BookJob(LibraryManager libraryManager, IBooksRepository booksRepository)
        {
            this.libraryManager = libraryManager;
            this.booksRepository = booksRepository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            booksRepository.GetAll.Where(b => b.CurrentBookStatus.Status == Status.Booked && b.CurrentBookStatus.TimeOfEndBook < DateTime.Now).ToList().ForEach(b => libraryManager.Unbook(b.Id));
            return Task.CompletedTask;
        }
    }
}
