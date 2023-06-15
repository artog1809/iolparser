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

            Patient newPatient = new Patient(input[1], DateTime.Parse(word[2]), word[4], input[5]);
            
            var (eye1, eye2) = PdfParser.ParseFourthPage();

            var fileName = "patient.json";
            
            File.WriteAllText(fileName, "// Информация о пациенте\n");
            File.AppendAllText(fileName, JsonConvert.SerializeObject(newPatient));
            
            File.AppendAllText(fileName, "\n// Левый глаз\n");
            File.AppendAllText(fileName, JsonConvert.SerializeObject(eye1));
            
            File.AppendAllText(fileName, "\n// Правый глаз\n");
            File.AppendAllText(fileName, JsonConvert.SerializeObject(eye2));
            
            Console.WriteLine(newPatient.Name + "\n" + newPatient.BirthDate + "\n" + newPatient.Sex + "\n" +
                              newPatient.PatientId);
        }

        public static string[] pdfReading()
        {
            PdfReader reader = new PdfReader("dip.pdf");
            string text = string.Empty;
            for (int page = 1; page <= reader.NumberOfPages; page++)
            {
                text += PdfTextExtractor.GetTextFromPage(reader, page);
            }

            reader.Close();

            string[] arr = text.Split(new char[] { '\n' });
            return arr;
        }
    }
}