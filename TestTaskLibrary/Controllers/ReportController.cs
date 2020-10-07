using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTaskLibrary.Domain.Application.Features.BookFeatures.Queries;
using TestTaskLibrary.Domain.Application.Features.Report;
using TestTaskLibrary.Domain.Application.Features.StatusFeatures.Queries;
using TestTaskLibrary.Infrastructure.Business;
using TestTaskLibrary.Models;
using TestTaskLibrary.Models.Report;

namespace TestTaskLibrary.Controllers
{
    [Authorize(Roles = RoleTypes.Librarian)]
    public class ReportController : Controller
    {
        private readonly IMediator mediator;

        public ReportController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Report(int id)
        {
            var book = await mediator.Send(new GetBookByIdQuery() { Id = id });
            var statuses = (await mediator.Send(new GetStatusesByBookId() { BookId = id })).OrderBy(s => s.StatusSetAt);
            if (book != null)
            {
                var reportBuilder = new ReportBuilder();
                var stream = reportBuilder.BuildBookReport(book, statuses);

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx");
            }
            return NotFound();
        }

        public IActionResult ReportAll()
        {
            return View(new ReportInPeriod() { End = DateTime.Now, Start = new DateTime((DateTime.Now.Year),1,1)});
        }
        [HttpPost]
        public async Task<IActionResult> ReportAll(ReportInPeriod reportPeriod)
        {
            var stream = await mediator.Send(new GetBooksReportInPeriodQuery() { End = reportPeriod.End, Start = reportPeriod.Start });
            if (stream != null)
            {
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx");
            }
            //var books = await mediator.Send(new GetBooksWithStatusesQuery());
            //if (books != null)
            //{
            //    var reportBuilder = new ReportBuilder();
            //    var stream = reportBuilder.BuildBooksReport(books);

            //    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx");
            //}
            return NotFound();
        }

    }
}