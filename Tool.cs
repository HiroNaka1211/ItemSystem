using System;
using System.Collections.Generic;
using System.Text;

namespace IFN664
{
    class Tool : IComparable
    {
        private string _toolName;
        private int _count;
        private int _popularity;
        private int _availableitems; //The number of the left item
        private int _stock; //This is the number of stocks which includes borrowed items.
        private Member[] _bowworingMember = new Member[20];

        public string ToolName
        {
            get { return _toolName; }
            set { _toolName = value; }
        }

        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }
        public int Popularity
        {
            get { return _popularity; }
            set { _popularity = value; }
        }
        public int Stock //This is the total number of the item. This number will not be changed
        {
            get { return _stock; }
            set { _stock = value; }
        }
        public int AvailableItems
        {
            get { return _availableitems; }
            set { _availableitems = value; }
        }
        public Member[] BorrowingMember // This will be used for showing the all members who are borrowing this tool.
        {
            get { return _bowworingMember; }
            set { _bowworingMember = value; }
        }

        public Tool() { }
        public Tool(string toolName, int availableitem)
        {
            ToolName = toolName;
            Stock = availableitem;
            AvailableItems = availableitem;
        }
        public Tool(string toolName, int availableitem, int popularity)
        {
            ToolName = toolName;
            Stock = availableitem;
            AvailableItems = availableitem;
            Popularity = popularity;
        }

        public override string ToString()
        {
            return /*"Item name: " + ItemName + "The number of available item: " + AvailableItems;*/
                String.Format("{0,-55}  {1, -8}",ToolName,AvailableItems );
        }
        
        public int CompareTo(Object obj) //This method is used for sort by name. This time, item name is unique. Therefore, name will be used for comparing.
        {
            Tool another = (Tool)obj;
            if (this.ToolName.CompareTo(another.ToolName) < 0)
                return -1;
            else
            {
                if (this.ToolName.Replace(" ", String.Empty).ToUpper().CompareTo(another.ToolName.Replace(" ", String.Empty).ToUpper()) == 0)
                    return this.ToolName.Replace(" ", String.Empty).ToUpper().CompareTo(another.ToolName.Replace(" ", String.Empty).ToUpper());
                else return 1;
            }
                

        }

        public int CompareNumber(Object obj)
        {
            Tool another = (Tool)obj;
            if (this.Popularity.CompareTo(another.Popularity) < 0)
                return -1;
            else
            {
                if (this.Popularity.CompareTo(another.Popularity) == 0)
                    return this.Popularity.CompareTo(another.Popularity);
                else return 1;
            }
        }
    }
}
