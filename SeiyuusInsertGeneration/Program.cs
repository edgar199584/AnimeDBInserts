using System;
using System.Collections.Generic;
using System.IO;

namespace SeiyuusInsertGeneration
{
    class Program
    {
        static void Main(string[] args)
        {

            string InsertIntoCommand = "insert into dbo.Seiyuus(languageid, fullname, country, notes) values(";
            for (int i = 1; i <= 1936; i++)
            {
                string htmlString = File.ReadAllText($"C:\\Users\\hovse\\Downloads\\curl-7.79.1_4-win64-mingw\\curl-7.79.1-win64-mingw\\Seiyuus\\{i}.html");
                List<String> data = getBetween(htmlString, @"><span class='name-en'>", "</span><span class='name-ru'>");
                for (int j = 0; j < data.Count; j++)
                {
                    if (data[j].Contains("'")) { data[j] = data[j].Replace("'", ""); }
                    InsertIntoCommand = InsertIntoCommand + "'jp'," + "'" + data[j] + "'" + "," +"'Japan',"+ "' '";
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
