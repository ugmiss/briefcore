using System;
using System.Data;
using System.Configuration;
using System.Web;


namespace Entities
{
    [Serializable]
    public class Person
    {
        private int num = 0;

        public int Num
        {
            get { return num; }
            set { num = value; }
        }
        public Person() { }

        public Person(int id,string name,string password,int age) {

            this.ID = id;
            this.Name = name;
            this.Password = password;
            this.Age = age;
            num++;
            
        
        }
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Password;

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        private int _Age;

        public int Age
        {
            get { return _Age; }
            set {
                if (value < 0) value = 0;                
                _Age = value; }
        }

    }
}
