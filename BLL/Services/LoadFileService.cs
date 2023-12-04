using BLL.Interfaces;
using DAL.Interfaces;
using DAL.ReportForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Windows;
using BLL.DTO;

namespace BLL.Services
{
    public class LoadFileService : ILoadFileService
    {
        private IDbRepos db;
        public LoadFileService(IDbRepos db)
        {
            this.db = db;
        }

        public void SaveStatisticForAllPerson(string filename, List<ReportStatisticByAllPersonDTO> data, string header)
        {
            Document document = new Document();

            PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create));

            document.Open();

            BaseFont baseFont = BaseFont.CreateFont(@"D:\Лабораторные Фомина\ProjectSystems\BLL\TimesNewRooman.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font font = new Font(baseFont, Font.DEFAULTSIZE, Font.NORMAL);

            PdfPTable table = new PdfPTable(4);

            Paragraph headerPDF = new Paragraph(header, font);
            headerPDF.Capacity = 4;
            headerPDF.Alignment = 1;
            document.Add(headerPDF);

            Paragraph temp = new Paragraph(" ", font);
            document.Add(temp);


            table.AddCell(new PdfPCell(new Phrase(new Phrase("Работник", font))));
            table.AddCell(new PdfPCell(new Phrase(new Phrase("Должность", font))));
            table.AddCell(new PdfPCell(new Phrase(new Phrase("Количество часов", font))));
            table.AddCell(new PdfPCell(new Phrase(new Phrase("Количество выполненных заданий", font))));

            foreach (ReportStatisticByAllPersonDTO item in data)
            {
                table.AddCell(new Phrase(item.Person, font));
                table.AddCell(new Phrase(item.Position, font));
                table.AddCell(new Phrase(item.CountHours.ToString(), font));
                table.AddCell(new Phrase(item.CountCompletedTasks.ToString(), font));
            }

            document.Add(table);

            Paragraph record = new Paragraph("\nПодпись ____________", font);
            record.Capacity = 4;
            record.Alignment = 2;

            document.Add(record);

            document.Close();

        }

        public void SaveStatisitcForCurrentTask(string filename, List<StatisticTrackDTO> data, string header)
        {
            Document document = new Document();

            PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create));

            document.Open();

            BaseFont baseFont = BaseFont.CreateFont(@"D:\Лабораторные Фомина\ProjectSystems\BLL\TimesNewRooman.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font font = new Font(baseFont, Font.DEFAULTSIZE, Font.NORMAL);

            PdfPTable table = new PdfPTable(2);

            Paragraph headerPDF = new Paragraph(header, font);
            headerPDF.Capacity = 4;
            headerPDF.Alignment = 1;
            document.Add(headerPDF);

            Paragraph temp = new Paragraph(" ", font);
            document.Add(temp);


            table.AddCell(new PdfPCell(new Phrase(new Phrase("Стадия проекта", font))));
            table.AddCell(new PdfPCell(new Phrase(new Phrase("Количество часов", font))));

            foreach (StatisticTrackDTO item in data)
            {
                table.AddCell(new Phrase(item.NameStatus, font));
                table.AddCell(new Phrase(item.CountHours.ToString(), font));
            }

            document.Add(table);

            Paragraph record = new Paragraph("\nПодпись ____________", font);
            record.Capacity = 4;
            record.Alignment = 2;

            document.Add(record);

            document.Close();
        }
    }
}
