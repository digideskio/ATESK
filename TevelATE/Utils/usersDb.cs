using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TevelATE
{
    public class usersDb  
    {

        const string adminPassword = "1";

        public enum USERS_TYPES
        {
            OPERATOR,
            TECHNICIAN
        }
        public class User
        {
            public int ID;
            public int typeOfUser;
            public string userName;
            public string employeeId;
            public string password;
            public string firstName;
            public string lastName;
            public string phoneNumber;
            public bool active;
        }
        public static string GetAdminPassword()
        {
            return adminPassword;
        }
        
        public bool CheckAuthtintication(string userName, string password, int typeOfUser)
        {
            try
            {
                if (userName == "Admin" && password == GetAdminPassword())
                    return true;

                int id = MySQLConnector.CheckAuthtintication(userName, password, typeOfUser);
                if (id == -1)
                    return false;
                return true;
            }
            catch (Exception err)
            {
                throw (new SystemException(err.Message));
            }
        }
        public  int GetUserID(string userName, int typeofuser)
        {
            try
            {
                return MySQLConnector.GetUserID(userName, typeofuser);
            }
            catch (Exception err)
            {
                throw (new SystemException(err.Message));
            }
        }
        public  bool IsUserActive(string userName, int typeOfUser)
        {
            return false;
        }
        public  bool IsUserExist(string userName, int typeOfUser)
        {
            return false;
        }
        public  List<User> ReadAllUsers()
        {
            try
            {
                return MySQLConnector.GetAllUsers();
            }
            catch (Exception err)
            {
                throw (new SystemException(err.Message));
            }
        }

        public  void DeleteUser(int id)
        {
            try
            {
                MySQLConnector.DeleteUser(id);
            }
            catch (Exception err)
            {
                throw (new SystemException(err.Message));
            }
        }
        public  void SaveUserChanges(User user)              
        {

            try
            {
                MySQLConnector.SaveUserChanges(user);
            }
            catch (Exception err)
            {
                throw (new SystemException(err.Message));
            }
        }

        public  void CreateNewUser(User user)              
        {

            try
            {
                MySQLConnector.CreateNewUser(user);
            }
            catch (Exception err)
            {
                throw (new SystemException(err.Message));
            }
        }

        public  void SuspendUser(int id, bool suspend)
        {
            try
            {
                MySQLConnector.SuspendUser(id, suspend);
            }
            catch (Exception err)
            {
                throw (new SystemException(err.Message));
            }
        }
    }
}
