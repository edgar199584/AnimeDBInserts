using System;
using System.Collections.Generic;
using System.IO;

namespace AnimesInsertGenreation
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, string> typesDict = new Dictionary<int, string>();
            typesDict.Add(1, "TV");
            typesDict.Add(2, "TV Special");
            typesDict.Add(3, "OVA");
            typesDict.Add(4,"Movie");
            typesDict.Add(5,"DVD Special");
            typesDict.Add(6,"Music Video");
            typesDict.Add(7,"Web");
            typesDict.Add(8,"Other");
           // < img alt = "     " data - src =
            Dictionary<int, string> seasonsDict = new Dictionary<int, string>();
            seasonsDict.Add(1, "Winter");
            seasonsDict.Add(2, "Spring");
            seasonsDict.Add(3, "Summer");
            seasonsDict.Add(4, "Autumn");

            string InsertIntoCommand = "insert into dbo.animes(title,releasedate,season,episodescount,episodestime,releasecountry,typeid,studiocreatorid,originid,statusid,description) values(";

            int k = 0;
            Random rand = new Random();
            k = rand.Next(1, 350);
            for (int i = 1; i <= 14970; i++)
            {
                if (File.Exists($"C:\\Users\\hovse\\Downloads\\curl-7.79.1_4-win64-mingw\\curl-7.79.1-win64-mingw\\RedactedAnimes\\{i}.html"))
                {
                    string htmlString = File.ReadAllText($"C:\\Users\\hovse\\Downloads\\curl-7.79.1_4-win64-mingw\\curl-7.79.1-win64-mingw\\RedactedAnimes\\{i}.html");
                    List<String> data = getBetween(htmlString, @"<h5 class='theme-font'>", "</h5>");
                    if (data[0].Contains("'")) { data[0] = data[0].Replace("'", ""); }
                    InsertIntoCommand = InsertIntoCommand + "'" + data[0] + "',";
                    List<String> releaseDate = getBetween(htmlString, @"<li class='iconYear'>", "</li><li>");
                    if (releaseDate.Count == 0)
                    {
                        InsertIntoCommand = InsertIntoCommand + "'" + "2014" + "',";
                    }
                    else
                    {
                        if (releaseDate[0].Contains("'")) { releaseDate[0] = releaseDate[0].Replace("'", ""); }
                        InsertIntoCommand = InsertIntoCommand + "'" + releaseDate[0] + "',";
                    }
                    k = rand.Next(1, 4);
                    InsertIntoCommand = InsertIntoCommand + "'" + seasonsDict[k] + "',";
                    List<String> episodesCount = getBetween(htmlString, @"<li class='type'>", "</li><li>");
                    if (episodesCount[0].Contains("'")) { episodesCount[0] = episodesCount[0].Replace("'", ""); }
                    string str = getBetween(episodesCount[0], @"(", " ep")[0];
                    if (str.Contains("+")) { str = str.Replace("+", ""); }
                    int epCounter = Int32.Parse(str);
                    InsertIntoCommand = InsertIntoCommand + epCounter + ",";
                    if (epCounter == 1)
                    {
                        InsertIntoCommand = InsertIntoCommand + 120 + ",";
                    }
                    else if (epCounter < 8)
                    {
                        InsertIntoCommand = InsertIntoCommand + 40 + ",";
                    }
                    else { InsertIntoCommand = InsertIntoCommand + 24 + ","; }
                    InsertIntoCommand = InsertIntoCommand + "'Japan',";
                    foreach (var element in typesDict)
                    {
                        if (episodesCount[0].Contains(element.Value))
                        {
                            InsertIntoCommand = InsertIntoCommand + element.Key + ",";
                            break;
                        }
                    }
                    k = rand.Next(1, 699);
                    InsertIntoCommand = InsertIntoCommand + k + ",";
                    k = rand.Next(1, 16848);
                    InsertIntoCommand = InsertIntoCommand + k + "," + 2 + ",";
                    List<String> description = getBetween(htmlString, @"</div></li></ul><p>", "<div class='tags'>");
                    if (description.Count == 0)
                    {
                        InsertIntoCommand = InsertIntoCommand + "'" + "No Description" + "'";
                        InsertIntoCommand += "),(";
                    }
                    else
                    {
                        if (description[0].Contains("'")) { description[0] = description[0].Replace("'", ""); }
                        InsertIntoCommand = InsertIntoCommand + "'" + description[0] + "'";
                        InsertIntoCommand += "),(";
                    }
                }
                else { continue; }
            }

        }
        public static List<string> getBetween(string strSource, string strStart, string strEnd)
        {
            List<String> result = new List<string>();
            while (strSource != null)
            {
                if (strSource.Contains(strStart) && strSource.Contains(strEnd))
                {
                    int Start, End;
                    Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                    End = strSource.IndexOf(strEnd, Start);
                    result.Add(strSource.Substring(Start, End - Start));
                    strSource = strSource.Substring(strSource.IndexOf(strEnd, strSource.IndexOf(strStart, 0) + strStart.Length));
                }
                else
                    strSource = null;

            }

            return result;
        }
    }
}
