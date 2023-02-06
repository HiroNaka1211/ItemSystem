using System;
using static System.Console;
using System.Collections.Generic;
using System.Text;

namespace IFN664
{
    class ToolCollection
    {
        private Tool[,,] _toolList = new Tool[9, 20, 20];
        private Tool[] _borrowedTool;
        public int borrowedToolCount; //This will show how many tools are borrowed currently.
        private int _borrowedListCount;
        private Tool[] _popularity;
        private int KindTools; //This is incremented whenever new item is added.
        private Tool[] registeredTool; // This will be used for storing all items. And when the staff adds new items, this will be used to check if the system has the same name tool already.
        private Member deletedMember = new Member("Deleted", "Deleted", "Deleted", 0000);


        //I will use a two dimentional array for categories and types. The number of category is fixed 9. However, the number of types may be changed (will add or delete) later.
        //Therefore, I need to set the array size big enough. I can't set the category and the type into the array at first because in that case, the array size must be fixed.
        //But this time, it must not be fixed. Using Array.Resize, the size of arraies can be changed. But I don't think I can use it this time. Therefore,


        private string[] categories = new string[]{"Gardening tools", "Flooring tools", "Fencing tools", "Measuring tools",
            "Cleaning tools", "Painting tools", "Electronic tools", "Electricity tools", "Automotive tools"}; //This is fixed, therefore the size is 9.

        private string[][] types = new string[][] //This will be used for showing category which is mathched to the category. 
            {
                new string[] { "Line Trimmers", "Lawn Mowers", "Hand Tools", "Wheelbarrows", "Garden Power Tools" },
                new string[] {"Scrapers" ,"Floor Lasers","Floor Levelling Tools", "Floor Levelling Materials", "Floor Hand Tools", "Tiling Tool"},
                new string[] { "Hand Tools", "Electric Fencing", "Steel Gencing Tools", "Power Tools", "Fencing Accessories" },
                new string[] { "Distance Tools", "Laser Measurer", "Measuring Jugs", "Temperature & Humidity Tools", "Levelling Tools", "Markers" },
                new string[] { "Draining", "Car Cleaning", "Vacuum", "Pressure Cleaners", "Pool Cleaning", "Floor Cleaning" },
                new string[] { "Sanding Tools", "Brushes", "Rollers", "Paint Removal Tools", "Paint Scrapers", "Sprayers" },
                new string[] { "Voltage Tester", "Oscilloscopes", "Thermal Imaging", "Data Test Tool", "Insulation Testers" },
                new string[] { "Test Equipment", "Safety Equipment", "Basic Hand tools", "Circuit Protection", "Cable Tools" },
                new string[] { "Jacks", "Air Compressors", "Battery Chargers", "Socket Tools", "Braking", "Drivetrain" }
            };
        public Tool[,,] ToolList //In this array, tool will be stored by category and the type;
        {
            get { return _toolList; }
            set { _toolList = value; }
        }
        public Tool[] BorrowedTool //This array will be stored the tools which are borrowed by members.
        {
            get { return _borrowedTool; }
            set { _borrowedTool = value; }
        }
        public int BorrowedListCount //This will be shown the count of the borrowed tools
        {
            get { return _borrowedListCount; }
            set { _borrowedListCount = value; }
        }
        public Tool[] Popularity //In this property, borrowed item will be stored. And this is used for displaying top three (3) most frequently borrowed tools
        {
            get { return _popularity; }
            set { _popularity = value; }
        }

        public ToolCollection(int capacity) {

            Popularity = new Tool[capacity];
            BorrowedTool = new Tool[capacity];
            registeredTool = new Tool[capacity];

        }
        public void AddNewTool(int category, int type, Tool aTool) //This is used for the test
        {
            int selectedTool = 0;
            for (int i = 0; i < ToolList.GetLength(2); i++)
            {
                if (ToolList[category, type, i] == null)
                {
                    selectedTool = i;
                    break;
                }
            }
            ToolList[category, type, selectedTool] = aTool;
            registeredTool[KindTools] = aTool;

            KindTools++;

            registeredTool = MergeSort<Tool>.StartSortForRegistration(registeredTool);
            ToolList = MergeSort<Tool>.StartSort(ToolList, category, type); //After adding new tool, the array will be sorted.
            Popularity[BorrowedListCount] = aTool;
            BorrowedListCount++;
            Popularity = MergeSort<Tool>.StartSortByPopularity(Popularity);


        }

