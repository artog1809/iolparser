using System;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Net.Mime.MediaTypeNames;
using iTextSharp.text;

namespace Practice;

public class PdfParser
{
    public static string output_name = "test1.json";


    // Функция для парсинга пятой страницы файла
    public static void FifthPageParser(GeneralData gd)
    {
        // Считывание текста из файла
        PdfReader reader = new PdfReader("test1.pdf");

        string text = string.Empty;

        text = PdfTextExtractor.GetTextFromPage(reader, 5);

        // Разбиение на строки
        string[] arr = text.Split(new char[] { '\n' });

        //Regex regex = new Regex(@"SE: -?\d{1,2},\d{1,2} дптр");
        //MatchCollection matches = regex.Matches(text);


        // Парсинг блока "Значения роговицы"
        CorneaValuesParsing(arr.Skip(20).ToArray(), gd);

        // Парсинг блока "Total Keratometry"
        //TotalKeratometeryParsing(arr, gd);

        //arr = text.Split(new char[] { '\n' });

        // Парсинг блока "Значения задней поверхности роговицы"
        //CorneaBackSurfaceParsing(arr, gd);

        //arr = text.Split(new char[] { '\n' });

        // Парсинг блока "Прочие значения"

        //OtherValueParsing(arr, gd);

        //text = PdfTextExtractor.GetTextFromPage(reader, 1);
        // Разбиение на строки
        //string[] arr1 = text.Split(new char[] { '\n' });


        //if(arr1.Length > 60)
        //    BioIndicatorsParsing(arr1, gd);


        JsonSerializerOptions options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };

        string jsonString = System.Text.Json.JsonSerializer.Serialize(gd, options);


        File.WriteAllText(output_name, jsonString);


    }


    public static Stack<System.Text.RegularExpressions.Match> RegExp(string elemForParsing, string[] arr, string typeOfEye)
    {
        if(typeOfEye == "OD")
        {
            Stack<System.Text.RegularExpressions.Match> tmpOD = new Stack<System.Text.RegularExpressions.Match>(5);
            Regex regexOD = new Regex($@"^{elemForParsing}: -?\d{{1,2}},\d{{1,2}}\sдптр");
            Regex regex = new Regex(@"-?\d{1,2},\d{1,2}\sдптр(@\s\d{1,3}.{1}|)");
            MatchCollection matches = regexOD.Matches(arr[0]);
            for (int i = 1; i < arr.Length; i++)
            {
                matches = regexOD.Matches(arr[i]);
                if (matches.Count > 0)
                {
                    foreach (Match match in matches)
                    {
                        tmpOD.Push(regex.Match(match.Value));
                    }
                }
            }
            return tmpOD;
        }
        else
        {
            Stack<System.Text.RegularExpressions.Match> tmpOS = new Stack<System.Text.RegularExpressions.Match>(5);
            Regex seRegexOS = new Regex($@"\s{elemForParsing}: -?\d{{1,2}},\d{{1,2}}\sдптр");
            Regex regex = new Regex(@"-?\d{1,2},\d{1,2}\sдптр(@\s\d{1,3}.{1}|)");
            MatchCollection matches = seRegexOS.Matches(arr[0]);
            for (int i = 1; i < arr.Length; i++)
            {
                matches = seRegexOS.Matches(arr[i]);
                if (matches.Count > 0)
                {
                    foreach (Match match in matches)
                        tmpOS.Push(regex.Match(match.Value));
                }
            }
            return tmpOS;

        }

    }


    public static void CorneaValuesParsing(string[] arr, GeneralData gd)
    {


        Stack<System.Text.RegularExpressions.Match> tmp = RegExp("SE", arr, "OS");

        Console.WriteLine(tmp);

        //Stack<System.Text.RegularExpressions.Match> tmpOD = new Stack<System.Text.RegularExpressions.Match>(5);
        //Regex seRegexOD = new Regex(@"^SE: -?\d{1,2},\d{1,2}\sдптр");
        //Regex regex = new Regex(@"-?\d{1,2},\d{1,2}\sдптр(@\s\d{1,3}.{1}|)");
        //MatchCollection matches = seRegexOD.Matches(arr[0]);
        //for (int i = 1; i < arr.Length; i++)
        //{
        //    matches = seRegexOD.Matches(arr[i]);
        //    if (matches.Count > 0)
        //    {
        //        foreach (Match match in matches)
        //        {
        //            tmpOD.Push(regex.Match(match.Value));
        //        }
        //    }
        //}




        //Regex k1RegexOD = new Regex(@"^K1: -?\d{1,2},\d{1,2}\sдптр@\s\d{1,3}.{1}");
        //matches = k1RegexOD.Matches(arr[0]);
        //for (int i = 1; i < arr.Length; i++)
        //{
        //    matches = k1RegexOD.Matches(arr[i]);
        //    if (matches.Count > 0)
        //    {
        //        foreach (Match match in matches)
        //        {
        //            tmpOD.Push(regex.Match(match.Value));
        //        }
        //    }
        //}

        //Cornea rogovicaOD = new Cornea
        //{
        //    K1 = (tmpOD.Pop()).ToString(),
        //    SE3 = (tmpOD.Pop()).ToString(),
        //    SE2 = (tmpOD.Pop()).ToString(),
        //    SE1 = (tmpOD.Pop()).ToString(),
        //    SE = (tmpOD.Pop()).ToString()
        //};

        //gd.rogovicaOD = rogovicaOD;


        //Stack<System.Text.RegularExpressions.Match> tmpOS = new Stack<System.Text.RegularExpressions.Match>(5);
        //Regex seRegexOS = new Regex(@"\sSE: -?\d{1,2},\d{1,2}\sдптр");
        //matches = seRegexOS.Matches(arr[0]);
        //for (int i = 1; i < arr.Length; i++)
        //{
        //    matches = seRegexOS.Matches(arr[i]);
        //    if (matches.Count > 0)
        //    {
        //        foreach (Match match in matches)
        //            tmpOS.Push(regex.Match(match.Value));
        //    }
        //}

        //Cornea rogovicaOS = new Cornea
        //{
        //    SE3 = (tmpOS.Pop()).ToString(),
        //    SE2 = (tmpOS.Pop()).ToString(),
        //    SE1 = (tmpOS.Pop()).ToString(),
        //    SE = (tmpOS.Pop()).ToString()
        //};

        //gd.rogovicaOS = rogovicaOS;


        //    Cornea rogovicaOD = new Cornea
        //    {
        //        SE = result[0],
        //        SD1 = result[1],
        //        K1 = result[2],
        //        SD2 = result[3],
        //        K2 = result[4],
        //        SD3 = result[5],
        //        DK = result[6],
        //        SE1 = result[7],
        //        DK1 = result[8],
        //        SE2 = result[9],
        //        DK2 = result[10],
        //        SE3 = result[11],
        //        DK3 = result[12]
        //    };



        //    result = DataFormatting(arr, "Значения роговицы", "left");

        //    Cornea rogovicaOS = new Cornea
        //    {
        //        SE = result[0],
        //        SD1 = result[1],
        //        K1 = result[2],
        //        SD2 = result[3],
        //        K2 = result[4],
        //        SD3 = result[5],
        //        DK = result[6],
        //        SE1 = result[7],
        //        DK1 = result[8],
        //        SE2 = result[9],
        //        DK2 = result[10],
        //        SE3 = result[11],
        //        DK3 = result[12]
        //    };


        //    gd.rogovicaOD = rogovicaOD;
        //    gd.rogovicaOS = rogovicaOS;
        //}





    }
}