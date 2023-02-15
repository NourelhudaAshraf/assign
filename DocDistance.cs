using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentDistance
{
	class DocDistance
	{
		// *****************************************
		// DON'T CHANGE CLASS OR FUNCTION NAME
		// YOU CAN ADD FUNCTIONS IF YOU NEED TO
		// *****************************************
		/// <summary>
		/// Write an efficient algorithm to calculate the distance between two documents
		/// </summary>
		/// <param name="doc1FilePath">File path of 1st document</param>
		/// <param name="doc2FilePath">File path of 2nd document</param>
		/// <returns>The angle (in degree) between the 2 documents</returns>
		public static double CalculateDistance(string doc1FilePath, string doc2FilePath)
		{
			// TODO comment the following line THEN fill your code here
            /*get content of file path*/
			string s1 = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, doc1FilePath)).ToLower();
			string s2 = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, doc2FilePath)).ToLower();
            
			/*get vector of each document*/
			double D1 = 0;
			double D2 = 0;
			Dictionary<string, double> map1 = documentToWords(s1, ref D1);
			Dictionary<string, double> map2 = documentToWords(s2, ref D2);

           /*get dot product*/
			double D1_D2 = calcualteD1_D2(map1, map2);
            /*get angle between two vector*/
			double distance = Math.Acos((D1_D2 / (Math.Sqrt(D1 * D2)))) * 180 / 3.1415926535897931;

			return distance;
		}
		private static Dictionary<string, double> documentToWords(string s2, ref double d)
		{
			Dictionary<string, double> map2 = new Dictionary<string, double>();
			string currentWord = "";
			for (int i = 0; i < s2.Length; ++i)
			{
				char currentLetter = s2[i];
				if ((currentLetter >= 'a' && currentLetter <= 'z') || (currentLetter >= '0' && currentLetter <= '9'))
				{
					currentWord += currentLetter;
				}
				else
				{
					if (currentWord != "")
					{
						if (!map2.ContainsKey(currentWord))
						{
							map2.Add(currentWord, 1);
						}
						else
							map2[currentWord]++;
						currentWord = "";
					}
				}
			}
			if (currentWord != "")
			{
				if (!map2.ContainsKey(currentWord))
				{
					map2.Add(currentWord, 1);
				}
				else
				{
					map2[currentWord]++;
				}
			}
			foreach (KeyValuePair<string, double> pair in map2)
			{
				d += pair.Value * pair.Value;
			}
			return map2;
		}
		private static double calcualteD1_D2(Dictionary<string, double> map1, Dictionary<string, double> map2)
		{
			double result = 0;
			/*choose smallest one to iterate over it*/
			if (map1.Count() <= map2.Count())
			{
				foreach (KeyValuePair<string, double> pair in map1)
				{
					if (map2.ContainsKey(pair.Key))
					{
						result += pair.Value * map2[pair.Key];
					}
				}
			}
			else
			{
				foreach (KeyValuePair<string, double> pair in map2)
				{
					if (map1.ContainsKey(pair.Key))
					{
						result += pair.Value * map1[pair.Key];
					}
				}
			}
			return result;
		}
	}
}
