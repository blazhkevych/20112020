using System;
using System.Collections.Generic;
using System.Linq;
namespace _20112020
{
    class Student : IComparable<Student>
    {
        public string PIB { get; set; }
        public DateTime Birthday { get; set; }
        public string Academy { get; set; }
        public int Age
        {
            get
            {
                DateTime now = DateTime.Today;
                int age = now.Year - Birthday.Year;
                if (Birthday > now.AddYears(-age)) age--;
                return age;
            }
        }
        private Dictionary<string, List<int>> Journal;
        public Student(string PIB, DateTime Bday, string Acad)
        {
            this.PIB = PIB;
            Birthday = Bday;
            Academy = Acad;
            Journal = new Dictionary<string, List<int>>();
        }
        public override string ToString()
        {
            string str = $"|{PIB,15}| {Age,2} | {Birthday:dd.MM.yyyy} |{Academy,10}|\n";
            foreach (var subject in Journal)
            {
                // str += $"\t\t|{subject.Key,15}|{string.Join('|', subject.Value)}|\n";
                str += $"\t\t|{subject.Key,10}|{subject.Value.Average(),6:N2}|{string.Join('|', subject.Value.Select(x => $"{x,2}"))}|\n";
            }
            return str;
        }
        //public int CompareTo(object obj)
        //{
        //    if (obj is Student st)
        //        return String.Compare(PIB, st.PIB);
        //    throw new InvalidCastException();
        //}

        public void NewMark(string subject, int mark)
        {
            if (!Journal.ContainsKey(subject))
            {
                Journal.Add(subject, new List<int>());
                // Journal[subject]=new List<int>();
            }
            Journal[subject].Add(mark);
        }
        public int CompareTo(Student other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var pibComparison = string.Compare(PIB, other.PIB, StringComparison.Ordinal);
            if (pibComparison != 0) return pibComparison;
            var birthdayComparison = Birthday.CompareTo(other.Birthday);
            if (birthdayComparison != 0) return birthdayComparison;
            return string.Compare(Academy, other.Academy, StringComparison.Ordinal);
        }
    }
    internal class CmpAge : IComparer<Student>
    {
        public int Compare(Student x, Student y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(x, null)) return -1;
            return x.Age.CompareTo(y.Age);
        }
    }
    class Group
    {
        public string Name { get; set; }
        private List<Student> list = new List<Student>();
        public void AddStudent(Student st) => list.Add(st);
        public override string ToString() =>
                 $"{new string('-', 50)}\nGroup name: {Name}\n" +
                 $"{String.Join('\r', list.Select(s => s.ToString()))}" +
                 $"{new string('-', 50)}\n";
        public void Sort(IComparer<Student> comparer = null)
        {
            list.Sort(comparer);
        }
        public void Sort(Comparison<Student> comparison)
        {
            list.Sort(comparison);
        }
        public void DeleteStudent(Student st)
        {
            list.Remove(st);
        }
        public Student FindStudent(string name)
        {
            return list.Find(s => 0 == String.Compare(s.PIB, name, StringComparison.CurrentCultureIgnoreCase));
        }
    }

    class Program
    {
        static Random rnd = new Random();
        static void TestList()
        {
            Student Ivan = new Student("Ivan", new DateTime(2000, 10, 20), "IT Step");
            Console.WriteLine(Ivan);
            Console.WriteLine("----------------------------");
            Ivan.NewMark("c++", 5);
            Ivan.NewMark("c#", 12);
            Ivan.NewMark("c++", 8);
            Ivan.NewMark("c++", 3);
            Ivan.NewMark("c++", 8);
            Ivan.NewMark("c++", 6);
            Console.WriteLine(Ivan);
            Console.WriteLine("----------------------------");
        }
        static void RandMarksStudent(Student st)
        {
            for (int i = 0; i < 10; i++)
            {
                st.NewMark("php", rnd.Next(1, 13));
                st.NewMark("c++", rnd.Next(1, 13));
                st.NewMark("c#", rnd.Next(1, 13));
            }
        }
        static void TestGroup()
        {
            Group gr = new Group() { Name = "PE911" };
            Student Ivan = new Student("Ivan", new DateTime(2000, 10, 20), "IT Step");
            RandMarksStudent(Ivan);
            gr.AddStudent(Ivan);
            Student Piter = new Student("Piter", new DateTime(2012, 11, 15), "it Step");
            RandMarksStudent(Piter);
            gr.AddStudent(Piter);
            Student inna = new Student("Inna", new DateTime(2012, 11, 25), "ZHDTU");
            RandMarksStudent(inna);
            gr.AddStudent(inna);
            Student Anna = new Student("Anna", new DateTime(1999, 10, 20), "ZHATC");
            RandMarksStudent(Anna);
            gr.AddStudent(Anna);
            Student Taras = new Student("Taras", new DateTime(1986, 10, 20), "ZHDU");
            RandMarksStudent(Taras);
            gr.AddStudent(Taras);
            gr.DeleteStudent(Taras);
            // gr.Sort();
            // gr.Sort( new CmpAge());
            // gr.Sort((x,y)=>string.Compare(x.Academy,y.Academy, StringComparison.CurrentCultureIgnoreCase));
            //gr.Sort((x,y)=>x.Age.CompareTo(y.Age));
            //  gr.Sort((x,y)=>-x.Age.CompareTo(y.Age));
            Console.WriteLine(gr);
            Student f = gr.FindStudent("Ann6a");
            Console.WriteLine(f);

        }
        static void Main(string[] args)
        {
            //TestList();
            TestGroup();
        }
    }
}