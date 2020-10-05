using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TestTaskLibrary.Domain.Application.Features.BookFeatures.ViewModels;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Infrastructure.Business
{
    public class ReportBuilder
    {
        public Stream BuildBookReport(BookViewModel book, IEnumerable<StatusViewModel> statuses)
        {
            var workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet("Отчет");

            var firstRow = sheet.CreateRow(0);
            firstRow.CreateCell(0).SetCellValue("Название книги");

            var cell = firstRow.CreateCell(1);
            cell.SetCellValue(book.Title);
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 1, 2));
            var style = workbook.CreateCellStyle();
            style.WrapText = true;
            cell.CellStyle = style;

            var headRow = sheet.CreateRow(1);
            headRow.CreateCell(0).SetCellValue("Статус");
            headRow.CreateCell(1).SetCellValue("Дата и время");
            headRow.CreateCell(2).SetCellValue("Клиент");

            int i = 2;
            foreach (var status in statuses)
            {
                var row = sheet.CreateRow(i++);
                row.CreateCell(0).SetCellValue(StatusToString(status.Status));
                row.CreateCell(1).SetCellValue(status.StatusSetAt?.ToString());
                row.CreateCell(2).SetCellValue(status.User?.FullName);
            }
            for (i = 0; i < 3; i++)
            {
                sheet.AutoSizeColumn(i);
            }
            var stream = new MemoryStream();
            workbook.Write(stream, true);
            stream.Position = 0;
            return stream;
        }

        private string StatusToString(Status status)
        {
            return status switch
            {
                Status.Free => "Доступна",
                Status.Booked => "Забронирована",
                Status.Issued => "Выдана",
                _ => "Неизвестно"
            };
        }
    }
}
