using System;
using System.Collections.Generic;
using System.IO;

namespace AnimeGenreMappingInsertGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            string InsertIntoCommand = "insert into dbo.anime_genre(animeid,genreid) values(";
            string a = @$"(select animeid from dbo.animes where title = ";
            string b = @$"(select genreid from dbo.genres where genre = ";
            string c;
            string d;
            for (int i = 1; i <= 14970; i++)
            {
                if (File.Exists($"C:\\Users\\hovse\\Downloads\\curl-7.79.1_4-win64-mingw\\curl-7.79.1-win64-mingw\\RedactedAnimes\\{i}.html"))
                {
                    string htmlString = File.ReadAllText($"C:\\Users\\hovse\\Downloads\\curl-7.79.1_4-win64-mingw\\curl-7.79.1-win64-mingw\\RedactedAnimes\\{i}.html");
                    List<String> data = getBetween(htmlString, @"<h5 class='theme-font'>", "</h5>");
                    if (data[0].Contains("'")) { data[0] = data[0].Replace("'", ""); }
                    List<String> genres = getBetween(htmlString, @"><h4>Tags</h4><ul>", "</ul></div>");
                    if (genres.Count != 0)
                    {
                        List<String> genreNames = getBetween(genres[0], @"<li>", "</li>");
                        foreach (var item in genreNames)
                        {
                            string ku = item;
                            if (ku.Contains("'")) { ku = ku.Replace("'", ""); }
                            InsertIntoCommand = InsertIntoCommand + a + "'" + data[0] + "' limit 1),"+ b +"'" + ku + "')),(";
                        }
                    }
                    else { continue; }
                }
                else { continue; }
            }
            int k = 0;
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
