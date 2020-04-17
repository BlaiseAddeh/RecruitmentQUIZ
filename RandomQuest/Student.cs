using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomQuest
{
	public class Student
	{
		string name;
		public Student(string name)
		{ Name = name; }
		public string Name { get => name; set => name = value; }
	}
}
