/* Ontario Skills Competition Management
 * 5/7/2019 - Josh Degazio(S22)
 * To manage competitions and contestants in them.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace S22Terminal
{
    class Program
    {
        static int input = 0;
        static string[] schoolBoards = { "Bluewater", "Catholic", "Niagara", "Dufferin", "Durham", "Lambton", "Huron" };
        static List<Contestant> contestants = new List<Contestant>();
        static List<string> competitions = new List<string>();


        static void Main(string[] args)
        {
            readFromFile();
            competitionManagement();

        }

        private static void competitionManagement()
        {
            input = 0;
            Console.Clear();

            Console.WriteLine("- Ontario Skills Competition Management -");
            Console.WriteLine("");

            Console.WriteLine("1. Add Contestant.");
            Console.WriteLine("2. Remove Constestant.");
            Console.WriteLine("3. Search Contestant.");
            Console.WriteLine("4. Display All Contestants.");
            Console.WriteLine("5. Ran out of time.");
            Console.WriteLine("6. Help.");

            Console.WriteLine("");
            Console.Write("Enter:");

            input = Console.Read();

            if (input == 49)
            {
                addContestant();
            }
            else if (input == 50)
            {
                removeContestant();
            }
            else if (input == 51)
            {
                searchContestant();
            }
            else if (input == 52)
            {
                allContestants();
            }
            else if (input == 54)
            {
                help();
            }
        }

        private static void addContestant()
        {
            string tempId;
            string tempFirstName;
            string tempLastName;
            string tempEmailAddress;
            string tempSchoolDistrict;
            string tempBirthday;
            string tempCompetition;
            double tempScore;
            bool doesCompetitionExist = false;

            Console.Clear();

            Console.WriteLine("- Add Contestant -");

            Console.WriteLine("");
            Console.WriteLine("Enter Contestant ID: ");
            Console.ReadLine();
            tempId = Console.ReadLine();
            foreach(Contestant c in contestants)
            {
                if (tempId == c.allInfo[0].ToString())
                {
                    Console.WriteLine("The contestant already exists. Press any enter to return to main menu.");
                    Console.ReadKey();
                    competitionManagement();
                }
            }
            if (tempId.Length != 8)
            {
                Console.WriteLine("Contestants ID is not long enough. Press any enter to return to main menu.");
                Console.ReadKey();
                competitionManagement();
            }

            Console.WriteLine("");
            Console.WriteLine("Enter Contestant First Name: ");
            tempFirstName = Console.ReadLine();

            Console.WriteLine("");
            Console.WriteLine("Enter Contestant Last Name: ");
            tempLastName = Console.ReadLine();

            Console.WriteLine("");
            Console.WriteLine("Enter Contestant Email Address: ");
            tempEmailAddress = Console.ReadLine();

            Console.WriteLine("");
            Console.WriteLine("Enter Contestant School District: ");
            tempSchoolDistrict = Console.ReadLine();
            for (int i = 0; i < schoolBoards.Length; i++)
            {
                if (tempSchoolDistrict != schoolBoards[i])
                {
                    Console.WriteLine("The school board is not included in the program. Press any key to return to the mainmenu");
                    Console.ReadKey();
                    competitionManagement();
                }
            }

            Console.WriteLine("");
            Console.WriteLine("Enter Contestant Birthday: ");
            tempBirthday = Console.ReadLine();

            Console.WriteLine("");
            Console.WriteLine("Enter Contestant Competition: ");
            tempCompetition = Console.ReadLine();
            foreach (string competition in competitions)
            {
                if (tempCompetition == competition)
                {
                    doesCompetitionExist = true;
                }
            }
            if (doesCompetitionExist == false)
            {
                competitions.Add(tempCompetition);
            }
            

            Console.WriteLine("");
            Console.WriteLine("Enter Contestant Score: ");
            double.TryParse(Console.ReadLine(), out tempScore);

            Contestant contestant = new Contestant
                (tempId, tempFirstName, tempLastName, tempEmailAddress, tempSchoolDistrict, tempBirthday, tempCompetition, tempScore);

            Console.WriteLine("");
            Console.WriteLine("Contestant has been created with the following information: ");
            Console.WriteLine(contestant.allInfo);
            Console.ReadKey();

            contestants.Add(contestant);
            writeToFile();

            competitionManagement();
        }

        private static void removeContestant()
        {
            string tempId;
            string[] tempContestantInfo = new string[8];
            bool foundContestant = false;
            int contestantIndex = 0;

            Console.Clear();

            Console.WriteLine("- Remove Contestant -");

            Console.WriteLine("");
            Console.WriteLine("Enter The ID of The Contestant You Wish to Remove: ");

            Console.ReadLine();
            tempId = Console.ReadLine();

            for (int i = 0; i < contestants.Count; i++)
            {
                tempContestantInfo = contestants[i].allInfo.Split(',');
                if (tempContestantInfo[0] == tempId)
                {
                    foundContestant = true;
                    contestantIndex = i;
                }
            }

            if (foundContestant == true)
            {

                Console.WriteLine("");
                Console.WriteLine("Are you sure you want to remove " + tempContestantInfo[1] + " " + tempContestantInfo[2] + " as a contestant?" );
                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No");
                input = Console.Read();

                if (input == 49)
                {
                    contestants.RemoveAt(contestantIndex);

                    Console.WriteLine("");
                    Console.WriteLine("Successfully removed " + tempContestantInfo[1] + " " + tempContestantInfo[2] + ".");
                    Console.WriteLine("Press any key to return to the main menu.");
                    Console.ReadKey();

                    writeToFile();
                }
            }

            else if (foundContestant == false)
            {

                Console.WriteLine("");
                Console.WriteLine("Could not find contestant.");
                Console.WriteLine("Press any key to return to the main menu.");
                Console.ReadKey();
            }

            competitionManagement();
            Console.ReadLine();
            Console.ReadLine();
        }

        private static void searchContestant()
        {
            string tempLastName = "";
            string[] tempContestantInfo = new string[8];
            input = 0;

            Console.Clear();

            Console.WriteLine("- Search Contestant By Last Name -");
            Console.WriteLine("");
            Console.WriteLine("Enter last name: ");

            Console.ReadLine();
            tempLastName = Console.ReadLine();

            for (int i = 0; i < contestants.Count; i++)
            {
                tempContestantInfo = contestants[i].allInfo.Split(',');
                if (tempContestantInfo[2] == tempLastName)
                {
                    Console.WriteLine("");
                    Console.WriteLine("ID: " + tempContestantInfo[0]);
                    Console.WriteLine("First Name: " + tempContestantInfo[1]);
                    Console.WriteLine("Last Name: " + tempContestantInfo[2]);
                    Console.WriteLine("Email: " + tempContestantInfo[3]);
                    Console.WriteLine("School District: " + tempContestantInfo[4]);
                    Console.WriteLine("Birthday: " + tempContestantInfo[5]);
                    Console.WriteLine("Competition: " + tempContestantInfo[6]);
                    Console.WriteLine("Score: %" + tempContestantInfo[7] + "00");
                    Console.ReadKey();
                }
            }
        
            competitionManagement();

        }

        private static void allContestants()
        {
            string[] tempContestantInfo = new string[8];
            input = 0;
            Console.Clear();

            Console.WriteLine("- Display All Contestants Sorted by Last And First Name -");
            Console.WriteLine("");

            IEnumerable<Contestant> query = contestants.OrderBy(Contestant => Contestant.lastName).ThenBy(Contestant => Contestant.firstName);
            foreach (Contestant contestant in query)
            {
                Console.WriteLine(contestant.allInfo);
            }
            Console.ReadKey();
        }

        private static void help()
        {
            input = 0;
            Console.Clear();

            Console.WriteLine("Use digits and the enter key to navigate menus.");
            Console.WriteLine("Press enter to return to the mainmenu.");
            Console.ReadKey();
        }
        private static void topContestants()
        {
            int i = 0;
            input = 0;
            Console.Clear();

            Console.WriteLine("- Display Top Three Contestants In Any Given Competition -");
            Console.WriteLine("");

            IEnumerable<Contestant> query = contestants.OrderBy(Contestant => Contestant.score).ThenBy(Contestant => Contestant.competition);
            foreach (Contestant contestant in query)
            {
                Console.WriteLine(contestant.allInfo);
                i++;
                if (i >= 3)
                {
                    break;
                }
            }

            Console.ReadKey();


        }

        private static void readFromFile()
        {
            try
            {
                using (StreamReader streamReader = new StreamReader("Contestants.txt"))
                {
                    string[] tempString;
                    double tempScore;
                    while (!streamReader.EndOfStream)
                    {
                        tempString = streamReader.ReadLine().Split(',');
                        double.TryParse(tempString[7], out tempScore);
                        Contestant contestant = 
                            new Contestant(tempString[0], tempString[1], tempString[2], tempString[3],
                            tempString[4], tempString[5], tempString[6], tempScore);
                        contestants.Add(contestant);
                    }
                    streamReader.Close();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey();
            }
        }

        private static void writeToFile()
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter("Contestants.txt"))
                {
                    foreach (Contestant contestant in contestants)
                    {
                        streamWriter.WriteLine(contestant.allInfo);
                    }
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey();
            }
        }
    }

    class Contestant
    {
        public string allInfo
        {
            get
            {
                return id + "," + firstName + "," + lastName + "," + emailAddress + "," 
                    + schoolDistrict + "," + birthday + "," + competition + "," + score.ToString();
            }
        }
        string id;
        public string firstName;
        public string lastName;
        public string emailAddress;
        public string schoolDistrict;
        public string birthday;
        public string competition;
        public double score;
        public Contestant(string ID, string FirstName, string LastName, string EmailAddress, string SchoolDistrict, string Birthday, string Competition, double Score)
        {
            id = ID;
            firstName = FirstName;
            lastName = LastName;
            emailAddress = EmailAddress;
            schoolDistrict = SchoolDistrict;
            birthday = Birthday;
            competition = Competition;
            score = Score;
        }
    }
}
