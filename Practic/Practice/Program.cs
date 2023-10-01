using System.Text.Json;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf.qrcode;
using Newtonsoft.Json;

namespace Practice
{
    public static class Program
    {
        public static string? output_name;
        public static GeneralData? gd = new GeneralData();

        static void Main(string[] args)
        {
            // Выгрузка текста с пдф
            string[] input = pdfReading();

            string[] word = input[2].Split(new char[] { ' ' });


            Patient newPatient = new Patient
            {
                Name = input[1],
                BirthDate = word[2],
                Sex = word[4],
                PatientId = input[5]
            };

            gd.patient = newPatient;

            PdfParser.FifthPageParser(gd);

        }


        public static string[] pdfReading()
        {
            output_name = "test3";
            PdfReader reader = new PdfReader(output_name + ".pdf");
            string text = string.Empty;

            text = PdfTextExtractor.GetTextFromPage(reader, 1);

            reader.Close();

            string[] arr = text.Split(new char[] { '\n' });
            return arr;
        }
    }
}