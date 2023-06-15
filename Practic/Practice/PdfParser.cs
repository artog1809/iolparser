using System;
using System.Reflection;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Newtonsoft.Json;

namespace Practice;

public class PdfParser
{

    // Функция для парсинга пятой страницы файла
    public static void FifthPageParser()
    {
        // Считывание текста из файла
        PdfReader reader = new PdfReader("dip.pdf");

        string text = string.Empty;

        text = PdfTextExtractor.GetTextFromPage(reader, 5);


        // Разбиение на строки
        string[] arr = text.Split(new char[] { '\n' });


        // Парсинг блока "Total Keratometry"
        TotalKeratometeryParsing(arr);

        arr = text.Split(new char[] { '\n' });

        // Парсинг блока "Значения задней поверхности роговицы"
        CorneaBackSurfaceParsing(arr);

        arr = text.Split(new char[] { '\n' });

        // Парсинг блока "Прочие значения"

        OtherValueParsing(arr);

    }

    //Функция для парсинга блока "Total Kerotemetry"
    public static void TotalKeratometeryParsing(string[] arr)
    {
        
        string[] result = DataFormatting(arr, "Total Keratometry", "right");


        // Заполнить JSON файл данными класса TotalKeratometery для правого глаза
        TotalKeratometery totalKeratometery = new TotalKeratometery(result[0], result[1], result[2], result[3], result[4],
                                                                    result[5], result[6], result[7], result[8],
                                                                    result[9], result[10], result[11], result[12]);

        File.AppendAllText("patient.json", "\n" + "//TotalKeratometry для правого глаза");
        File.AppendAllText("patient.json", "\n" + JsonConvert.SerializeObject(totalKeratometery));


        result = DataFormatting(arr, "Total Keratometry", "left");


        // Заполнить JSON файл данными класса TotalKeratometery для левого глаза
        TotalKeratometery totalKeratometeryLeft = new TotalKeratometery(result[0], result[1], result[2], result[3], result[4],
                                                                    result[5], result[6], result[7], result[8],
                                                                    result[9], result[10], result[11], result[12]);

        File.AppendAllText("patient.json", "\n" + "//TotalKeratometry для левого глаза");
        File.AppendAllText("patient.json", "\n" + JsonConvert.SerializeObject(totalKeratometeryLeft));
    }

    public static void CorneaBackSurfaceParsing(string[] arr)
    {
        string[] result = DataFormatting(arr, "Значения задней поверхности роговицы", "right");

        // Заполнить JSON файл данными класса CorneaBackSurface для правого глаза
        CorneaBackSurface corneaBackSurface = new CorneaBackSurface(result[0], result[1], result[2], result[3], result[4],
                                                                    result[5], result[6], result[7], result[8],
                                                                    result[9], result[10], result[11], result[12]);

        File.AppendAllText("patient.json", "\n" + "//Значения задней поверхности роговицы для правого глаза");
        File.AppendAllText("patient.json", "\n" + JsonConvert.SerializeObject(corneaBackSurface));

        result = DataFormatting(arr, "Значения задней поверхности роговицы", "left");

        // Заполнить JSON файл данными класса CorneaBackSurface для левого глаза
        CorneaBackSurface corneaBackSurfaceLeft = new CorneaBackSurface(result[0], result[1], result[2], result[3], result[4],
                                                                    result[5], result[6], result[7], result[8],
                                                                    result[9], result[10], result[11], result[12]);

        File.AppendAllText("patient.json", "\n" + "//Значения задней поверхности роговицы для левого глаза");
        File.AppendAllText("patient.json", "\n" + JsonConvert.SerializeObject(corneaBackSurfaceLeft));
    }

    public static void OtherValueParsing(string[] arr)
    {

        string[] result = DataFormatting(arr, "Прочие значения", "right");

        // Заполнить JSON файл данными класса OtherValues для правого глаза
        OtherValues otherValues = new OtherValues(result[0], result[1], result[2], result[3], result[4], result[5], result[6]);

        File.AppendAllText("patient.json", "\n" + "//Прочие значения для правого глаза");
        File.AppendAllText("patient.json", "\n" + JsonConvert.SerializeObject(otherValues));

        result = DataFormatting(arr, "Прочие значения", "left");


        // Заполнить JSON файл данными класса OtherValues для левого глаза
        OtherValues otherValuesLeft = new OtherValues(result[0], result[1], result[2], result[3], result[4], result[5], result[6]);

        File.AppendAllText("patient.json", "\n" + "//Прочие значения для левого глаза");
        File.AppendAllText("patient.json", "\n" + JsonConvert.SerializeObject(otherValuesLeft));


    }


    public static string[] DataFormatting(string[] arr, string nameOfBlock, string eye)
    {
        string[] arr_copy = arr;

        // Поиск нужного блока на странице
        int flagOfBegin = 0;
        for (int i = 0; arr_copy[i] != nameOfBlock; i++)
        {
            flagOfBegin = i;
        }


        // Удалить строки до нужного блока
        for (int i = 0; i <= flagOfBegin; i++)
        {
            arr_copy[i] = string.Empty;
        }

        if(nameOfBlock != "Прочие значения")
        {
            int flagOfEnd = flagOfBegin + 9;

            // Удалить строки после нужного блока
            for (int i = flagOfEnd; i < arr_copy.Length; i++)
            {
                arr_copy[i] = string.Empty;

            }

            arr_copy = arr_copy.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            

            // Удалить часть строки, в зависимости от глаза
            for (int i = 1; i < arr_copy.Length; i++)
            {
                int freq = arr_copy[i].Where(x => (x == ':')).Count();
                if (eye == "right")
                    if (freq > 3)
                    arr_copy[i] = arr_copy[i].Substring(0, arr_copy[i].Length / 2);
                else
                    arr_copy[i] = arr_copy[i].Substring(arr_copy[i].Length/2);

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

            // Удалить строки после нужного блока
            int flagOfEnd = flagOfBegin + 6;

            for (int i = flagOfEnd; i < arr_copy.Length; i++)
            {
                arr_copy[i] = string.Empty;

            }

            arr_copy = arr_copy.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            // Удалить часть строки, в зависимости от глаза 
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


            // Выделить в каждую строку подстроки формата "AA: 'значение'"
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


        // Дополнительное форматирование текста для блока "Прочие значения"
        if (nameOfBlock == "Прочие значения")
        {
            string buff = result[6];

            word = result[5].Split(new char[] { ' ' });

            word = word.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            result[5] = string.Empty;
            result[6] = string.Empty;

            result = result.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            result = result.Concat(word).ToArray();


            buff = buff.Remove(6, 1).Insert(6, '.'.ToString());
            word = buff.Split(new char[] { '.' });

            result[5] = result[5].Insert(2, word[0]);

            result[6] = result[6].Insert(9, word[1]);

        }

        // Привести текст к удобному виду, для парсинга в json
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