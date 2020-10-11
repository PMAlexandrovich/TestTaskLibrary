using MediatR;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Domain.Application.Features.Report
{
    public class GetBooksReportInPeriodQuery : IRequest<Stream>
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public class GetBooksReportInPeriodQueryHandler : IRequestHandler<GetBooksReportInPeriodQuery, Stream>
        {
            private readonly IGenericRepository<BookStatus> statusRepository;

            public GetBooksReportInPeriodQueryHandler(IGenericRepository<BookStatus> statusRepository)
            {
                this.statusRepository = statusRepository;
            }

            public async System.Threading.Tasks.Task<Stream> Handle(GetBooksReportInPeriodQuery request, CancellationToken cancellationToken)
            {
                var groupStatuses = statusRepository.GetAll()
                    .Include(s => s.User)
                    .Include(s => s.Book)
                        .ThenInclude(b => b.Author)
                    .Include(s => s.Book)
                        .ThenInclude(b => b.Genre)
                    .Where(s => s.StatusSetAt <= request.End && s.StatusSetAt >= request.Start && s.Status == Status.Issued).AsEnumerable().OrderBy(s => s.StatusSetAt).GroupBy(s => s.Book);

                var workbook = new XSSFWorkbook();
                var sheet = workbook.CreateSheet("Отчет");

                var headRow = sheet.CreateRow(0);

                var font = workbook.CreateFont();
                font.IsBold = true;

                var style = workbook.CreateCellStyle();
                style.SetFont(font);

                ICell cell;
                cell = headRow.CreateCell(0);
                cell.SetCellValue("Название книги");
                cell.CellStyle = style;

                cell = headRow.CreateCell(1);
                cell.SetCellValue("Была выдана, раз");
                cell.CellStyle = style;

                int i = 1;
                foreach (var group in groupStatuses)
                {
                    var book = group.Key;
                    var issued = group.Count();

                    var row = sheet.CreateRow(i++);
                    row.CreateCell(0).SetCellValue(book.Title);
                    row.CreateCell(1).SetCellValue(issued);
                }

                for (int j = 0; j < 2; j++)
                {
                    sheet.AutoSizeColumn(j);
                }

                var stream = new MemoryStream();
                workbook.Write(stream, true);
                stream.Position = 0;
                return stream;
            }
        }
    }
}
