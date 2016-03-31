using System;
using System.Collections;

namespace CS_console
{
    class DataObject
    {
        // Populate
        private long count;
        private string val;
        private string reference;

        public DataObject(string value)
        {
            val = value;
            count = 0;
            reference = "";
        }
        
        public long Count
        {
            get { return count; }
            set { count = value; }
        }
        public void Inc()
        {
            count++;
        }
        public void Dec()
        {
            count--;
        }

        public string Val
        {
            get { return val; }
            set { val = value; }
        }

        public string Ref
        {
            get { return reference; }
            set { reference = value; }
        }
    }

    class Program
    {
        static Hashtable Data = new Hashtable();
        static string[] StaticData = new string[] { 
            "X-Ray","Echo","Alpha", "Yankee","Bravo", "Charlie", 
            "Delta",    "Hotel", "India", "Juliet", "Foxtrot","Sierra",
            "Mike","Kilo", "Lima",  "November", "Oscar", "Papa", "Qubec", 
            "Romeo",  "Tango","Golf", "Uniform", "Victor", "Whisky",
            "Zulu"};

        static void Main(string[] args)
        {
            for (int i = 0; i < StaticData.Length; i++)
                Data.Add(StaticData[i].ToLower(), new DataObject(StaticData[i]));
            while (true)
            {
                PrintSortedData();
                Console.WriteLine();
                Console.Write("> ");
                string str = Console.ReadLine();
                string[] strs = str.Split(' ');

                if (strs[0] == "q")
                    break;
                else if (strs[0] == "printv")
                    PrintSortedDataByValue();
                else if (strs[0] == "print")
                    PrintSortedData();
                else if (strs[0] == "inc")
                    Increase(strs[1]);
                else if (strs[0] == "dec")
                    Decrease(strs[1]);
                else if (strs[0] == "swap")
                    Swap(strs[1], strs[2]);
                else if (strs[0] == "ref")
                    Ref(strs[1], strs[2]);
                else if (strs[0] == "unref")
                    UnRef(strs[1]);
            }
        }

        /// <summary>
        /// Create a reference from one data object to another.
        /// </summary>
        /// <param name="key1">The object to create the reference on</param>
        /// <param name="key2">The reference object</param>
        static void Ref(string key1, string key2)
        {
            // Populate
            if (Data.ContainsKey(key1))
            {
                //update
                ((DataObject)Data[key1]).Ref= ((DataObject)Data[key2]).Val;
            }


        }

        /// <summary>
        /// Removes an object reference on the object specified.
        /// </summary>
        /// <param name="key">The object to remove the reference from</param>
        static void UnRef(string key)
        {
            // Populate
            if (Data.ContainsKey(key))
            {
                ((DataObject)Data[key]).Ref= "";
            }
        }

        /// <summary>
        /// Swap the data objects stored in the keys specified
        /// </summary>
        static void Swap(string key1, string key2)
        {
            // Populate
            if (Data.ContainsKey(key1) && Data.ContainsKey(key2))
            {
                DataObject tmpObj = ((DataObject)Data[key1]);
                Data[key1] = ((DataObject)Data[key2]);
                Data[key2] = tmpObj;
                //Console.WriteLine("key1:" + key1 + "<->" + "key2:" + key2);
            }
        }

        /// <summary>
        /// Decrease the Value field by 1 of the 
        /// data object stored with the key specified
        /// </summary>
        static void Decrease(string key)
        {
            // Populate
            if (Data.ContainsKey(key))
            {
                ((DataObject)Data[key]).Dec();
            }
        }

        /// <summary>
        /// Increase the Value field by 1 of the 
        /// data object stored with the key specified
        /// </summary>
        static void Increase(string key)
        {
            // Populate
            if (Data.ContainsKey(key))
            {
                ((DataObject)Data[key]).Inc();
            }
        }


        /// <summary>
        /// Prints the information in the Data hashtable to the console.
        /// Output should be sorted by key 
        /// References should be printed between '<' and '>'
        /// The output should look like the following : 
        /// 
        /// 
        /// Alpha...... -3
        /// Bravo...... 2
        /// Charlie.... <Zulu>
        /// Delta...... 1
        /// Echo....... <Alpha>
        /// --etc---
        /// 
        /// </summary>
        static void PrintSortedData()
        {
            // Populate
            ArrayList list = new ArrayList();
            list.AddRange(Data.Keys);
            list.Sort();
            for (int i = 0; i < list.Count; i++)
            {
                DataObject obj = (DataObject)Data[list[i]];
                //Console.Write("key:" + list[i].ToString() + " ");  //verify key if map to the 
                //Console.Write("...... ");Console.Write("...... "); // correct content of object
                Console.Write((string)obj.Val);
                Console.Write("...... ");
                if (obj.Ref == "")
                    Console.Write(obj.Count);
                else
                    Console.Write("<" + obj.Ref + ">");
                Console.WriteLine();
            }
        }


        /// <summary>
        /// Prints the information in the Data hashtable to the console.
        /// Output should be sorted by stored value
        /// References should be printed between '<' and '>'
        /// Sorting order start from max to min, larger value takes priority.
        /// The output should look like the following : 
        /// 
        /// 
        /// Bravo...... 100
        /// Echo...... 99
        /// Zulu...... 98
        /// Charlie.... <Zulu>
        /// Delta...... 34
        /// Echo....... 33
        /// Alpha...... <Echo>
        /// --etc---
        /// 
        /// </summary>
        static void PrintSortedDataByValue()
        {
            // Populate
            ArrayList sortValue = new ArrayList();
            sortValue.AddRange(Data.Keys);
            for (int i = sortValue.Count - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (((DataObject)Data[sortValue[j]]).Count < ((DataObject)Data[sortValue[j + 1]]).Count)
                    {
                        string tmp = sortValue[j].ToString();
                        sortValue[j] = sortValue[j + 1];
                        sortValue[j + 1] = tmp;
                    }
                }
            }
            // sorted by value, then print
            for (int i = 0; i < sortValue.Count; i++)
            {
                DataObject obj = (DataObject)Data[sortValue[i]];
                //Console.Write("key:" + sortValue[i].ToString() + " ");  //verify key if map to the 
                //Console.Write("...... ");Console.Write("...... ");      // correct content of object
                Console.Write((string)obj.Val);
                Console.Write("...... ");
                if (obj.Ref == "")
                    Console.Write(obj.Count);
                else
                    Console.Write("<" + obj.Ref + ">");
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
