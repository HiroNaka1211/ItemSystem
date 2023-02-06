using System;
using static System.Console;

namespace IFN664
{
    class Program
    {
        static void Main(string[] args)
        {
            MemberCollection aMemberCollection = new MemberCollection(200);
            ToolCollection aToolCollection = new ToolCollection(200);

            //Below tools will be used for test
            aToolCollection.AddNewTool(0, 0, new Tool("18V BRUSHLESS 33CM LINE TRIMMER 4.0AH FORCE KIT", 3, 6));
            aToolCollection.AddNewTool(0, 0, new Tool("58V BRUSHLESS 43CM LINE TRIMMER SKIN", 8, 5));
            aToolCollection.AddNewTool(0, 0, new Tool("58V 4.0AH BRUSHLESS 43CM LINE TRIMMER KIT", 6, 13));
            aToolCollection.AddNewTool(0, 1, new Tool("Honda 3-in-1 Variable Speed Gas Lawn Mower", 6, 3));
            aToolCollection.AddNewTool(0, 1, new Tool("John Deere S120 Gas Hydrostatic Lawn Tractor", 6, 7));
            aToolCollection.AddNewTool(1, 0, new Tool("QEP Multi-Use Power Scrapers", 6, 9));
            aToolCollection.AddNewTool(1, 0, new Tool("Crain 350 Floor Stripper", 6, 5));
            aToolCollection.AddNewTool(1, 0, new Tool("Crain 375 Big Scraper", 6, 7));

            //Below members will be used for test
            aMemberCollection.Add(new Member("Mary", "Shelton", "0412123123", 3421));
            aMemberCollection.Add(new Member("Dave", "Allen", "0412123324", 9876));
            aMemberCollection.Add(new Member("Ivan", "Green", "0412134123", 6754));
            aMemberCollection.Add(new Member("Vera", "Adams", "041212899", 1256));
            aMemberCollection.Add(new Member("Lucy", "Gordon", "0418923123", 9993));

            bool end = false;

            WriteLine("Welcome to the Tool Library System");
            while (!end) 
            {
                WriteLine();
                WriteLine("=========Main Menu==========");
                WriteLine("1. Staff login");
                WriteLine("2. Member login");
                WriteLine("0. Exit");
                WriteLine("=============================");
                Write("Select option from menu (0 to exit) - ");

                string input = ReadLine();

                bool correctInput = Int32.TryParse(input, out int select);


                if (!correctInput || select < 0 || 2 < select)
                {
                    WriteLine("Your input number is not valid");
                    WriteLine();
                    continue;
                }

                switch (select)
                {
                    case 0:
                        end = true;
                        WriteLine();
                        WriteLine("Thank you for using this app");
                        continue;

                    case 1:
                        ShowStaffMenu(aMemberCollection, aToolCollection);
                        break;

                    case 2:
                        ShowMemberMenu(aMemberCollection, aToolCollection);
                        break;
                }
                
            }
            ReadKey();
        }
        public static void ShowStaffMenu(MemberCollection aMemberCollection, ToolCollection aToolCollection)
        {
            const string staffName = "staff";
            const string password = "today123";
            bool end = false;

            while (!end)
            {
                WriteLine();
                Write("Input the user name >> ");
                string inputUsername = ReadLine();
                WriteLine();
                Write("Input the password >> ");
                string inputPassword = ReadLine();
                if (inputUsername == staffName && inputPassword == password)
                {
                    break;
                }
                else
                {
                    WriteLine();
                    WriteLine("Input username or password is not exist. Please Input corretly");

                    while (true)
                    {
                        WriteLine();
                        WriteLine("1. Try again ");
                        WriteLine("2. Go back to main page");
                        Write("Select option from menu - ");
                        string selected = ReadLine();

                        bool inputnumber = Int32.TryParse(selected, out int choice);
                        if (selected == "" || !inputnumber || choice < 0 || 2 < choice)
                        {
                            WriteLine();
                            WriteLine("Please input 1 or 2");
                            continue;
                        }
                        else
                        {
                            if (choice == 1)
                            {
                                break;
                            }
                            else
                            {
                                end = true;
                                break;
                            }
                        }
                    }
                    
                }
            }
            while (!end)
            {


/*                WriteLine("Welcome to the Tool Library");*/
                WriteLine();
                WriteLine("==========Staff Menu==========");
                WriteLine("1. Add a tool");
                WriteLine("2. Remove a tool");
                WriteLine("3. Register a new member");
                WriteLine("4. Remove a member");
                WriteLine("5. Display all the members who are borrowing a tool");
                WriteLine("6. Find a member's phone number");
                WriteLine("0. Return to main menu");
                WriteLine("===============================");
                Write("   Select option from menu (0 to exit) - ");
           
                string input = ReadLine();
                WriteLine();

                bool correctinput = Int16.TryParse(input, out short select);
                if (!correctinput || select < 0 || 6 < select)
                { 
                    WriteLine("Your input is not valid.\nPlease input valid number.\nTry again.");
                    WriteLine();
                    continue;
                }
                switch (select)
                {
                    case 0:
                        end = true;
                        continue;
                    case 1:
                        aToolCollection.AddNewTool();
                        break;
                    case 2:
                        aToolCollection.RemoveTool();
                        break;
                    case 3:
                        aMemberCollection.Add();
                        break;
                    case 4:
                        aMemberCollection.Delete();
                        break;
                    case 5:
                        if (aToolCollection.borrowedToolCount > 0)
                        {
                            aToolCollection.DisplayAllMembers();
                        }
                        else
                        {
                            WriteLine("There is no member who are borrowing tools currently.");   
                        }
                        break;
                    case 6:
                        aMemberCollection.GetPhoneNumber();
                        break;

                }

            }

        }

