using System;
using System.Reflection.PortableExecutable;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using Org.BouncyCastle.Asn1.X509.SigI;

namespace Praktik
{
    public static class PdfExtractor
    {

        static void Main(string[] args)
        {

            //Выгрузка текста с пдф
            string[] input = pdfReading();
            string[] word = input[2].Split(new char[] { ' ' });

            Pacient newPacient = new Pacient(input[1], word[2], word[4], input[5]);

            File.WriteAllText("pacient.json", JsonConvert.SerializeObject(newPacient));


            Console.WriteLine(newPacient.Name + "\n" + newPacient.BirthDate + "\n" + newPacient.Sex + "\n" + newPacient.PacientId);

        }


        public static string[] pdfReading()
        {
            PdfReader reader = new PdfReader("praktik.pdf");
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

    class Pacient
    {
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string Sex { get; set; }
        public string PacientId { get; set; }

        public Pacient(string name, string birthDate, string sex, string pacientId)
        {
            Name = name;
            BirthDate = birthDate;
            Sex = sex;
            PacientId = pacientId;
        }
    }
}
