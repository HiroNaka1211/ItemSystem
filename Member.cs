using System;
using static System.Console;
using System.Collections.Generic;
using System.Text;

namespace IFN664
{
    class Member
    {
        private string _firstName;
        private string _lastName;
        private string _contactNumber;
        private int _pin;
        private List<Tool> _holdingTools= new List<Tool>();
        private int _noHoldingTools;

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        public string Name
        {
            get { return (FirstName + LastName).Replace(" ", String.Empty).ToUpper(); }
        }
        public string ContactNumber
        {
            get { return _contactNumber; }
            set { _contactNumber = value; }
        }
        public int Pin
        {
            get { return _pin; }
            set { _pin = value; }
        }
        public List<Tool> HoldingTools
        {
            get { return _holdingTools; }
            set { _holdingTools = value; }
        }
        public int NoHoldingTools  //The number of the tools which the user is borrowing currently
        {
            get { return _noHoldingTools; }
            set { _noHoldingTools = value; }
        }

        public Member() { }
        public Member(string firtName, string lastName, string contactNumber, int pin)
        {
            FirstName = firtName;
            LastName = lastName;
            ContactNumber = contactNumber;
            Pin = pin;
        }

        public override string ToString()
        {
            return FirstName + " " + LastName + " " + ContactNumber;
        }

        public void ShowHoldingTool()//This is for showing borrowing tools for the user
        {
            string items="";
            var count = 0;
            foreach(Tool tool in HoldingTools)
            {
                if (count == 0)
                {
                    items += tool.ToolName;
                    count++;
                }
                else
                {
                    items += ", " + tool.ToolName;
                }
                
            }
            WriteLine("Borrowing tools: {0}", items);
        }
        public string ShowFullName()
        {
            return FirstName + " " + LastName;
        }
    }
}
