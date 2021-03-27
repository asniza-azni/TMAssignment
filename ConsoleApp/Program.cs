/** 
* Synopsis:
* Console program to tokenize a free form text into address component
*
* Written by: Asniza Azni 2021-03-26
* Synopsis:
* Revised: Date / Name / Synopsis
*                  
*/
 
using System; 
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq; 
using System.Text.RegularExpressions;
using ConsoleApp.Com.Model;
using ConsoleApp.Com.Process;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            OuterResponse outerResponse = new OuterResponse();
            bool tryAgain = true, withComma = true;
            string address, input;
            // Display title as the C# console calculator app.
            Console.WriteLine("Address Tokenizer in C#\r");
            Console.WriteLine("------------------------");

            do
            {
                // Ask the user to type the address input.  
                Console.WriteLine("\nPlease enter a valid address, and then press Enter");
                input = Console.ReadLine().ToString();
                if (string.IsNullOrWhiteSpace(input)) continue;

                address = Regex.Replace(input.Replace(".", " ").Replace(",", " "), @"\s+", " ", RegexOptions.Multiline);//Regex.Replace(input, " *, *", ",").Replace(".", "");

                string[] words = input.Split(',').Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray(); //with comma separated
                if (words.Length <= 1)
                {
                    words = input.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    withComma = false;
                }
                 
                int aptNoIndex = Array.FindIndex(words, t => t.Replace(",", "").Equals("No", StringComparison.InvariantCultureIgnoreCase));
                if (aptNoIndex >= 0)
                {
                    string aptNo = words[aptNoIndex] + " " + words[aptNoIndex + 1];
                    outerResponse.AptNo = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(aptNo.ToLower()).Replace(",", "");
                    address = address.Replace(words[aptNoIndex], "").Replace(words[aptNoIndex + 1], "").TrimStart();
                }

                Regex regex = new Regex(@"^\d{5}(?:\[A-Z]{3})?$");
                string[] numbers = Regex.Split(address, @"\D+").Where(s => !string.IsNullOrWhiteSpace(s) && regex.IsMatch(s)).ToArray();
                foreach (string value in numbers)
                {
                    int val = int.Parse(value);
                    if (val >= 1000 && val <= 98859)
                    {
                        outerResponse.PostCode = val.ToString("00000"); //01000-98859 
                        address = address.Replace(outerResponse.PostCode, "");
                    }
                }
                
                string[] matchingCities = DefaultConst.CITITES_GROUP().Where(s => address.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) > -1).ToArray();
                if (matchingCities.Length > 1)
                {
                    Console.WriteLine("\nAn adress cannot have more than one city.");
                    continue; 
                    //outerResponse.City = String.Join(", ", matchingCities);
                    //foreach (string city in matchingCities)
                    //{
                    //    address = Regex.Replace(address, city + ",", "", RegexOptions.IgnoreCase);
                    //    address = Regex.Replace(address, city, "", RegexOptions.IgnoreCase);
                    //}
                }
                else
                {
                    string firstMatchingCity = DefaultConst.CITITES_GROUP().FirstOrDefault(s
                        => address.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) > -1);
                    if (!string.IsNullOrEmpty(firstMatchingCity))
                    {
                        outerResponse.City = firstMatchingCity;
                        address = Regex.Replace(address, firstMatchingCity + ",", "", RegexOptions.IgnoreCase);
                        address = Regex.Replace(address, firstMatchingCity, "", RegexOptions.IgnoreCase);
                    }
                }
                
                string[] matchingStates = DefaultConst.STATES_GROUP().Where(s => address.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) > -1).ToArray();
                if (matchingStates.Length > 1)
                {
                    Console.WriteLine("\nAn adress cannot have more than one state.");
                    continue;
                }
                else
                {
                    string firstMatchingState = DefaultConst.STATES_GROUP().FirstOrDefault(s
                        => address.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) > -1);
                    if (!string.IsNullOrEmpty(firstMatchingState))
                    {
                        outerResponse.State = firstMatchingState;
                        address = Regex.Replace(address, firstMatchingState + ",", "", RegexOptions.IgnoreCase);
                        address = Regex.Replace(address, firstMatchingState, "", RegexOptions.IgnoreCase);
                    }
                }

                string firstMatchingStreet = DefaultConst.STREETS_GROUP().FirstOrDefault(s
                    => address.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) > -1);
                if (!string.IsNullOrEmpty(firstMatchingStreet))
                {
                    int streetIndex = Array.FindIndex(words, t => t.StartsWith(firstMatchingStreet, StringComparison.InvariantCultureIgnoreCase));
                    if (streetIndex >= 0)
                    {
                        string streetNo;
                        if (withComma)
                            streetNo = words[streetIndex];
                        else
                            streetNo = words[streetIndex] + " " + words[streetIndex + 1];

                        outerResponse.Street = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(streetNo.ToLower()).Trim();
                        address = Regex.Replace(address, streetNo + ",", "", RegexOptions.IgnoreCase);
                        address = Regex.Replace(address, streetNo, "", RegexOptions.IgnoreCase);
                    }
                }

                string others = Regex.Replace(System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(address.ToLower()), @"\s+", " ", RegexOptions.Multiline);
                 
                //.Replace(",", "").Trim().TrimStart(',').TrimEnd(',')
                if (!string.IsNullOrEmpty(others))
                    outerResponse.Section = others;

                // If all attribute is empty ask user to re-input a valid address
                if (string.IsNullOrEmpty(outerResponse.AptNo) && string.IsNullOrEmpty(outerResponse.Street) && string.IsNullOrEmpty(outerResponse.PostCode) && string.IsNullOrEmpty(outerResponse.City) && string.IsNullOrEmpty(outerResponse.State))
                    tryAgain = true;
                else
                    tryAgain = false;

            } while (tryAgain);
             
            Console.WriteLine("Output:\n");
            //Console.WriteLine(outerResponse.ToJson());  // single line JSON string 
            string jsonFormatted = JValue.Parse(outerResponse.ToJson()).ToString(Formatting.Indented);
            Console.WriteLine(jsonFormatted);

            // Wait for the user to respond before closing.
            Console.Write("\nPress any key to close the Address console app...");
            Console.ReadKey(); 
        }
    }
}
