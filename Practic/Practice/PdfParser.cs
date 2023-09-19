using System;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        // Парсинг блока "Значения роговицы"
        CorneaValuesParsing(arr.Skip(20).ToArray(), gd);

        // Парсинг блока "Total Keratometry"
        TotalKeratometeryParsing(arr, gd);

        arr = text.Split(new char[] { '\n' });

        // Парсинг блока "Значения задней поверхности роговицы"
        CorneaBackSurfaceParsing(arr, gd);

        arr = text.Split(new char[] { '\n' });

        // Парсинг блока "Прочие значения"

        OtherValueParsing(arr, gd);
        

        JsonSerializerOptions options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };

        string jsonString = System.Text.Json.JsonSerializer.Serialize(gd, options);


        File.WriteAllText(output_name,jsonString);


    }

    // Функция для парсинга блока "Значения роговицы"
    public static void CorneaValuesParsing(string[] arr, GeneralData gd)
    {
        string[] result = DataFormatting(arr, "Значения роговицы", "right");

        Cornea rogovicaOD = new Cornea
        {
            SE = result[0],
            SD1 = result[1],
            K1 = result[2],
            SD2 = result[3],
            K2 = result[4],
            SD3 = result[5],
            DK = result[6],
            SE1 = result[7],
            DK1 = result[8],
            SE2 = result[9],
            DK2 = result[10],
            SE3 = result[11],
            DK3 = result[12]
        };

        

        result = DataFormatting(arr, "Значения роговицы", "left");

        Cornea rogovicaOS = new Cornea
        {
            SE = result[0],
            SD1 = result[1],
            K1 = result[2],
            SD2 = result[3],
            K2 = result[4],
            SD3 = result[5],
            DK = result[6],
            SE1 = result[7],
            DK1 = result[8],
            SE2 = result[9],
            DK2 = result[10],
            SE3 = result[11],
            DK3 = result[12]
        };


        gd.rogovicaOD = rogovicaOD;
        gd.rogovicaOS = rogovicaOS;
    }

    // Функция для парсинга блока "Total Kerotemetry"
    public static void TotalKeratometeryParsing(string[] arr, GeneralData gd)
    {
        string[] result = DataFormatting(arr, "Total Keratometry", "right");

        // Заполнить JSON файл данными класса TotalKeratometery
        TotalKeratometery TotalKeratometeryOD = new TotalKeratometery
        {
            TSE = result[0],
            SD1 = result[1],
            TK1 = result[2],
            SD2 = result[3],
            TK2 = result[4],
            SD3 = result[5],
            DTK = result[6],
            TSE1 = result[7],
            DTK1 = result[8],
            TSE2 = result[9],
            DTK2 = result[10],
            TSE3 = result[11],
            DTK3 = result[12]
        };


        result = DataFormatting(arr, "Total Keratometry", "left");

        TotalKeratometery TotalKeratometeryOS = new TotalKeratometery
        {
            TSE = result[0],
            SD1 = result[1],
            TK1 = result[2],
            SD2 = result[3],
            TK2 = result[4],
            SD3 = result[5],
            DTK = result[6],
            TSE1 = result[7],
            DTK1 = result[8],
            TSE2 = result[9],
            DTK2 = result[10],
            TSE3 = result[11],
            DTK3 = result[12]
        };


        gd.TotalKeratometeryOD = TotalKeratometeryOD;
        gd.TotalKeratometeryOS = TotalKeratometeryOS;
    }

    public static void CorneaBackSurfaceParsing(string[] arr, GeneralData gd)
    {
        string[] result = DataFormatting(arr, "Значения задней поверхности роговицы", "right");

        // Заполнить JSON файл данными класса TotalKeratometery
        CorneaBackSurface zadRogovicaOD = new CorneaBackSurface
        {
            PSE = result[0],
            SD1 = result[1],
            PK1 = result[2],
            SD2 = result[3],
            PK2 = result[4],
            SD3 = result[5],
            DPK = result[6],
            PSE1 = result[7],
            DPK1 = result[8],
            PSE2 = result[9],
            DPK2 = result[10],
            PSE3 = result[11],
            DPK3 = result[12]
        };


        result = DataFormatting(arr, "Значения задней поверхности роговицы", "left");

        // Заполнить JSON файл данными класса TotalKeratometery
        CorneaBackSurface zadRogovicaOS = new CorneaBackSurface
        {
            PSE = result[0],
            SD1 = result[1],
            PK1 = result[2],
            SD2 = result[3],
            PK2 = result[4],
            SD3 = result[5],
            DPK = result[6],
            PSE1 = result[7],
            DPK1 = result[8],
            PSE2 = result[9],
            DPK2 = result[10],
            PSE3 = result[11],
            DPK3 = result[12]
        };

        gd.zadRogovicaOD = zadRogovicaOD;
        gd.zadRogovicaOS = zadRogovicaOS;
    }

    public static void OtherValueParsing(string[] arr, GeneralData gd)
    {
        string[] result = DataFormatting(arr, "Прочие значения", "right");


        OtherValues otherOD = new OtherValues
        {
            CCT = result[0],
            SD = result[1],
            WTW = result[2],
            Ix = result[3],
            Iy = result[4],
            P = result[5],
            CWChord = result[6],
        };


        result = DataFormatting(arr, "Прочие значения", "left");

        OtherValues otherOS = new OtherValues
        {
            CCT = result[0],
            SD = result[1],
            WTW = result[2],
            Ix = result[3],
            Iy = result[4],
            P = result[5],
            CWChord = result[6],
        };


        gd.otherOD = otherOD;
        gd.otherOS = otherOS;
    }


    public static string[] DataFormatting(string[] arr, string nameOfBlock, string eye)
    {
        string[] arr_copy = arr;


        int flagOfBegin = 0;
        for (int i = 0; arr_copy[i] != nameOfBlock; i++)
        {
            flagOfBegin = i;
        }

        for (int i = 0; i <= flagOfBegin; i++)
        {
            arr_copy[i] = string.Empty;
        }
        arr_copy = arr_copy.Where(x => !string.IsNullOrEmpty(x)).ToArray();

        if (nameOfBlock != "Прочие значения")
        {
            if (nameOfBlock == "Значения роговицы")
            {
                arr_copy[7] = arr_copy[8];
                
            }
           
            int flagOfEnd =  8;

            for (int i = flagOfEnd; i < arr_copy.Length; i++)
            {
                arr_copy[i] = string.Empty;
            }

            arr_copy = arr_copy.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            for (int i = 1; i < arr_copy.Length; i++)
            {
                if (eye == "right")
                {
                    if (nameOfBlock == "Значения роговицы" && i == 1)
                    {
                        arr_copy[i] = arr_copy[i].Substring(0, arr_copy[i].Length / 2 + 1);
                    }
                    else
                    {
                        arr_copy[i] = arr_copy[i].Substring(0, arr_copy[i].Length / 2);
                    }
                }
                else
                {
                    if (nameOfBlock == "Значения роговицы" && i == 1)
                    {
                        arr_copy[i] = arr_copy[i].Substring(arr_copy[i].Length / 2 + 1);
                    }
                    else
                    {
                        arr_copy[i] = arr_copy[i].Substring(arr_copy[i].Length / 2);
                    }
                }
            }

            int index = 0;
            int indexOfSpace = 0;

            // Выделить в каждую строку подстроки формата "AA: 'значение'"
            for (int i = 1; i < arr_copy.Length; i++)
            {
                index = arr_copy[i].LastIndexOf(':');

                if (arr_copy[i].Length > 25)
                {
                    for (int j = index; arr_copy[i][j] != ' '; j--)
                    {
                        indexOfSpace = j;
                    }

                    arr_copy[i] = arr_copy[i].Remove(indexOfSpace - 1, 1).Insert(indexOfSpace - 1, '.'.ToString());
                }
            }
        }
        else
        {
            int flagOfEnd = flagOfBegin + 6;

            for (int i = flagOfEnd; i < arr_copy.Length; i++)
            {
                arr_copy[i] = string.Empty;
            }

            arr_copy = arr_copy.Where(x => !string.IsNullOrEmpty(x)).ToArray();


            for (int i = 1; i < arr_copy.Length; i++)
            {
                if (eye == "right")
                    arr_copy[i] = arr_copy[i].Substring(0, arr_copy[i].Length / 2);
                else
                    arr_copy[i] = arr_copy[i].Substring((arr_copy[i].Length / 2));
            }

            int index = 0;
            int indexOfSpace = 0;

            index = arr_copy[1].LastIndexOf(':');


            for (int j = index; arr_copy[1][j] != ' '; j--)
            {
                indexOfSpace = j;
            }

            arr_copy[1] = arr_copy[1].Remove(indexOfSpace - 1, 1).Insert(indexOfSpace - 1, '.'.ToString());


            index = arr_copy[2].IndexOf("x: ");

            for (int j = index; arr_copy[2][j] != ' '; j--)
            {
                indexOfSpace = j;
            }

            arr_copy[2] = arr_copy[2].Remove(indexOfSpace - 1, 1).Insert(indexOfSpace - 1, '.'.ToString());

            index = arr_copy[2].IndexOf("y: ");

            for (int j = index; arr_copy[2][j] != ' '; j--)
            {
                indexOfSpace = j;
            }

            arr_copy[2] = arr_copy[2].Remove(indexOfSpace - 1, 1).Insert(indexOfSpace - 1, '.'.ToString());
        }


        // Буферные массивы для вспомоготальных действий
        string[] word = Array.Empty<string>();
        string[] secword = Array.Empty<string>();

        string[] result = Array.Empty<string>();
        string[] result1 = Array.Empty<string>();


        // Форматирование данных для корректного отображения в JSON файле
        for (int i = 1; i < arr_copy.Length; i += 2)
        {
            word = arr_copy[i].Split(new char[] { '.' });

            if (i != arr_copy.Length - 1)
            {
                secword = arr_copy[i + 1].Split(new char[] { '.' });
                result1 = word.Concat(secword).ToArray();
            }
            else
            {
                result1 = word;
            }

            result = result.Concat(result1).ToArray();
        }


        if (nameOfBlock == "Прочие значения")
        {
            string buff = result[6];

            word = result[5].Split(new char[] { ' ' });

            word = word.Where(x => !string.IsNullOrEmpty(x)).ToArray();
           

            for (int i = 5; i < result.Length; i++)
            {
                result[i] = string.Empty;
            }

            result = result.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            result = result.Concat(word).ToArray();


            buff = buff.Remove(6, 1).Insert(6, '.'.ToString());
            word = buff.Split(new char[] { '.' });

            result[5] = result[5].Insert(2, word[0]);

            result[6] = result[6].Insert(9, word[1]);
        }

        for (int i = 0; i < result.Length; i++)
        {
            int index = result[i].LastIndexOf(':');
            result[i] = result[i].Remove(0, index + 1);
            for (int j = 0; j < result[i].Length; j++)
            {
                if (result[i][0] == ' ')
                {
                    result[i] = result[i].Remove(0, 1);
                }
            }
        }

        return result;
    }
}