using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomQuest
{
	class Program
	{
		static void Main(string[] args)
		{
            List<Student> mylist = new List<Student>();

            mylist.Add(new Student("JAMES"));
            mylist.Add(new Student("DAVID"));
            mylist.Add(new Student("PAUL"));
            mylist.Add(new Student("GEORGE"));
            mylist.Add(new Student("LARRY"));
            mylist.Add(new Student("BLAISE"));
            mylist.Add(new Student("BENI"));
            mylist.Add(new Student("DEGNON"));
            mylist.Add(new Student("GEOFFROY"));
            mylist.Add(new Student("BEA"));
            mylist.Add(new Student("OYO"));
            mylist.Add(new Student("EXPEDIT"));
            mylist.Add(new Student("RAOUL"));
            mylist.Add(new Student("FIKE"));
            mylist.Add(new Student("TATA"));
            mylist.Add(new Student("SENY"));
            mylist.Add(new Student("NONGODO"));
            mylist.Add(new Student("SEIDOU"));
            mylist.Add(new Student("BINTA"));
            mylist.Add(new Student("ZELLY"));
            mylist.Add(new Student("ZATA"));

            //shuffle
            var rnd = new Random();
            var result = mylist.OrderBy(item => rnd.Next()).OrderBy(item => rnd.Next()).Take(7);

            foreach (var item in result)
            {
                Console.WriteLine(item.Name);
            }
            Console.ReadLine();
        }
	}
}