        public void AddNewTool()
        {
            int selectedCategory;
            int selectedType;
            int searchResult = -1;
            int selectedTool = 0;
            WriteLine("======Add new tool======");

            while (true)
            {
                var temp = new Tool();

                string toolName;
                int availableStock;

                while (true)
                {
                    WriteLine();
                    Write("Input tool name >> ");
                    string input = ReadLine().Trim();
                    WriteLine();
                    if (input == "")
                    {
                        WriteLine("Please input tool name");
                        continue;
                    }
                    toolName = input;
                    break;
                }
                if (KindTools > 0)
                {
                    searchResult = BinarySearch(registeredTool, toolName.Replace(" ", String.Empty).ToUpper());
                }

                if (searchResult != -1)
                {
                    temp = registeredTool[searchResult];
                    while (true)
                    {
                        WriteLine();
                        Write("The same name tool has already been in the item list\nThe number of stock is {0}.\n" +
                            "You can increase the quantity of the tool.\n How many do you want to increase the quantity?\n" +
                            "(If you do not want to increment, please input 0) >> ", temp.Stock);
                        if (!Int32.TryParse(ReadLine(), out int inputQuantity) || inputQuantity < 0)
                        {
                            WriteLine();
                            WriteLine("The input is not valid");
                            WriteLine();
                            continue;
                        }
                        else
                        {
                            temp.Stock += inputQuantity;
                            temp.AvailableItems += inputQuantity;
                            WriteLine();
                            WriteLine("Operation Successful");
                            WriteLine();
                            break;
                        }
                    }
                    break;

                }
                else if (searchResult == -1)
                {
                    ShowOption(out selectedCategory, out selectedType);
                    WriteLine();
                    while (true)
                    {
                        Write("Input the number of the stock (must be more than or equal to 1) >> ");
                        if (!Int32.TryParse(ReadLine(), out int inputstock) || inputstock < 1)
                        {
                            WriteLine();
                            WriteLine("The input is not valid");
                            WriteLine();
                            continue;
                        }
                        availableStock = inputstock;
                        break;
                    }
                    for (int i = 0; i < ToolList.GetLength(2); i++)
                    {
                        if (ToolList[selectedCategory, selectedType, i] == null)
                        {
                            selectedTool = i;
                            break;
                        }
                    }

                    var aTool = new Tool(toolName, availableStock);
                    ToolList[selectedCategory, selectedType, selectedTool] = aTool;
                    registeredTool[KindTools] = aTool;

                    KindTools++;

                    WriteLine();
                    WriteLine("Operation Successful");
                    WriteLine();


                    registeredTool = MergeSort<Tool>.StartSortForRegistration(registeredTool);

                    ToolList = MergeSort<Tool>.StartSort(ToolList, selectedCategory, selectedType); //After adding new tool, the array will be sorted.
                    for (int i = 0; i < ToolList.GetLength(2); i++)
                    {
                        if (ToolList[selectedCategory, selectedType, i] != null)
                        {
                            WriteLine(ToolList[selectedCategory, selectedType, i].ToolName);
                        }
                        else break;
                    }
                    break;
                }
                else
                {
                    WriteLine();
                    WriteLine("The memory is already full. Therefore, you cannot add the tool."); // In the case of full of the array
                    break;
                }
            }
        }

