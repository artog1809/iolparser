using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Newtonsoft.Json;

namespace Practice
{
    public static class Program
    {
        static void Main(string[] args)
        {
            // Выгрузка текста с пдф
            string[] input = pdfReading();
            string[] word = input[2].Split(new char[] { ' ' });

            Patient newPatient = new Patient(input[1], word[2], word[4], input[5]);

            File.WriteAllText("patient.json", JsonConvert.SerializeObject(newPatient));


            string[] arr = PdfParser.FifthPageParser();
        }


        public static string[] pdfReading()
        {   
            PdfReader reader = new PdfReader("dip.pdf");
            string text = string.Empty;

            text = PdfTextExtractor.GetTextFromPage(reader, 1);

            reader.Close();

            string[] arr = text.Split(new char[] { '\n' });
            return arr;
        }
    }
}