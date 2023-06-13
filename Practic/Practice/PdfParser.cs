using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Newtonsoft.Json;

namespace Practice;

public class PdfParser
{

    public static string[] FifthPageParser()
    {

        PdfReader reader = new PdfReader("dip.pdf");

        string text = string.Empty;

        text = PdfTextExtractor.GetTextFromPage(reader, 5);

        string[] arr = text.Split(new char[] { '\n' });

        string[] arr_copy = arr;

        for (int i = 0; i < 32; i++)
        {
            arr_copy[i] = string.Empty;
        }

        arr_copy = arr_copy.Where(x => !string.IsNullOrEmpty(x)).ToArray();

        for (int i = 21; i < 25; i++)
        {
            arr_copy[i] = string.Empty;
        }

        arr_copy = arr_copy.Where(x => !string.IsNullOrEmpty(x)).ToArray();

        arr = arr_copy;



        string[] ttkparsed = TotalKeratometeryParsing(arr);

        // string[] cbsparsed = CorneaBackSurfaceParsing(arr);


        //string[] ovparsed = OtherValueParsing(arr);


        return ttkparsed;
    }


    public static string[] TotalKeratometeryParsing(string[] arr)
    {
        string[] arr_copy = arr;

        for (int i = 8; i < arr_copy.Length; i++)
        {
            arr_copy[i] = string.Empty;
        }

        arr_copy = arr_copy.Where(x => !string.IsNullOrEmpty(x)).ToArray();

        for (int i = 1; i < arr_copy.Length; i++)
        {
            arr_copy[i] = arr_copy[i].Substring(0,arr_copy[i].Length / 2);
        }

        arr = arr_copy;

        int index = 0;
        int indexOfSpace = 0;
        string[] word = Array.Empty<string>();
        string[] secword = Array.Empty<string>();

        string[] result = Array.Empty<string>();
        string[] result1 = Array.Empty<string>();

        List<string> list = new List<string>();

        for (int i = 1; i < arr_copy.Length; i ++)
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

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = result[i].Remove(0, 4);
            if (result[i][0] == ' ')
            {
                result[i] = result[i].Remove(0, 1);
            }
        }


        TotalKeratometery totalKeratometery = new TotalKeratometery(result[0], result[1], result[2], result[3], result[4],
                                                                    result[5], result[6], result[7], result[8],
                                                                    result[9], result[10], result[11], result[12]);
        File.AppendAllText("patient.json", "\n" + "//TotalKeratometry");
        File.AppendAllText("patient.json", "\n" + JsonConvert.SerializeObject(totalKeratometery));

        //arr = arr_copy;
        arr = result;

        return arr;
    }

    public static string[] CorneaBackSurfaceParsing(string[] arr)
    {
        string[] arr_copy = arr;

        for (int i = 0; i < 8; i++)
        {
            arr_copy[i] = string.Empty;
        }

        arr_copy = arr_copy.Where(x => !string.IsNullOrEmpty(x)).ToArray();


        for (int i = 8; i < arr_copy.Length; i++)
        {
            arr_copy[i] = string.Empty;
        }

        for (int i = 1; i < arr_copy.Length; i++)
        {
            arr_copy[i] = arr_copy[i].Substring(0, arr_copy[i].Length / 2);
        }

        arr = arr_copy;

        return arr;
    }

    public static string[] OtherValueParsing(string[] arr)
    {
        string[] arr_copy = arr;

        for (int i = 0; i < 16; i++)
        {
            arr_copy[i] = string.Empty;
        }

        arr_copy = arr_copy.Where(x => !string.IsNullOrEmpty(x)).ToArray();


        arr = arr_copy;

        return arr;
    }

}