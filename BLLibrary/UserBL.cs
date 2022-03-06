using DALibrary;
using ModelsLibrary;
using ErrorExeption;
using System.Diagnostics;

namespace BLLibrary
{
    public class UserBL
    {
        UserDAL userDAL;
        readonly int minUserNameLength = 2;
        readonly int maxUserNameLength = 20;
        readonly int minPasswordLength = 3;
        readonly int maxPasswordLength = 20;
        readonly int minAge = 0;
        readonly int maxAge = 200;
        public UserBL()
        {
            userDAL = new UserDAL();
        }
        public User RegisterUser(string userName, int userAge)
        {
            try
            {
                if (userName.Length < minUserNameLength || userName.Length > maxUserNameLength)
                {
                    throw new InvalidPropValue("user name", minUserNameLength, maxUserNameLength, "length");
                    return null;
                }
                if (userAge < minAge || userAge > maxAge)
                {
                    throw new InvalidPropValue("user age", minAge, maxAge);
                    return null;
                }
                return userDAL.Register(userName, userAge);
            }
            catch (InvalidPropValue e)
            {
                Console.WriteLine(e.errorMessage);
                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
                Console.WriteLine("Something went wrong");
                return null;
            }
        }
        public string LoginUser(int id, string password)
        {
            try
            {
                if (password.Length < minUserNameLength || password.Length > maxUserNameLength)
                {
                    throw new InvalidPropValue("password", minPasswordLength, maxPasswordLength, "length");
                    
                }
                return userDAL.Login(id, password);
            }
            catch (InvalidPropValue e)
            {
                Console.WriteLine(e.errorMessage);
                return null;
            }
            catch(Exception e)
            {                
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
                Console.WriteLine("Something went wrong");
                return null;
            }
        }


    }
}