        public void RemoveTool()
        {
            int selectedCategory;
            int selectedType;
            int selectedTool;
            bool end = false;
            if (0 < KindTools)
            {
                WriteLine("======Remove a tool======");
                WriteLine();
                ShowOption(out selectedCategory, out selectedType);

                while (true)
                {
                    var temp = new Tool();
                    int counter = -9999; //This stores the null place in toolList;
                    while (true)
                    {
                        for (int i = 0; i < ToolList.GetLength(2); i++)
                        {
                            if (ToolList[selectedCategory, selectedType, 0] != null)
                            {
                                if (i == 0)
                                {
                                    WriteLine();
                                    WriteLine("         {0,-14} {1, 20}", "Name", "Available items");
                                }
                                if (ToolList[selectedCategory, selectedType, i] == null)
                                {
                                    counter = i;
                                    break;
                                }
                                else
                                {
                                    WriteLine("{0}. {1}", i + 1, ToolList[selectedCategory, selectedType, i].ToString());
                                    counter = i;
                                }
                            }
                            else
                            {
                                break;
                            }

                        }
                        if (counter != -9999)
                        {
                            WriteLine();
                            Write("Select item from above - ");
                            if (!Int32.TryParse(ReadLine(), out int inputNumber) || inputNumber < 1 || counter < inputNumber)
                            {
                                WriteLine();
                                WriteLine("Please input correctly");
                                WriteLine();
                                continue;
                            }
                            selectedTool = inputNumber - 1;
                            temp = ToolList[selectedCategory, selectedType, selectedTool];
                            if (temp.AvailableItems > 0)
                            {
                                while (!end)
                                {
                                    Write("{0} has {1} available tools ({2} Tool(s) is(are) borrowed now).\n" +
                                        "How many tools do you want to remove from available ones?\n" +
                                        "(If you do not want to remove anything, you can input 0) >> ", temp.ToolName, temp.AvailableItems, temp.Stock - temp.AvailableItems);
                                    if (!Int32.TryParse(ReadLine(), out int inputQuantity) || inputQuantity < 0 || temp.AvailableItems < inputQuantity)
                                    {
                                        WriteLine();
                                        WriteLine("Please input correctly");
                                        WriteLine();
                                        continue;
                                    }
                                    else
                                    {
                                        temp.Stock -= inputQuantity;
                                        temp.AvailableItems -= inputQuantity;
                                        if (temp.Stock == 0)
                                        {
                                            WriteLine();
                                            WriteLine("{0}'s stock is 0.", temp.ToolName);
                                            WriteLine("Do you want to delete it?");
                                            while (!end)
                                            {
                                                WriteLine("1. Yes\n2. No");
                                                Write("Select option from menu (0 to exit) - ");
                                                if (!Int32.TryParse(ReadLine(), out int inputstock) || inputstock < 1 || inputstock > 2)
                                                {
                                                    WriteLine("Your input is not vali. Try again");
                                                }
                                                switch (inputstock)
                                                {
                                                    case 1:
                                                        DeleteToolExist(selectedCategory, selectedType, selectedTool, temp);
                                                        end = true;
                                                        break;
                                                    case 2:
                                                        end = true;
                                                        break;
                                                }
                                            }



                                        }
                                        WriteLine();
                                        WriteLine("Operation Successful");
                                        WriteLine();
                                        break;
                                    }
                                }
                                break;
                            }
                            else
                            {
                                if (temp.Stock != 0)
                                {
                                    WriteLine();
                                    WriteLine("You cannot delete this Tool. Because Some members are borrowing.");
                                }
                                else
                                {
                                    WriteLine();
                                    WriteLine("{0}'s stock is 0.", temp.ToolName);
                                    WriteLine("Do you want to delete it?");
                                    while (!end)
                                    {
                                        WriteLine("1. Yes\n2. No");
                                        Write("Select option from menu (0 to exit) - ");
                                        if (!Int32.TryParse(ReadLine(), out int inputstock) || inputstock < 1 || inputstock > 2)
                                        {
                                            WriteLine("Your input is not vali. Try again");
                                        }
                                        switch (inputstock)
                                        {
                                            case 1:
                                                DeleteToolExist(selectedCategory, selectedType, selectedTool, temp);
                                                end = true;
                                                break;
                                            case 2:
                                                end = true;
                                                break;
                                        }
                                    }
                                }

                            }
                            break;
                        }
                        else
                        {
                            WriteLine();
                            WriteLine("There is no item in this category and type");
                            break;
                        }
                    }
                    break;
                }
            }
            else
                WriteLine("There is no registered items yet");
        }

