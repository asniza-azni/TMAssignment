/** 
* Synopsis:
* 
*
* Written by: Asniza Azni 2021-03-26
* Synopsis:
* Revised: Date / Name / Synopsis
*                  
*/

using System.Collections.Generic; 

namespace ConsoleApp.Com.Process
{
    public static class DefaultConst
    {
        private static readonly List<string> LIST_STREETS = new List<string>()
                    {
                        "Jalan",
                        "Jln",
                        "Lorong",
                        "Persiaran"
                    };

        public static List<string> STREETS_GROUP()
        {
            return LIST_STREETS;
        }

        private static readonly List<string> LIST_CITITES = new List<string>()
            {
                "Kuala Terengganu",
                "Kuala Lumpur",
                "Kajang",
                "Bangi",
                "Damansara",
                "Petaling Jaya",
                "Puchong",
                "Subang Jaya",
                "Cyberjaya",
                "Putrajaya",
                "Mantin",
                "Kuching",
                "Seremban"
            };

        public static List<string> CITITES_GROUP()
        {
            return LIST_CITITES;
        }

        private static readonly List<string> LIST_STATES = new List<string>()
                    {
                        "Selangor",
                        "Terengganu",
                        "Pahang",
                        "Kelantan",
                        "Melaka",
                        "Pulau Pinang",
                        "Puchong",
                        "Kedah",
                        "Johor",
                        "Perlis",
                        "Sabah",
                        "Sarawak"
                    };


        public static List<string> STATES_GROUP()
        {
            return LIST_STATES;
        }
    }
}