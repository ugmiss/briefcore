using System;
using System.Data;
using System.Configuration;
using System.Web;


namespace Entity
{
    [Serializable]
    public class UserInfo
    {
        public UserInfo() {
            this._id = 1;
            this._name = "ace";
            this.Password = "123456";
        }
        public UserInfo(int id,string name,string password) {

            this._id = id;
            this._name = name;
            this.Password = password;
        
        }
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

    }
}
