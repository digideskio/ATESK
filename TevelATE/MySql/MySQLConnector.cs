using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TevelATE
{

    public struct OUTPUTPowerEffTest
    {
        public int channel;
        public float Frequency;
        public ushort DDSCode;
        public float Power;
        public float Voltage;
        public float Current;
        public float irTemp1;
        public float irTemp2;
        public float irTemp3;
        public float irTemp4;
        public float irTemp5;
        public bool pass_fail;
        public string datetime;
        public double efficiency;
        public int testid;
        public string reasonToLogInDb;
    }
    public struct StationInfo
    {
        public string guid;
        public DateTime added;
        public string description;
        public int userId;
        public string Alias;
        public string strCouplerSerial;
        public string cablefix;
        public usersDb.User user;
    }
    public struct CirculatorIsolationTest
    {
        public int channel;
        public float frequency;
        public ushort ddscode;
        public float loadtemp;
        public float transistortemp;
        public int testid;
        public string failreason;
    }

    public static class MySQLConnector
    {

        static string myConnectionString;
        static Object m_lock = new Object();

        static MySQLConnector()
        {

        }
        public static void Initialize(string server, string password)
        {
            myConnectionString = string.Format("server={0};database=teveldb;uid=root;pwd={1};", server, password);
        }

        public static int AddNewStation(string guid, string Alias)
        {
            lock (m_lock)
            {
                using (MySqlConnection cn = new MySqlConnection(myConnectionString))
                {

                    try
                    {

                        string query = @"INSERT INTO station (guid, Alias) 
                                       VALUES (@guid, @Alias)";

                        cn.Open();


                        Int32 newId = 0;
                        using (MySqlCommand cmd = new MySqlCommand(query, cn))
                        {

                            cmd.Parameters.AddWithValue("@guid", guid);
                            cmd.Parameters.AddWithValue("@Alias", Alias);
                            newId = cmd.ExecuteNonQuery();
                            newId = (Int32)cmd.LastInsertedId;
                        }
                        cn.Close();
                        return newId;
                    }
                    catch (MySqlException err)
                    {
                        throw (new SystemException(err.Message));
                    }
                }
            }
        }
   
        public static void DeleteUser(int id)
        {
            lock (m_lock)
            {
                using (MySqlConnection cn = new MySqlConnection(myConnectionString))
                {
                    try
                    {
                        string query = "DELETE from users  where id = " + id;
                        cn.Open();

                        using (MySqlCommand cmd = new MySqlCommand(query, cn))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                        }
                        cn.Close();
                    }
                    catch (MySqlException err)
                    {
                        throw (new SystemException(err.Message));
                    }
                }
            }
        }

        public static void SaveUserChanges(usersDb.User u)
        {
            lock (m_lock)
            {
                using (MySqlConnection cn = new MySqlConnection(myConnectionString))
                {

                    SHA256Managed crypt = new SHA256Managed();
                    string passwordhash = String.Empty;
                    byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(u.password), 0, Encoding.ASCII.GetByteCount(u.password));
                    foreach (byte theByte in crypto)
                    {
                        passwordhash += theByte.ToString("x2");
                    }

                    try
                    {
                        string query = @"UPDATE users SET username = @username ,
                                            passwordhash=@passwordhash , 
                                            qmsusername = @qmsusername , 
                                           firstname = @firstname , 
                                           lastname = @lastname ,
                                           phone = @phone , 
                                           active = @active  where id = @id 
                                           and typeofuser = @typeofuser";
                        cn.Open();

                        using (MySqlCommand cmd = new MySqlCommand(query, cn))
                        {

                            cmd.Parameters.AddWithValue("@id", u.ID);
                            cmd.Parameters.AddWithValue("@username", u.userName);
                            cmd.Parameters.AddWithValue("@typeofuser", u.typeOfUser);
                            cmd.Parameters.AddWithValue("@employeeId", u.employeeId);
                            cmd.Parameters.AddWithValue("@firstname", u.firstName);
                            cmd.Parameters.AddWithValue("@passwordhash", passwordhash);
                            cmd.Parameters.AddWithValue("@lastname", u.lastName);
                            cmd.Parameters.AddWithValue("@phone", u.phoneNumber);
                            cmd.Parameters.AddWithValue("@active", u.active);

                            // Execute the query
                            cmd.ExecuteNonQuery();
                        }
                        cn.Close();
                    }
                    catch (MySqlException err)
                    {
                        throw (new SystemException(err.Message));
                    }
                }
            }
        }

        public static void SuspendUser(int id, bool suspend)
        {
            lock (m_lock)
            {
                using (MySqlConnection cn = new MySqlConnection(myConnectionString))
                {
                   
                    try
                    {
                        string query = "UPDATE users SET active = @active where id = @id";

                        cn.Open();


                        using (MySqlCommand cmd = new MySqlCommand(query, cn))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            int active = suspend == true ? 0 : 1;
                            cmd.Parameters.AddWithValue("@active", active);
                            cmd.ExecuteNonQuery();
                        }
                        cn.Close();
                    }
                    catch (MySqlException err)
                    {
                        throw (new SystemException(err.Message));
                    }
                }
            }
        }

        public static void CreateNewUser(usersDb.User u)
        {
            lock (m_lock)
            {
                using (MySqlConnection cn = new MySqlConnection(myConnectionString))
                {


                    SHA256Managed crypt = new SHA256Managed();
                    string passwordhash = String.Empty;
                    byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(u.password), 0, Encoding.ASCII.GetByteCount(u.password));
                    foreach (byte theByte in crypto)
                    {
                        passwordhash += theByte.ToString("x2");
                    }

                    try
                    {
                        string query = @"INSERT INTO users (username, typeofuser, passwordhash, firstname,
                                                            lastname, phone, active,employeeId) VALUES (@username, @typeofuser, @passwordhash,@firstname, 
                                                            @lastname, @phone, @active, @employeeId);";

                        cn.Open();


                        using (MySqlCommand cmd = new MySqlCommand(query, cn))
                        {

                            cmd.Parameters.AddWithValue("@username", u.userName);
                            cmd.Parameters.AddWithValue("@typeofuser", u.typeOfUser);
                            cmd.Parameters.AddWithValue("@passwordhash", passwordhash);
                            cmd.Parameters.AddWithValue("@firstname", u.firstName);
                            cmd.Parameters.AddWithValue("@employeeId", u.employeeId);
                            cmd.Parameters.AddWithValue("@lastname", u.lastName);
                            cmd.Parameters.AddWithValue("@phone", u.phoneNumber);
                            cmd.Parameters.AddWithValue("@active", u.active);

                            // Execute the query
                            cmd.ExecuteNonQuery();
                        }
                        cn.Close();
                    }
                    catch (MySqlException err)
                    {
                        throw (new SystemException(err.Message));
                    }
                }
            }
        }
        public static StationInfo getStationInfo(string guid)
        {
            lock (m_lock)
            {
                StationInfo info = new StationInfo();
                using (MySqlConnection cn = new MySqlConnection(myConnectionString))
                {

                    try
                    {

                        string query = "SELECT * FROM station where guid = @guid";
                        cn.Open();
                        using (MySqlCommand cmd = new MySqlCommand(query, cn))
                        {
                            cmd.Parameters.AddWithValue("@guid",  guid );
                            MySqlDataReader dataReader = cmd.ExecuteReader();
                            if (dataReader.Read())
                            {
                                info.guid = dataReader["guid"].ToString();
                                info.Alias = dataReader["Alias"].ToString();
                            }
                        }
                        cn.Close();
                        return info;
                    }
                    catch (MySqlException err)
                    {
                        throw (new SystemException(err.Message));
                    }
                }
            }
        }

        public static List<usersDb.User> GetAllUsers()
        {
            lock (m_lock)
            {
                using (MySqlConnection cn = new MySqlConnection(myConnectionString))
                {

                    try
                    {

                        string query = "SELECT * FROM users";
                        cn.Open();
                        List<usersDb.User> U = new List<usersDb.User>();
                        using (MySqlCommand cmd = new MySqlCommand(query, cn))
                        {


                            MySqlDataReader dataReader = cmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                usersDb.User u = new usersDb.User();
                                u.ID = int.Parse(dataReader["ID"].ToString());
                                u.userName = dataReader["username"].ToString();
                                u.typeOfUser = int.Parse(dataReader["typeofuser"].ToString());
                                u.firstName = dataReader["firstname"].ToString();
                                try
                                {
                                    u.employeeId = dataReader["qmsusername"].ToString();
                                }
                                catch (Exception)
                                {
                                    u.employeeId = string.Empty;
                                }
                                u.lastName =  dataReader["lastname"].ToString();
                                u.phoneNumber = dataReader["phone"].ToString();
                                if (int.Parse(dataReader["active"].ToString()) == 1)
                                    u.active = true;
                                else
                                    u.active = false;
                                U.Add(u);
                            }
                        }
                        cn.Close();
                        return U;
                    }
                    catch (MySqlException err)
                    {
                        throw (new SystemException(err.Message));
                    }
                }
            }
        }

        public static int CheckAuthtintication(string userName, string password, int typeOfUser)
        {
            lock (m_lock)
            {
                using (MySqlConnection cn = new MySqlConnection(myConnectionString))
                {
                    

                    try
                    {

                        SHA256Managed crypt = new SHA256Managed();
                        string passwordhash = String.Empty;
                        byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(password), 0, Encoding.ASCII.GetByteCount(password));
                        foreach (byte theByte in crypto)
                        {
                            passwordhash += theByte.ToString("x2");
                        }
                        string query = "SELECT ID FROM users where username= @username and passwordhash = @password and typeofuser = @typeofuser";

                        cn.Open();

                        
                        int ID = -1;
                        using (MySqlCommand cmd = new MySqlCommand(query, cn))
                        {

                            cmd.Parameters.AddWithValue("@username", userName);
                            cmd.Parameters.AddWithValue("@typeofuser", typeOfUser);
                            cmd.Parameters.AddWithValue("@password", passwordhash);

                            MySqlDataReader dataReader = cmd.ExecuteReader();

                            //Read the data and store them in the list
                            while (dataReader.Read())
                            {
                                ID = int.Parse(dataReader["ID"].ToString());
                            }
                        }
                        cn.Close();
                        return ID;
                    }
                    catch (MySqlException err)
                    {
                        throw (new SystemException(err.Message));
                    }
                }
            }
        }

        public static int GetUserID(string userName, int typeofuser)
        {
            lock (m_lock)
            {
                using (MySqlConnection cn = new MySqlConnection(myConnectionString))
                {


                    try
                    {
                        string query = "SELECT ID FROM users where username= @username and typeofuser = @typeofuser";

                        cn.Open();


                        int ID = -1;
                        using (MySqlCommand cmd = new MySqlCommand(query, cn))
                        {
                            

                            cmd.Parameters.AddWithValue("@username", userName);
                            cmd.Parameters.AddWithValue("@typeofuser", typeofuser);



                            MySqlDataReader dataReader = cmd.ExecuteReader();

                            //Read the data and store them in the list
                            while (dataReader.Read())
                            {
                                ID = int.Parse(dataReader["ID"].ToString());
                            }
                        }
                        cn.Close();
                        return ID;
                    }
                    catch (MySqlException err)
                    {
                        throw (new SystemException(err.Message));
                    }
                }
            }
        }

    
        public static int UpdateTestPassFail(int id, int passFail)
        {
            lock (m_lock)
            {
                using (MySqlConnection cn = new MySqlConnection(myConnectionString))
                {

                    try
                    {
                        string query = "UPDATE test SET passfail = @passfail where id = " + id;

                        cn.Open();

                        
                        Int32 newId = 0;
                        using (MySqlCommand cmd = new MySqlCommand(query, cn))
                        {

                            cmd.Parameters.AddWithValue("@passfail", passFail);

                            //newId = (Int32)cmd.ExecuteScalar();
                            // Execute the query
                            newId = cmd.ExecuteNonQuery();

                            newId = (Int32)cmd.LastInsertedId;


                        }
                        cn.Close();
                        return newId;
                    }
                    catch (MySqlException err)
                    {
                        throw (new SystemException(err.Message));
                    }
                }
            }
        }
      
        public static int RunNewtest(string serialNumber, string partNumber, string datetime, int testnum, int userid, bool passFail, int channel, string fieldGuid)
        {
            lock (m_lock)
            {
                using (MySqlConnection cn = new MySqlConnection(myConnectionString))
                {
                   
                    try
                    {

                        string query = @"INSERT INTO test (SerialNumber, datetime, testnum, userid, passfail, channel, guid, partnumber) 
                                VALUES (@serialNumber, @datetime, @testnum , @userid, @passfail, @channel, @guid, @partNumber);";

                        cn.Open();

                        
                        Int32 newId = 0;
                        using (MySqlCommand cmd = new MySqlCommand(query, cn))
                        {

                            cmd.Parameters.AddWithValue("@partNumber", partNumber);
                            cmd.Parameters.AddWithValue("@serialNumber", serialNumber);
                            cmd.Parameters.AddWithValue("@datetime", datetime);
                            cmd.Parameters.AddWithValue("@testnum", testnum);
                            cmd.Parameters.AddWithValue("@userid", userid);
                            cmd.Parameters.AddWithValue("@channel", channel);
                            cmd.Parameters.AddWithValue("@guid", fieldGuid);
                            cmd.Parameters.AddWithValue("@passfail", passFail == true? 1 : 0);

                            //newId = (Int32)cmd.ExecuteScalar();
                            // Execute the query
                            newId = cmd.ExecuteNonQuery();

                            newId = (Int32)cmd.LastInsertedId;


                        }
                        cn.Close();
                        return newId;
                    }
                    catch (MySqlException err)
                    {
                        throw (new SystemException(err.Message));
                    }
                }
            }
        }
               
        public static int AddFailedResult(string serialNumber,
                                          string partNumber,
                                          string datetime,
                                          int testid,
                                          int userid,
                                          string reason,
                                          int channel, 
                                          int testnumber)
        {
            lock (m_lock)
            {
                using (MySqlConnection cn = new MySqlConnection(myConnectionString))
                {

                    try
                    {
                        string query = @"INSERT INTO fail_results (SerialNumber, datetime, testid, userid, reason, channel, testnumber, partnumber) 
                                        VALUES (@serialNumber, @datetime, @testid , @userid , @reason, @channel,@testnumber,@partNumber);";

                        cn.Open();


                        Int32 newId = 0;
                        using (MySqlCommand cmd = new MySqlCommand(query, cn))
                        {

                            cmd.Parameters.AddWithValue("@partNumber", partNumber);
                            cmd.Parameters.AddWithValue("@serialNumber", serialNumber);
                            cmd.Parameters.AddWithValue("@datetime", datetime);
                            cmd.Parameters.AddWithValue("@testid", testid);
                            cmd.Parameters.AddWithValue("@testnumber", testnumber);
                            cmd.Parameters.AddWithValue("@userid", userid);
                            cmd.Parameters.AddWithValue("@reason", reason);
                            cmd.Parameters.AddWithValue("@channel", channel);

                            //newId = (Int32)cmd.ExecuteScalar();
                            // Execute the query
                            newId = cmd.ExecuteNonQuery();

                            newId = (Int32)cmd.LastInsertedId;

                        }
                        cn.Close();
                        return newId;
                    }
                    catch (MySqlException err)
                    {
                        throw (new SystemException(err.Message));
                    }
                }
            }
        }         
      
        public static TimeSpan GetTotalWorkTime()
        {

            lock (m_lock)
            {

        
                using (MySqlConnection cn = new MySqlConnection(myConnectionString))
                {

                    try
                    {

                        int hour;
                        int days;
                        int minute;
                        int id;
                        int seconds;

                        string query = "SELECT * FROM total_work";

                        cn.Open();

                        bool IsExist;
                        using (MySqlCommand cmd = new MySqlCommand(query, cn))
                        {
                            MySqlDataReader dataReader = cmd.ExecuteReader();

                            //Read the data and store them in the list
                            IsExist = dataReader.Read();
                            if (IsExist)
                            {
                                hour = int.Parse(dataReader["hour"].ToString());
                                minute = int.Parse(dataReader["minute"].ToString());
                                seconds = int.Parse(dataReader["seconds"].ToString());
                                days = int.Parse(dataReader["days"].ToString());
                                id = int.Parse(dataReader["ID"].ToString());

                                TimeSpan t = new TimeSpan(days, hour, minute, seconds);
                                cn.Close();
                                return t;
                            }
                            else
                            {
                                return new TimeSpan(0, 0, 0, 0);
                            }                          
                        }
                    }
                    catch (Exception err)
                    {
                        throw (new SystemException(err.Message));
                    }
                }
            }
        }
        public static int UpdateTotalWork(DateTime start, DateTime stop)
        {
            lock (m_lock)
            {

                TimeSpan timeToAdd = stop - start;

                using (MySqlConnection cn = new MySqlConnection(myConnectionString))
                {
                    try
                    {

                        int hour ;
                        int days ;
                        int minute;
                        int id;
                        int seconds; 

                        string query = "SELECT * FROM total_work";

                        cn.Open();

                        bool IsExist;
                        using (MySqlCommand cmd = new MySqlCommand(query, cn))
                        {

                            MySqlDataReader dataReader = cmd.ExecuteReader();

                            //Read the data and store them in the list
                            IsExist = dataReader.Read();
                            if (IsExist)
                            {
                                hour = int.Parse(dataReader["hour"].ToString());
                                minute = int.Parse(dataReader["minute"].ToString());
                                seconds = int.Parse(dataReader["seconds"].ToString());
                                days = int.Parse(dataReader["days"].ToString());
                                id = int.Parse(dataReader["ID"].ToString());


                                TimeSpan t = new TimeSpan(days, hour, minute, seconds);
                                timeToAdd = t + timeToAdd;

                            }
                            cn.Close();
                        }

                        cn.Open();

                        if (IsExist == false)
                        {
                            query = "INSERT INTO total_work (hour, minute, days,seconds, id) VALUES (@hour, @minute,@days, @seconds, @id)";
                        }
                        else
                        {
                            query = @"UPDATE total_work  SET days = @days, hour = @hour, minute = @minute, seconds = @seconds WHERE ID = @id";
                        }
                        using (MySqlCommand cmd = new MySqlCommand(query, cn))
                        {

                            hour = timeToAdd.Hours;
                            days = timeToAdd.Days;
                            minute = timeToAdd.Minutes;
                            seconds = timeToAdd.Seconds;

                            Int32 newId = 0;

                            cmd.Parameters.AddWithValue("@hour", hour);
                            cmd.Parameters.AddWithValue("@minute", minute);
                            cmd.Parameters.AddWithValue("@days", days);
                            cmd.Parameters.AddWithValue("@seconds", seconds);
                            cmd.Parameters.AddWithValue("@id", 1);

                            //newId = (Int32)cmd.ExecuteScalar();
                            // Execute the query
                            newId = cmd.ExecuteNonQuery();
                            newId = (Int32)cmd.LastInsertedId;

                            cn.Close();
                            return newId;
                        }                        
                         
                    }
                    catch (MySqlException err)
                    {
                        throw (new SystemException(err.Message));
                    }
                }
            }
        }

        public static int UpdateUsrtWorkTime(int userId, DateTime start, DateTime stop)
        {
            lock (m_lock)
            {

                TimeSpan timeToAdd = stop - start;

                using (MySqlConnection cn = new MySqlConnection(myConnectionString))
                {

                    try
                    {

                        int hour;
                        int days;
                        int minute;
                        int seconds;

                        string query = "SELECT * FROM user_worktime where userid = " + userId;

                        cn.Open();

                        bool IsExist;
                        using (MySqlCommand cmd = new MySqlCommand(query, cn))
                        {

                            MySqlDataReader dataReader = cmd.ExecuteReader();

                            //Read the data and store them in the list
                            IsExist = dataReader.Read();
                            if (IsExist)
                            {
                                hour = int.Parse(dataReader["hour"].ToString());
                                minute = int.Parse(dataReader["minutes"].ToString());
                                seconds = int.Parse(dataReader["seconds"].ToString());
                                days = int.Parse(dataReader["days"].ToString());

                                TimeSpan t = new TimeSpan(days, hour, minute, seconds);
                                timeToAdd = t + timeToAdd;

                            }
                            cn.Close();
                        }

                        cn.Open();

                        if (IsExist == false)
                        {
                            query = "INSERT INTO user_worktime (hour, minutes, days,seconds, userid) VALUES (@hour, @minutes,@days, @seconds, @userid)";
                        }
                        else
                        {
                            query = @"UPDATE user_worktime  SET days = @days, hour = @hour, minutes = @minutes, seconds = @seconds WHERE userid = @userid";
                        }
                        using (MySqlCommand cmd = new MySqlCommand(query, cn))
                        {
                            hour = timeToAdd.Hours;
                            days = timeToAdd.Days;
                            minute = timeToAdd.Minutes;
                            seconds = timeToAdd.Seconds;

                            Int32 newId = 0;

                            cmd.Parameters.AddWithValue("@hour", hour);
                            cmd.Parameters.AddWithValue("@minutes", minute);
                            cmd.Parameters.AddWithValue("@days", days);
                            cmd.Parameters.AddWithValue("@seconds", seconds);                            
                            cmd.Parameters.AddWithValue("@userid", userId);

                            //newId = (Int32)cmd.ExecuteScalar();
                            // Execute the query
                            newId = cmd.ExecuteNonQuery();
                            newId = (Int32)cmd.LastInsertedId;

                            cn.Close();
                            return newId;
                        }

                    }
                    catch (MySqlException err)
                    {
                        throw (new SystemException(err.Message));
                    }
                }
            }
        }

  
        public static bool IsGoldenUnitExist()
        {
             lock (m_lock)
            {

                bool found = false;
                using (MySqlConnection cn = new MySqlConnection(myConnectionString))
                {

                    try
                    {
                        string query = @"SELECT * FROM pa_ate.golden_unit
                                         where DATE(datetime) = CURDATE()";

                        cn.Open();
                        found = false;

                        using (MySqlCommand cmd = new MySqlCommand(query, cn))
                        {

                            MySqlDataReader dataReader = cmd.ExecuteReader();
                            if (dataReader.Read())
                            {
                                found = true;
                            }
                        }
                        cn.Close();
                        return found;
                    }
                    catch (MySqlException err)
                    {
                        throw (new SystemException(err.Message));
                    }
                }
            }
        }
    }
}
