using System;
using static System.Console;
using System.Collections.Generic;
using System.Text;

namespace IFN664
{
    class MemberCollection
    {
        private int count;
        private int buckets;
        private Member[] members;
        private Member emptyMember = new Member("Empty", "Empty", "Empty", 0000);
        private Member deletedMember = new Member("Deleted", "Deleted", "Deleted", 0000);
        private Member[] _holdingItemMember = new Member[200];

        public MemberCollection() { }
        public MemberCollection(int buckets)
        {
            if (buckets > 0)
                this.buckets = buckets;
            count = 0;
            members = new Member[buckets];
            for (int i = 0; i < buckets; i++)
            {
                members[i] = emptyMember;
            }
        }

        public Member[] Members
        {
            get { return members; }
        }
        public int Count
        {
            get { return count; }
        }

        public int Capacity
        {
            get { return buckets; }
            set { buckets = Capacity; }
        }

        public Member[] HoldingItemMember
        {
            get { return _holdingItemMember; }
            set { _holdingItemMember = value; }
        }


        //Quadratic probing is used for this hash table
        private int Hashing(string name)
        {
            int key = Math.Abs(name.GetHashCode());
            return (key % buckets);
        }

        public void Clear() {
            count = 0;
            for (int i = 0; i < buckets; i++)
            {
                members[i] = emptyMember;
            }
            /*for (int i = 0; i < members.Length; i++)
            {
                if (members[i] != emptyMember && members[i] != deletedMember)
                {
                    WriteLine($"{i}:  {members[i].ToString()}");
                }
            }*/
        }

        private int Find_Insertation_Bucket(string name)
        {
            int bucket = Hashing(name);
            int i = 0;
            int offset = 0;
            while ((i < buckets) &&
                (members[(bucket + offset) % buckets] != emptyMember) &&
                (members[(bucket + offset) % buckets] != deletedMember))
            {
                i++;
                offset = i * i;
            }
            return (offset + bucket) % buckets;
        }

        public void Add(Member member) // This is just for the test. I added some member first to check my project is working well;
        {
            string name = (member.FirstName + member.LastName).Replace(" ", String.Empty).ToUpper();
            if ((Count < members.Length) && (Search(name) == -1))
            {
                int bucket = Find_Insertation_Bucket(name);
                members[bucket] = member;
                count++;
            }

        }

        public void Add()
        {
            string firstName = "";
            string lastName = "";
            string mobile = "";
            int pin = -9999;
            bool end = false;
            WriteLine("You can register new member");
            WriteLine();
            while (!end)
            {
                var finish = false;
                var correctMobile = false;
                var correctPin = false;
                WriteLine("Please input new member details.");
                while (firstName == "")
                {
                    Write("Enter new member firstname >> ");
                    firstName = ReadLine().Replace(" ", String.Empty).ToUpper();
                }
                while (lastName == "")
                {
                    Write("Enter new member lastname >> ");
                    lastName = ReadLine().Replace(" ", String.Empty).ToUpper();
                }
                while (mobile == "")
                {
                    Write("Enter new member's mobile phone number(No hyphen, only numbers) >> ");
                    string phone = ReadLine();
                    correctMobile = long.TryParse(phone, out var phonen);
                    if (!correctMobile)
                    {
                        WriteLine();
                        WriteLine("The phone number must be assembled of only numbers");
                        WriteLine();
                        continue;
                    }
                    mobile = phone;
                }
                while (pin == -9999)
                {
                    Write("Enter new member's pin by 4 digits >> ");
                    string inputpin = ReadLine();
                    correctPin = Int32.TryParse(inputpin, out var pincode);
                    if (!correctPin || inputpin.Length != 4 || pincode < 0)
                    {
                        WriteLine();
                        WriteLine("The pin must be assembled of only 4 digits");
                        WriteLine();
                        continue;
                    }
                    pin = pincode;
                }
                WriteLine();
                WriteLine("First Name: {0}   Last Name: {1}   Phone Number: {2}   Pin: {3}", firstName, lastName, mobile, pin);
                WriteLine();
                while (!finish)
                {
                    WriteLine("Is your input correct?");
                    WriteLine("1. Yes\n2. No");
                    Write("Select option from menu - ");
                    string inputchoice = ReadLine();
                    bool correctInput = Int32.TryParse(inputchoice, out int select);
                    if (inputchoice == "" || !correctInput || select < 1 || 2 < select)
                    {
                        WriteLine();
                        WriteLine("Please input 1 or 2");
                        WriteLine();
                        continue;
                    }
                    else
                    {
                        switch (select)
                        {
                            case 1:
                                end = true;
                                finish = true;
                                break;
                            case 2:
                                firstName = "";
                                lastName = "";
                                mobile = "";
                                pin = -9999;
                                finish = true;
                                WriteLine();
                                break;
                        }
                    }
                }

            }
            Member member = new Member(firstName, lastName, mobile, pin);
            string name = (member.FirstName + member.LastName).Replace(" ", String.Empty).ToUpper();
            if ((Count < members.Length) && (Search(name) == -1))
            {
                int bucket = Find_Insertation_Bucket(name);
                members[bucket] = member;
                count++;
                WriteLine();
                WriteLine("Adding new member is successful");
                WriteLine();
            }
            else
            {
                WriteLine("The key has already been in the hashtable or the hashtable is full");
                WriteLine();
            }
        }

