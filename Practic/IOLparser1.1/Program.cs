using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Newtonsoft.Json;

namespace IOLparser1._1
{
    public static class Program
    {
        public static void Parsing(string path)
        {
            string[] input = pdfReading(path);
            string[] word = input[2].Split(new char[] { ' ' });

            Patient newPatient = new Patient(input[1], word[2], word[4], input[5]);

            string output_name = path.Remove(path.Length - 3) + "json";

            File.WriteAllText(output_name, JsonConvert.SerializeObject(newPatient));

            PdfParser.FifthPageParser(path, output_name);
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