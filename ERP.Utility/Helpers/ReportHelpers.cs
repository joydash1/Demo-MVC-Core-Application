using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
namespace ERP.Utility.Helpers
{
    public static class ReportHelpers
    {
        static string CompanyName = "United Corporate Advisory Service Ltd.";
        static string CompanyAddress = "Bijoy Nagar, Kakrail, Dhaka";
        static string MobileNo = "+88015364578, +8801345759842";
        static string CompanyLogo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/user/avatar1.jpg");
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
        public static byte[] GeneratePdfReport<T>(IEnumerable<T> data, string ReportName, bool isLandscape)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    if (isLandscape)
                        page.Size(842, 595, Unit.Point);
                    else
                        page.Size(PageSizes.A4); 
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontFamily("Arial Narrow").FontSize(10));

                    page.Header()
                        .Column(column =>
                        {
                            column.Item().Row(row =>
                            {
                                row.RelativeItem().Column(col =>
                                {
                                    col.Item().Text(CompanyName).SemiBold().FontSize(14);
                                    col.Item().Text(CompanyAddress).FontSize(12);
                                    col.Item().Text("Mobile: " + MobileNo).FontSize(10);
                                });

                                if (!string.IsNullOrEmpty(CompanyLogo))
                                {
                                    row.ConstantItem(100).Image(CompanyLogo);
                                }
                            });

                            column.Item().LineHorizontal(1);
                            column.Item().AlignCenter().Text(ReportName).Bold().FontSize(12);
                        });

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(column =>
                        {
                            column.Item().Table(table =>
                            {
                                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                                table.ColumnsDefinition(columns =>
                                {
                                    foreach (var _ in properties)
                                    {
                                        columns.RelativeColumn();
                                    }
                                });
                                table.Header(header =>
                                {
                                    foreach (var property in properties)
                                    {
                                        header.Cell().Text(property.Name).SemiBold();
                                    }
                                });
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

                    // Footer
                    page.Footer()
                          .Column(column =>
                          {
                              column.Item().LineHorizontal(1);

                              column.Item().Row(row =>
                              {
                                  row.RelativeItem().Text($"Printed Date: {DateTime.UtcNow:dd-MM-yyyy}");
                                  row.RelativeItem().AlignRight().Text(x =>
                                  {
                                      x.Span("Page ");
                                      x.CurrentPageNumber();
                                  });
                              });
                          });
                });
            });
            return document.GeneratePdf();
        }
    }
}