        public static void ShowMemberMenu(MemberCollection aMemberCollection, ToolCollection aTollCollection) 
        {
            bool end = false;
            bool finish = false;
            Member member;
            if (aMemberCollection.Count != 0)
            {
                while (!finish)
                {
                    member = aMemberCollection.SearchMember();
                    if (member != null)
                    {
                        WriteLine("-----Welcome {0} {1}-----", member.FirstName, member.LastName);
                        while (!end)
                        {
                            /*WriteLine("Welcome to the Tool Library");*/
                            WriteLine();
                            WriteLine("==========Member Menu==========");
                            WriteLine("1. Display tools of type");
                            WriteLine("2. Borrow a tool");
                            WriteLine("3. Return a tool");
                            WriteLine("4. List tools I'm borrowing");
                            WriteLine("5. Display top three most frequently borrowed tools");
                            WriteLine("0. Return to main menu");
                            WriteLine("===============================");
                            Write("   Select option from menu (0 to exit) - ");

                            string input = ReadLine();
                            WriteLine();

                            bool correctinput = Int16.TryParse(input, out short select);
                            if (!correctinput || select < 0 || 5 < select)
                            {
                                WriteLine("Your input is not valid.\nPlease input valid number.\nTry again.");
                                WriteLine();
                                continue;
                            }
                            switch (select)
                            {
                                case 0:
                                    end = true;
                                    finish = true;
                                    continue;
                                case 1:
                                    aTollCollection.ShowToolList();
                                    continue;
                                case 2:
                                    if (4 < member.NoHoldingTools) //If the member is borrowing 5 tools now, the member cannot borrow more.
                                    {
                                        WriteLine("You are borrowing 5 tools now. Therefore, you cannot borrow anymore.");
                                        continue;
                                    }
                                    else
                                    {
                                        aTollCollection.BorrowTool(member, aMemberCollection);
                                        continue;
                                    }
                                case 3:
                                    if (member.NoHoldingTools < 1)
                                    {
                                        WriteLine("You are not borrowing any items");
                                        continue;
                                    }
                                    else
                                    {
                                        aTollCollection.ReturnTool(member);
                                        continue;
                                    }


                                case 4:
                                    if (member.NoHoldingTools < 1)
                                    {
                                        WriteLine("You are not borrwoing any tools now.");
                                        continue;
                                    }
                                    else
                                    {
                                        member.ShowHoldingTool();
                                        continue;
                                    }


                                case 5:
                                    aTollCollection.ShowTop3();
                                    continue;
                            }

                        }
                    }
                    else
                    {
                        WriteLine();
                        WriteLine("The member is not exist");

                        while (true)
                        {
                            WriteLine();
                            WriteLine("1. Try again ");
                            WriteLine("2. Go back to main page");
                            Write("   Select option from menu - ");
                            string selected = ReadLine();

                            bool inputnumber = Int16.TryParse(selected, out short choice);
                            if (selected == "" || !inputnumber || choice < 0 || 2 < choice)
                            {
                                WriteLine();
                                WriteLine("Please input 1 or 2");
                                continue;
                            }
                            else
                            {
                                if (choice == 1)
                                {
                                    break;
                                }
                                else
                                {
                                    end = true;
                                    finish = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                return;
            }
            else
                WriteLine();
                WriteLine("There is no registered members");
        }
    }
}
