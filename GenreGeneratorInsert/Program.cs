using System;
using System.Collections.Generic;
using System.IO;

namespace GenreGeneratorInsert
{
    class Program
    {
        static void Main(string[] args)
        {
            string InsertIntoCommand = "insert into dbo.genres(genre,notes) values(";
            string htmlString = File.ReadAllText($"C:\\Users\\hovse\\Downloads\\curl-7.79.1_4-win64-mingw\\curl-7.79.1-win64-mingw\\qwe\\a.html");
            List<String> data = getBetween(htmlString, @"<i class=""fa fa-plus""></i>", @"<i class='fa fa-minus'></i>");
            for (int j = 0; j < data.Count; j++)
                {
                    if (data[j].Contains("'")) { data[j] = data[j].Replace("'", ""); }
                    InsertIntoCommand = InsertIntoCommand + "'" + data[j] + "'" + "," + "' '";
                    InsertIntoCommand += "),(";
                }
            InsertIntoCommand = InsertIntoCommand.Replace("<a>", "");
            InsertIntoCommand = InsertIntoCommand.Replace("</a>", "");
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
