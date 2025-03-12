using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
namespace ERP.Utility.Helpers
{
    public class ReportHelpers
    {
        public static DateTime FormatDateToString(string date)
        {
            try
            {
                DateTime parsedDate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                return parsedDate;
            }
            catch (FormatException)
            {
                return DateTime.Now;
            }
        }
        public static byte[] GeneratePdfReport()
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                        .Text("Report Header")
                        .SemiBold().FontSize(24).AlignCenter();

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(column =>
                        {
                            column.Item().Text("Dynamic Table:");

                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Text("Column 1");
                                    header.Cell().Text("Column 2");
                                    header.Cell().Text("Column 3");
                                });
                                for (int i = 1; i <= 10; i++)
                                {
                                    table.Cell().Text($"Row {i}, Col 1");
                                    table.Cell().Text($"Row {i}, Col 2");
                                    table.Cell().Text($"Row {i}, Col 3");
                                }
                            });
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                        });
                });
            });

            return document.GeneratePdf();
        }
    }
}
