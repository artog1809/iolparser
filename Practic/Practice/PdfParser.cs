using System.Text.RegularExpressions;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Practice.Indications;

namespace Practice;

public record FourthEyeInfo(EyeStatus EyeStatus, Biometric Biometric);

public static class PdfParser
{
    public static ValueTuple<FourthEyeInfo, FourthEyeInfo> ParseFourthPage()
    {
        var text = string.Empty;
        using (var reader = new PdfReader("dip.pdf"))
        {
            text += PdfTextExtractor.GetTextFromPage(reader, 4);
        }

        var (eye1, eye2) = ExtractEyeStatus(text);
        var (biometric1, biometric2) = ExtractBiometric(text);

        return new ValueTuple<FourthEyeInfo, FourthEyeInfo>(new FourthEyeInfo(eye1, biometric1), 
            new FourthEyeInfo(eye2, biometric2));
    }

    private static (EyeStatus, EyeStatus) ExtractEyeStatus(string text)
    {
        string pattern0 = @"Статус глаза(?:(?!Биометрические показатели)[\s\S])*Биометрические показатели";

        var mainText = Regex.Match(text, pattern0).Value;

        string pattern1 = @"LS:\s*(.*?)\sVS:\s*";
        Match match1 = Regex.Match(text, pattern1);
        var ls1 = match1.Value.Split(' ')[1];
        
        string pattern2 = @"VS:\s*(.*?)\sLS:\s*";
        Match match2 = Regex.Match(text, pattern2);
        var vs1 = match2.Value.Split(' ')[1];

        string pattern3 = @"LS:\s*(.*?)\sVS:\s*";
        Match match3 = Regex.Matches(text, pattern3)[1];
        var ls2 = match3.Value.Split(' ')[1];

        string pattern4 = @"VS:\s*(.*?)\s";
        Match match4 = Regex.Matches(text, pattern4)[1];
        var vs2 = match4.Value.Split(' ')[1];
        
        var secondLine = mainText.Split('\n')[3];
        var ref1_va1_ref2_va2 = secondLine.Split(' ');
        
        var thirdLine = mainText.Split('\n' )[4];
        var lvc_raw = thirdLine.Split(' ');
        var lvc1 = lvc_raw[1];
        var lvc2 = lvc_raw[3];
        
        return (new EyeStatus(ls1, vs1, ref1_va1_ref2_va2[0], lvc1, ref1_va1_ref2_va2[1]),
            new EyeStatus(ls2, vs2, ref1_va1_ref2_va2[2], lvc2, ref1_va1_ref2_va2[3]));
    }

    private static (Biometric, Biometric) ExtractBiometric(string text)
    {
        string pattern0 = @"Биометрические показатели[\s\S]*Значения роговицы";
        var mainText = Regex.Match(text, pattern0).Value;
        
        var sd1 = new List<string>();
        var sd2 = new List<string>();
        
        var al1 = new List<string>();
        var cct1 = new List<string>();
        var acd1 = new List<string>();
        var lt1 = new List<string>();
        
        var al2 = new List<string>();
        var cct2 = new List<string>();
        var acd2 = new List<string>();
        var lt2 = new List<string>();
        
        var line9 = mainText.Split('\n')[9];
        var al_mean1 = string.Concat(line9.Split(' ')[1], line9.Split(' ')[2]);
        sd1.Add(string.Concat(line9.Split(' ')[4], line9.Split(' ')[5]));

        var al_mean2 = string.Concat(line9.Split(' ')[7], line9.Split(' ')[8]);
        sd2.Add(string.Concat(line9.Split(' ')[10], line9.Split(' ')[11]));
        
        var line10 = mainText.Split('\n')[10];
        var cct_mean1 = string.Concat(line10.Split(' ')[1], line10.Split(' ')[2]);

        string cct_mean2;
        if (line10.Split(' ')[3] == "(!)")
        {
            sd1.Add(string.Concat(line10.Split(' ')[5], line10.Split(' ')[6]));
            cct_mean2 = string.Concat(line10.Split(' ')[8], line10.Split(' ')[9]);
            sd2.Add(string.Concat(line10.Split(' ')[11], line10.Split(' ')[12]));
        }
        else
        {
            sd1.Add(string.Concat(line10.Split(' ')[4], line10.Split(' ')[5]));
            cct_mean2 = string.Concat(line10.Split(' ')[7], line10.Split(' ')[8]);
            sd2.Add(string.Concat(line10.Split(' ')[10], line10.Split(' ')[11]));
        }

        var line11 = mainText.Split('\n')[11];
        var acd_mean1 = string.Concat(line11.Split(' ')[1], line11.Split(' ')[2]);
        sd1.Add(string.Concat(line11.Split(' ')[4], line11.Split(' ')[5]));
        
        var acd_mean2 = string.Concat(line11.Split(' ')[7], line11.Split(' ')[8]);
        sd2.Add(string.Concat(line11.Split(' ')[10], line11.Split(' ')[11]));

        var line12 = mainText.Split('\n')[12];
        var lt_mean1 = string.Concat(line12.Split(' ')[1], line12.Split(' ')[2]);
        sd1.Add(string.Concat(line12.Split(' ')[4], line12.Split(' ')[5]));
        
        var lt_mean2 = string.Concat(line12.Split(' ')[7], line12.Split(' ')[8]);
        sd2.Add(string.Concat(line12.Split(' ')[10], line12.Split(' ')[11]));

        for (var i = 14; i < 19; i++)
        {
            var linex = mainText.Split('\n')[i];
            var lineSplit = linex.Split(' ');
            al1.Add(string.Concat(lineSplit[0], lineSplit[1]));
            cct1.Add(string.Concat(lineSplit[2], lineSplit[3]));
            acd1.Add(string.Concat(lineSplit[4], lineSplit[5]));
            lt1.Add(string.Concat(lineSplit[6], lineSplit[7]));
        
            al2.Add(string.Concat(lineSplit[8], lineSplit[9]));
            cct2.Add(string.Concat(lineSplit[10], lineSplit[11]));
            acd2.Add(string.Concat(lineSplit[12], lineSplit[13]));
            lt2.Add(string.Concat(lineSplit[14], lineSplit[15]));
        }
        
        return (new Biometric(al_mean1, cct_mean1, acd_mean1, lt_mean1, al1, cct1, acd1, lt1, sd1), 
            new Biometric(al_mean2, cct_mean2, acd_mean2, lt_mean2, al2, cct2, acd2, lt2, sd2));
    }
}

