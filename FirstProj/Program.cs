using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FirstProj
{
    class Program
    {
        static void Main(string[] args)
        {
            int c = 0;
            for (int i = 1; i < 519; i++)
            {
                string htmlString = File.ReadAllText($"C:\\Users\\hovse\\Downloads\\curl-7.79.1_4-win64-mingw\\curl-7.79.1-win64-mingw\\AnimePlanet\\{i}.html");
                List<String> data = getBetween(htmlString, "<li data-type=", " class='cardName");
                for(int j = 0; j < data.Count; j++)
                {
                    string fileName = @$"C:\\Users\\hovse\\Downloads\\curl-7.79.1_4-win64-mingw\\curl-7.79.1-win64-mingw\\RedactedAnimes\\{i+j+c}.html";
                    using (FileStream fs = File.Create(fileName))
                    {
                        // Add some text to file    
                        Byte[] title = new UTF8Encoding(true).GetBytes(data[j]);
                        fs.Write(title, 0, title.Length);
                        //byte[] author = new UTF8Encoding(true).GetBytes("Mahesh Chand");
                        //fs.Write(author, 0, author.Length);
                    }

                }
                c += 34;
            }

            Console.ReadKey();
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
