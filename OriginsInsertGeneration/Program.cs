using System;
using System.Collections.Generic;
using System.IO;

namespace OriginsInsertGeneration
{
    class Program
    {
        static void Main(string[] args)
        {

            string InsertIntoCommand = "insert into dbo.origins(type,originalname,creatorid,notes) values(";

            int k = 0;
            Random rand = new Random();

            for (int i = 2; i <= 350; i++)
            {
                string htmlString = File.ReadAllText($"C:\\Users\\hovse\\Downloads\\curl-7.79.1_4-win64-mingw\\curl-7.79.1-win64-mingw\\Ranobes\\{i}.html");
                List<String> data = getBetween(htmlString, @"<span class='name-en'>", "</span><span class='name-ru'>");
                for (int j = 0; j < data.Count; j++)
                {
                    k = rand.Next(1, 350);
                    if (data[j].Contains("'")) { data[j] = data[j].Replace("'", ""); }
                    InsertIntoCommand = InsertIntoCommand +"'ranobe'," +"'"+ data[j] + "'," + $"{k}, " + "' '";
                    InsertIntoCommand += "),(";
                }
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