        public void DeleteToolExist(int selectedCategory, int selectedType, int selectedTool, Tool aTool) // This method is for deleteing tool exisit from the system (when the tool stock count is 0).
        {
            for (int i = selectedTool; i < ToolList.GetLength(2); i++)
            {
                if (ToolList[selectedCategory, selectedType, i] != null)
                {
                    ToolList[selectedCategory, selectedType, i] = ToolList[selectedCategory, selectedType, i + 1];
                }
                else break;
            }
            int index = BinarySearch(registeredTool, aTool.ToolName.Replace(" ", String.Empty).ToUpper());
            WriteLine(index);
            for (int i = index; i < registeredTool.Length; i++)
            {
                if (registeredTool[i] != null)
                {
                    registeredTool[i] = registeredTool[i + 1];
                }
                else break;
            }
        }

        public void ShowToolList() // This is used for showing the category list and the type list 
        {
            int selectedCategory;
            int selectedType;
            if (0 < KindTools)
            {
                ShowOption(out selectedCategory, out selectedType);

                WriteLine();
                for (int i = 0; i < ToolList.GetLength(2); i++)
                {
                    if (ToolList[selectedCategory, selectedType, 0] != null)
                    {
                        if (ToolList[selectedCategory, selectedType, i] == null)
                        {
                            break;
                        }
                        else
                        {
                            if (i == 0)
                            {
                                WriteLine("         {0,-37} {1, 20}", "Name", "Available items");
                            }
                            WriteLine(ToolList[selectedCategory, selectedType, i].ToString());
                        }
                    }
                    else
                    {
                        WriteLine();
                        WriteLine("There is no item in this category and type");
                        break;
                    }
                }
            }
            else
                WriteLine("There is no registered items yet");

        }

