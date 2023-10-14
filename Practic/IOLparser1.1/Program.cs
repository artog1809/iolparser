using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Newtonsoft.Json;

namespace IOLparser1._1
{
    public static class Program
    {
        public static GeneralData? gd = new GeneralData();
        public static void Parsing(string path)
        {
            string[] input = pdfReading(path);
            string[] word = input[2].Split(new char[] { ' ' });
            string[] word1 = input[12].Split(new char[] { ' ' });

            PdfParser.FifthPageParser(path, gd);

            Patient newPatient = new Patient
            {
                Name = input[1],
                BirthDate = word[2],
                Sex = word[4],
                PatientId = input[5],
                DateOfMeasurement = word1[2]
            };

            gd.patient = newPatient;

            PdfParser.FifthPageParser(path, gd);
        }

        public static string[] pdfReading(string path)
        {
            PdfReader reader = new PdfReader(path);
            string text = string.Empty;

            text = PdfTextExtractor.GetTextFromPage(reader, 1);

            reader.Close();

            string[] arr = text.Split(new char[] { '\n' });
            return arr;
        }
    }
}