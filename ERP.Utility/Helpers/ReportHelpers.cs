using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
        public static byte[] GeneratePdfReport<T>(IEnumerable<T> data)
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
                        .Text("Dynamic Report")
                        .SemiBold().FontSize(24).AlignCenter();

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(column =>
                        {
                            column.Item().Text("Data Table:");

                            // Create a table
                            column.Item().Table(table =>
                            {
                                // Get property names for headers
                                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                                // Define table columns dynamically
                                table.ColumnsDefinition(columns =>
                                {
                                    foreach (var _ in properties)
                                    {
                                        columns.RelativeColumn();
                                    }
                                });

                                // Add table headers
                                table.Header(header =>
                                {
                                    foreach (var property in properties)
                                    {
                                        header.Cell().Text(property.Name).SemiBold();
                                    }
                                });

                                // Add rows dynamically
                                foreach (var item in data)
                                {
                                    foreach (var property in properties)
                                    {
                                        var value = property.GetValue(item)?.ToString() ?? string.Empty;
                                        table.Cell().Text(value);
                                    }
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