        public void BorrowTool(Member member, MemberCollection aMemberCollction)
        {
            int selectedCategory;
            int selectedType;
            int selectedTool;
            int counter = -9999;
            bool matched = false;

            if (0 < KindTools)
            {
                WriteLine("======Borrow tools======");
                ShowOption(out selectedCategory, out selectedType);
                while (true)
                {
                    WriteLine();


                    for (int i = 0; i < ToolList.GetLength(2); i++)
                    {
                        if (ToolList[selectedCategory, selectedType, 0] != null)
                        {
                            if (i == 0)
                            {
                                WriteLine();
                                WriteLine("         {0,-14} {1, 20}", "Name", "Available items");
                            }
                            if (ToolList[selectedCategory, selectedType, i] == null)
                            {
                                counter = i;
                                break;
                            }
                            else
                            {
                                WriteLine("{0}. {1}", i + 1, ToolList[selectedCategory, selectedType, i].ToString());
                                counter = i;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (counter != -9999)
                    {
                        WriteLine();
                        Write("Select item from above - ");
                        if (!Int32.TryParse(ReadLine(), out int inputNumber) || inputNumber < 1 || counter < inputNumber)
                        {
                            WriteLine();
                            WriteLine("Please input correctly");
                            WriteLine();
                            continue;
                        }
                        selectedTool = inputNumber - 1;

                        Tool temp = ToolList[selectedCategory, selectedType, selectedTool];
                        if (0 < temp.AvailableItems)
                        {
                            WriteLine("You have {0} tools now.\n" +
                                "Therefore, you can borrow {1} more tool(s)", member.NoHoldingTools, 5 - member.NoHoldingTools);
                            WriteLine();
                            WriteLine("You are chooing {0}", temp.ToolName);
                            if (temp.AvailableItems > 5 - member.NoHoldingTools)
                            {
                                while (true)
                                {
                                    WriteLine();
                                    Write("How many do you want to borrow? (Between 0 to {0}) >> ", 5 - member.NoHoldingTools);
                                    if (!Int32.TryParse(ReadLine(), out int input) || input < 0 || input > 5 - member.NoHoldingTools)
                                    {
                                        WriteLine();
                                        WriteLine("Please input valid number");
                                    }
                                    else
                                    {
                                        if (input != 0)
                                        {
                                            if (temp.Popularity == 0)
                                            {
                                                Popularity[BorrowedListCount] = temp;
                                                BorrowedListCount++;
                                            }

                                            if (temp.Stock == temp.AvailableItems) //When this tool is not borrowed currently then is borrowednow, the tool stores in BorrowTool array.
                                            {
                                                for (int i = 0; i < BorrowedTool.Length; i++)
                                                {
                                                    if (BorrowedTool[i] == null)
                                                    {
                                                        BorrowedTool[i] = temp;
                                                        borrowedToolCount++;
                                                        break;
                                                    }
                                                }
                                            }
                                            temp.AvailableItems -= input;
                                            temp.Popularity += input;
                                            aMemberCollction.BorrowByMember(member, temp, input);
                                            foreach (var matchedTool in member.HoldingTools) // Check if the member already borrow the same tool.
                                            {
                                                if (matchedTool == temp)
                                                {
                                                    matched = true;
                                                    break;
                                                }
                                            }

                                            if (matched) // If the member didnt't have the tool, the member will be added BorrowingMember in Tool class. This is useful to show the all member who are borrowing a tool currently.
                                            {
                                                for (int i = 0; i < temp.BorrowingMember.Length; i++)
                                                {
                                                    if (temp.BorrowingMember[i] == null || temp.BorrowingMember[i] == deletedMember)
                                                    {
                                                        temp.BorrowingMember[i] = member;
                                                        break;
                                                    }
                                                }
                                            }
                                            for (int i = 0; i < temp.BorrowingMember.Length; i++)
                                            {
                                                if (temp.BorrowingMember[i] != null)
                                                {
                                                    WriteLine(temp.BorrowingMember[i]);
                                                }
                                            }

                                            Popularity = MergeSort<Tool>.StartSortByPopularity(Popularity);
                                            for (int i = 0; i < Popularity.Length; i++)
                                            {
                                                if (Popularity[i] != null)
                                                {
                                                    WriteLine(Popularity[i].ToString() + Popularity[i].Popularity);
                                                }
                                                else break;
                                            }
                                            break;
                                        }
                                        else break;

                                    }
                                }
                            }
                            else
                            {
                                while (true)
                                {
                                    WriteLine();
                                    Write("How many do you want to borrow? (Between 0 to {0}) >> ", temp.AvailableItems);
                                    if (!Int32.TryParse(ReadLine(), out int input) || input < 0 || input > temp.AvailableItems)
                                    {
                                        WriteLine();
                                        WriteLine("Please input valid number");
                                    }
                                    else
                                    {
                                        if (temp.Popularity == 0)
                                        {
                                            Popularity[BorrowedListCount] = temp;
                                            BorrowedListCount++;
                                        }
                                        if (temp.Stock == temp.AvailableItems) //When this tool is not borrowed currently then is borrowednow, the tool stores in BorrowTool array.
                                        {
                                            for (int i = 0; i < BorrowedTool.Length; i++)
                                            {
                                                if (BorrowedTool[i] == null)
                                                {
                                                    BorrowedTool[i] = temp;
                                                    borrowedToolCount++;
                                                    break;
                                                }
                                            }
                                        }
                                        temp.AvailableItems -= input;
                                        temp.Popularity += input;
                                        aMemberCollction.BorrowByMember(member, temp, input);
                                        foreach (var matchedTool in member.HoldingTools) // Check if the member already borrow the same tool.
                                        {
                                            if (matchedTool == temp)
                                            {
                                                matched = true;
                                                break;
                                            }
                                        }

                                        if (matched) // If the member didnt't have the tool, the member will be added BorrowingMember in Tool class. This is useful to show the all member who are borrowing a tool currently.
                                        {
                                            for (int i = 0; i < temp.BorrowingMember.Length; i++)
                                            {
                                                if (temp.BorrowingMember[i] == null || temp.BorrowingMember[i] == deletedMember)
                                                {
                                                    temp.BorrowingMember[i] = member;
                                                    break;
                                                }
                                            }
                                        }
                                        for (int i = 0; i < temp.BorrowingMember.Length; i++)
                                        {
                                            if (temp.BorrowingMember[i] != null)
                                            {
                                                WriteLine(temp.BorrowingMember[i].ToString());
                                            }
                                        }
                                        Popularity = MergeSort<Tool>.StartSortByPopularity(Popularity);
                                        /*for (int i = 0; i < Popularity.Length; i++)
                                        {
                                            if (Popularity[i] != null)
                                            {
                                                WriteLine(Popularity[i].ToString() + Popularity[i].Popularity);
                                            }
                                            else break;
                                        }*/
                                        break;
                                    }
                                }
                            }

                            WriteLine();
                            WriteLine("Operation Successful");
                            WriteLine();

                            break;
                        }
                        else
                        {
                            WriteLine();
                            WriteLine("Sorry, but there is no available now.");
                            break;
                        }
                    }
                    else
                    {
                        WriteLine();
                        WriteLine("There is no item in this category and type");
                        break;
                    }
                }
            }
            else
                WriteLine("There is no registered items yet");
        }

        public void ReturnTool(Member member)
        {
            Tool temp;
            int index = 1;
            bool deleteTool = false;
            WriteLine("======Return a tool======");
            WriteLine();
            foreach (Tool borrowingTool in member.HoldingTools)
            {
                WriteLine("{0}. {1}", index, borrowingTool.ToolName);
                index++;
            }
            while (true)
            {
                Write("Select category from above - ");
                if (!Int32.TryParse(ReadLine(), out int option) || option < 1 || member.HoldingTools.Count < option)
                {
                    WriteLine();
                    WriteLine("Please input correctly");
                    WriteLine();
                    continue;
                }
                else
                {
                    option--;
                    temp = member.HoldingTools[option];
                    temp.AvailableItems++;
                    member.HoldingTools.RemoveAt(option);
                    member.NoHoldingTools--;
                    if (member.NoHoldingTools != 0)
                    {
                        foreach (var tool in member.HoldingTools)
                        {
                            if (tool == temp)
                            {
                                deleteTool = false;
                                break;
                            }
                        }
                    }
                    else {
                        deleteTool = true;
                    }
                    if (deleteTool)
                    {
                        for (int i = 0; i < temp.BorrowingMember.Length; i++)
                        {
                            if (temp.BorrowingMember[i] == member)
                            {
                                temp.BorrowingMember[i] = deletedMember;
                                break;
                            }
                        }
                    }
                    for (int i = 0; i < temp.BorrowingMember.Length; i++)
                    {
                        if (temp.BorrowingMember[i] != null)
                        {
                            WriteLine(temp.BorrowingMember[i].ToString());
                        }
                    }
                    if (temp.Stock == temp.AvailableItems)
                    {
                        for (int i = 0; i < BorrowedTool.Length; i++) //if the number of stock and available is the same, that means the tool is not borrowed.Therefore, the tool should be deleted from the BorrowedTool array.
                        {
                            if (BorrowedTool[i] == temp)
                            {
                                BorrowedTool[i] = null;
                                borrowedToolCount--;
                            }
                        }
                    }
                    WriteLine();
                    WriteLine("Operation Successful");
                    WriteLine();
                    break;
                }
            }
        }


        public void ShowOption(out int selectedCategory, out int selectedType)
        {
            while (true)
            {
                WriteLine("==========Category selection==========");
                WriteLine();
                for (int i = 0; i < categories.Length; i++)
                {
                    WriteLine("{0}: {1}", i + 1, categories[i]);
                }
                Write("Select category from above - ");
                if (!Int32.TryParse(ReadLine(), out int inputCategory) || inputCategory < 1 || categories.Length < inputCategory)
                {
                    WriteLine();
                    WriteLine("Please input correctly");
                    WriteLine();
                    continue;
                }
                selectedCategory = inputCategory - 1;
                break;
            }
            while (true)
            {
                WriteLine();
                WriteLine("==========Type selection==========");
                WriteLine();
                for (int i = 0; i < types[selectedCategory].Length; i++)
                {
                    WriteLine("{0}: {1}", i + 1, types[selectedCategory][i]);
                }
                Write("Select category from above - ");
                if (!Int32.TryParse(ReadLine(), out int inputType) || inputType < 1 || types[selectedCategory].Length < inputType)
                {
                    WriteLine();
                    WriteLine("Please input correctly");
                    continue;
                }
                selectedType = inputType - 1;
                break;
            }
        }

        public static int BinarySearch(Tool[] arr, string target)
        {
            var minimum = 0;
            var maximum = -1;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != null)
                {
                    maximum = i;
                }
                else break;
            }
            while (minimum <= maximum)
            {
                var middle = minimum + (maximum - minimum) / 2;
                switch (target.CompareTo(arr[middle].ToolName.Replace(" ", String.Empty).ToUpper()))
                {
                    case 1: //Bigger than middle
                        minimum = middle + 1;
                        break;
                    case -1: //Smaller than middle
                        maximum = middle - 1;
                        break;
                    case 0:
                        return middle;
                }
            }
            return -1; //There is no matched value.
        }

        public void DisplayTop3()
        {
            string top3 = "";
            int index = 0; //After being borrowed the tool which have never been borrowed so far, the index will be increment. 

            WriteLine("======Displaying top three (3) most frequently borrowed tools======");
            if (BorrowedListCount == 0)      //That is, after first item is added in the array, borrowedListCount is 1. Therefore, it needs to subtract 1 to show first tool. 
            {
                WriteLine();
                WriteLine("No tool has borrowed yet");
            }
            else if (BorrowedListCount == 1)
            {
                WriteLine();
                WriteLine("Only one tool has been borrowed so far.\n{0}", Popularity[index].ToolName);
            }
            else if (BorrowedListCount == 2)
            {
                WriteLine();
                WriteLine("Only two tools has been borrowed so far.\n{0}, {1}", Popularity[index].ToolName, Popularity[index + 1].ToolName);
            }
            else {
                WriteLine();
                WriteLine("Top three (3) most frequently borrowed tools are below.\n" +
                    "(if more than three items has been borrowed the same times, the all tools are shown)");
                WriteLine();
                for (int i = 0; i < BorrowedListCount; i++)
                {
                    if (Popularity[i] != null)
                    {
                        WriteLine(Popularity[i].ToolName + "     " + Popularity[i].Popularity);
                    }
                    else break;
                }
                for (int i = 0; i < BorrowedListCount; i++)
                {
                    if (i == 0)
                    {
                        top3 += Popularity[i].ToolName;
                    }
                    else
                    {
                        if (i > 2 && Popularity[i].Popularity != Popularity[i - 1].Popularity)
                        {
                            break;
                        }
                        else
                        {
                            top3 += ", " + Popularity[i].ToolName;
                        }
                    }
                }
                WriteLine(top3);
            }
        }

        public void DisplayAllMembers()
        {
            int options = 0;
            int index = 0;
            Tool temp;
            WriteLine("======Display all the members who are borrowing a tool======");
            WriteLine();
            while (true)
            {
                WriteLine("Below the tool list shows the borrowed tools currently.\n" +
               "You can choose a tool, whcich you want to display all members in.");
                WriteLine();
                for (int i = 0; i < BorrowedTool.Length; i++)
                {
                    if (BorrowedTool[i] == null)
                    {
                        break;
                    }
                    else WriteLine("{0}. {1}", i + 1, BorrowedTool[i].ToolName);
                    options = i + 1;
                }
                Write("Select category from above - ");
                if (!Int32.TryParse(ReadLine(), out int input) || input < 1 || input > options)
                {
                    WriteLine();
                    WriteLine("Please input correctly");
                    WriteLine();
                    continue;
                }
                index = input- 1;
                break;
            }
            temp = BorrowedTool[index];
            WriteLine();
            WriteLine("=====The members who are borrowing {0} currently======", temp.ToolName);
            for (int i = 0; i < BorrowedTool.Length; i++)
            {
                if (temp.BorrowingMember[i] == null)
                {
                    break;
                }
                else if (temp.BorrowingMember[i] == deletedMember)
                { }
                else
                {
                    WriteLine(temp.BorrowingMember[i].ShowFullName());
                }
            }
        }
    }
}