        public int Search(string name)
        {
            int bucket = Hashing(name);

            int i = 0;
            int offset = 0;
            while ((i < buckets) &&
                (members[(bucket + offset) % buckets].Name != name) &&
                (members[(bucket + offset) % buckets] != emptyMember))
            {
                i++;
                offset = i * i;
            }
            if (members[(bucket + offset) % buckets].Name == name)
            {
                return (offset + bucket) % buckets;
            }
            else {
                return -1;
            }
        }

        public void Delete()
        {
            string firstName = "";
            string lastName = "";
            if (Count != 0)
            {
                WriteLine("You can delete a registered member.");
                WriteLine();
                while (true)
                {
                    while (firstName == "")
                    {
                        Write("Enter new member firstname >> ");
                        firstName = ReadLine().Replace(" ", String.Empty).ToUpper();
                    }
                    while (lastName == "")
                    {
                        Write("Enter new member lastname >> ");
                        lastName = ReadLine().Replace(" ", String.Empty).ToUpper();
                    }
                    string name = firstName + lastName;
                    int bucket = Search(name);
                    if (bucket != 1)
                    {
                        if (members[bucket].NoHoldingTools > 0)
                        {
                            WriteLine();
                            WriteLine("{0} is borrowing tools. Therefore you cannot delete the member", members[bucket].ShowFullName());
                            break;
                        }
                        else
                        {
                            members[bucket] = deletedMember;
                            count--;
                            WriteLine();
                            WriteLine("Operation Successful");
                            WriteLine();
                            break;
                        }
                        
                    }
                    else
                    {
                        WriteLine("The given key is not in the hashtable");
                        break;
                    }
                }
            }
            else
                WriteLine("There is no registered members");
        }

        public void GetPhoneNumber() {
            string firstName = "";
            string lastName = "";
            if (Count != 0)
            {
                WriteLine("You can find a member's phone number.");
                WriteLine();
                while (true)
                {
                    while (firstName == "")
                    {
                        Write("Enter new member firstname >> ");
                        firstName = ReadLine().Replace(" ", String.Empty).ToUpper();
                    }
                    while (lastName == "")
                    {
                        Write("Enter new member lastname >> ");
                        lastName = ReadLine().Replace(" ", String.Empty).ToUpper();
                    }
                    string name = firstName + lastName;

                    int bucket = Search(name);
                    if (bucket != -1)
                    {
                        Member temp = members[bucket];
                        WriteLine("The phone number for {0} {1} is   {2}", temp.FirstName, temp.LastName, temp.ContactNumber);
                        WriteLine();
                        /*for (int i = 0; i < members.Length; i++)
                        {
                            if (members[i] != emptyMember && members[i] != deletedMember)
                            {
                                WriteLine($"{i}:  {members[i].ToString()}");
                            }
                        }*/
                        break;
                    }
                    else
                    {
                        WriteLine("The given key is not in the hashtable");
                        break;
                    }
                }
            }
            else
                WriteLine("There is no registered members");

        }

        public Member SearchMember()
        {
            string firstName = "";
            string lastName = "";
            int pin;
            if (Count != 0)
            {
                WriteLine("Please input your inforamtion.");
                WriteLine();
                while (firstName == "")
                {
                    Write("Enter new member firstname >> ");
                    firstName = ReadLine().Replace(" ", String.Empty).ToUpper();
                }
                while (lastName == "")
                {
                    Write("Enter new member lastname >> ");
                    lastName = ReadLine().Replace(" ", String.Empty).ToUpper();
                }
                while (true)
                {
                    Write("Enter the your pin >> ");
                    string input = ReadLine();
                    bool correctinput = Int32.TryParse(input, out int inputPin);
                    if (!correctinput || input.Length != 4 || inputPin < 0)
                    {
                        WriteLine();
                        WriteLine("The pin must be assembled of only 4 digits");
                        WriteLine();
                        continue;
                    }
                    else
                    {
                        pin = inputPin;
                        break;
                    }
                }
                string name = firstName + lastName;

                int bucket = Search(name);
                if (bucket != -1 && members[bucket].Pin == pin)
                {
                    Member temp = members[bucket];
                    WriteLine("Operation Successful");
                    WriteLine();
                    return temp;
                }
                else
                {
                    WriteLine("The given key is not in the hashtable");
                    return null;
                }
            }
            else return null;
        }

        public void BorrowByMember(Member member, Tool tool, int count)
        {
            for (int i = 0; i < count; i++)
            {
                member.HoldingTools.Add(tool);
            }
            member.NoHoldingTools += count;
        }
        /*public void ShowHoldingTools(Member member)
        {
            foreach (Tool tool in HoldingItemMember)
            {
                
            }
        }*/

        /*public void Return(Member member)
        {
            int i = 1;
            foreach (Tool borrowingTool in member.HoldingTools)
            {
                WriteLine("{0}. {1}", i, borrowingTool.ToolName);
                i++;
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
                    member.HoldingTools[option].AvailableItems++;
                    member.HoldingTools.RemoveAt(option);
                    member.NoHoldingTools--;
                    WriteLine();
                    WriteLine("Operation Successful");
                    WriteLine();
                    break;
                }
            }

        }*/

        /*public void ShowMembers()
        {
            WriteLine();
            WriteLine("The members who are borrowing tools are ");
            for (int i = 0; i < members.Length; i++)
            {
                if (members[i] != emptyMember && members[i] != deletedMember && members[i].NoHoldingTools > 0)
                {
                    WriteLine(members[i].FirstName + " " + members[i].LastName);
                }
            }
        }*/
    }
}
