using System;
using System.Collections.Generic;
using System.Text;

namespace IFN664
{
    static class MergeSort<Type> where Type: IComparable
    {
        public static Type[,,] StartSort(Type[,,] arr, int selectedCategory, int selectedType) //For three dimentional array (Toolscolleciton)
        {
            Type[] tools = new Type[20];
            int count = 0;
            for (int i = 0; i < arr.GetLength(2); i++)
            {
                if (arr[selectedCategory, selectedType, i] != null)
                {
                    tools[i] = arr[selectedCategory, selectedType, i];
                    count++;
                }
                else break;
            }
                tools = Excuse(tools, 0, count);
                for (int i = 0; i < tools.Length; i++)
                {
                    if (tools[i] != null)
                    {
                        arr[selectedCategory, selectedType, i] = tools[i];
                    }
                    else break;
                }
            return arr;
        }

        private static Type[] Excuse(Type[] arr, int begin, int end)
        {
            if (begin + 1 == end)
            {
                return new Type[] { arr[begin] };
            }

            int middle = (begin + end) / 2;
            var firstArray = Excuse(arr, begin, middle);
            var secondArray = Excuse(arr, middle, end);
            var sortedArray = Merge(firstArray, secondArray);

            return sortedArray;
        }

        private static Type[] Merge(Type[] firstArray, Type[] secondArray)
        {
            int i = 0;
            int j = 0;
            var sortedArray = new Type[firstArray.Length + secondArray.Length];
            int index = 0; //Used for megedArray

            while (i < firstArray.Length && j < secondArray.Length)
            {
                if (firstArray[i].CompareTo(secondArray[j]) < 0)
                {
                    sortedArray[index] = firstArray[i];
                    i++;
                    index++;
                }
                else
                {
                    sortedArray[index] = secondArray[j];
                    j++;
                    index++;
                }
            }

            for (; i < firstArray.Length; i++, index++)
            {
                sortedArray[index] = firstArray[i];
            }
            for (; j < secondArray.Length; j++, index++)
            {
                sortedArray[index] = secondArray[j];
            }

            return sortedArray;
        }


        //Below methods is used for sorting the registered item array. This array will be used to search if the array has the same name item which staff wants to add.
        public static Tool[] StartSortForRegistration(Tool[] arr) //This is used for sorting Tool by its Popularity 
        {
            Tool[] tools = new Tool[20];
            int count = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != null) //The given array has null value because the size of array set big enough.
                {                   //Therefore, if this loop finds null, after the index place deos not a value. That is, null;
                    tools[i] = arr[i];
                    count++;
                }
                else break;
            }
            tools = ExcuseByName(tools, 0, count);
            for (int i = 0; i < tools.Length; i++)
            {
                if (tools[i] != null)
                {
                    arr[i] = tools[i];
                }
                else break;
            }
            return arr;
        }

        private static Tool[] ExcuseByName(Tool[] arr, int begin, int end)
        {
            if (begin + 1 == end)
            {
                return new Tool[] { arr[begin] };
            }

            int middle = (begin + end) / 2;
            var firstArray = ExcuseByName(arr, begin, middle);
            var secondArray = ExcuseByName(arr, middle, end);
            var sortedArray = MergeByName(firstArray, secondArray);

            return sortedArray;
        }


        private static Tool[] MergeByName(Tool[] firstArray, Tool[] secondArray)
        {
            int i = 0;
            int j = 0;
            var sortedArray = new Tool[firstArray.Length + secondArray.Length];
            int index = 0; //Used for megedArray

            while (i < firstArray.Length && j < secondArray.Length)
            {
                if (firstArray[i].ToolName.CompareTo(secondArray[j].ToolName) < 0)
                {
                    sortedArray[index] = firstArray[i];
                    i++;
                    index++;
                }
                else
                {
                    sortedArray[index] = secondArray[j];
                    j++;
                    index++;
                }
            }

            for (; i < firstArray.Length; i++, index++)
            {
                sortedArray[index] = firstArray[i];
            }
            for (; j < secondArray.Length; j++, index++)
            {
                sortedArray[index] = secondArray[j];
            }

            return sortedArray;
        }



        //Below methods for The most  top three (3) most frequently borrowed tools

        public static Tool[] StartSortByPopularity(Tool[] arr) //This is used for sorting Tool by its Popularity 
        {
            Tool[] tools = new Tool[20];
            int count = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != null) //The given array has null value because the size of array set big enough.
                {                   //Therefore, if this loop finds null, after the index place deos not a value. That is, null;
                    tools[i] = arr[i];
                    count++;
                }
                else break;
            }
            tools = ExcuseByPopularity(tools, 0, count);
            for (int i = 0; i < tools.Length; i++)
            {
                if (tools[i] != null)
                {
                    arr[i] = tools[i];
                }
                else break;
            }
            return arr;
        }

        private static Tool[] ExcuseByPopularity(Tool[] arr, int begin, int end)
        {
            if (begin + 1 == end)
            {
                return new Tool[] { arr[begin] };
            }

            int middle = (begin + end) / 2;
            var firstArray = ExcuseByPopularity(arr, begin, middle);
            var secondArray = ExcuseByPopularity(arr, middle, end);
            var sortedArray = MergeByPopularity(firstArray, secondArray);

            return sortedArray;
        }


        private static Tool[] MergeByPopularity(Tool[] firstArray, Tool[] secondArray)
        {
            int i = 0;
            int j = 0;
            var sortedArray = new Tool[firstArray.Length + secondArray.Length];
            int index = 0; //Used for megedArray

            while (i < firstArray.Length && j < secondArray.Length)
            {
                if (firstArray[i].Popularity.CompareTo(secondArray[j].Popularity) > 0)
                {
                    sortedArray[index] = firstArray[i];
                    i++;
                    index++;
                }
                else
                {
                    sortedArray[index] = secondArray[j];
                    j++;
                    index++;
                }
            }

            for (; i < firstArray.Length; i++, index++)
            {
                sortedArray[index] = firstArray[i];
            }
            for (; j < secondArray.Length; j++, index++)
            {
                sortedArray[index] = secondArray[j];
            }

            return sortedArray;
        }
    }
}
