using LiteDB;
using ProjectWeeb.GameCard.Business.BusinessData;
using ProjectWeeb.GameCard.Business.ModelLiteDbClass;
using ProjectWeeb.GameCard.Control.Converter;
using ProjectWeeb.GameCard.Helper;

namespace ProjectWeeb.GameCard.Control.DAO
{
    public class CUserDAO
    {
        private const string USER_TABLE = "ModelUserLiteDb";

        private static CUserDAO _instance;

        private CUserDAO()
        {

        }

        public static CUserDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CUserDAO();
            }

            return _instance;
        }

        public void RegisterUser(User user)
        {
            using (var database = new LiteDatabase(WeebPathHelper.DatabasePath))
            {
                UserConverter userConverter = new UserConverter();

                ModelUserLiteDb modelUserLiteDb = userConverter.ConvertToModel(user);

                var col = database.GetCollection<ModelUserLiteDb>(USER_TABLE);
                
                col.EnsureIndex(x => x.Id, true);

                col.Insert(modelUserLiteDb);
            }
        }

        public bool UpdateUser(User user)
        {
            bool result = false;

            using (var database = new LiteDatabase(WeebPathHelper.DatabasePath))
            {
                UserConverter userConverter = new UserConverter();

                ModelUserLiteDb modelUserLiteDb = userConverter.ConvertToModelUpdate(user);

                var col = database.GetCollection<ModelUserLiteDb>(USER_TABLE);

                if (col.FindOne(u => u.Id == user.Id) != null)
                {
                    result = col.Update(modelUserLiteDb);
                }
            }

            return result;
        }

        public User GetUserByLoginAndPassword(string login, string password)
        {
            ModelUserLiteDb modelUser = null;
            using (var database = new LiteDatabase(WeebPathHelper.DatabasePath))
            {
                var col = database.GetCollection<ModelUserLiteDb>(USER_TABLE);

                modelUser = col.FindOne(c => c.UserName.Contains(login) && c.Password.Contains(password));
            }

            if (modelUser != null)
            {
                UserConverter userConverter = new UserConverter();

                return userConverter.ConvertToBusiness(modelUser);
            }

            return null;
        }

        public void DeleteAccount(int id)
        {
            using (var database = new LiteDatabase(WeebPathHelper.DatabasePath))
            {
                var col = database.GetCollection<ModelUserLiteDb>(USER_TABLE);

                var value = new BsonValue(id);
                col.Delete(value);
            }
        }

        public bool DoesUserExist(string login)
        {
            using (var database = new LiteDatabase(WeebPathHelper.DatabasePath))
            {
                var col = database.GetCollection<ModelUserLiteDb>(USER_TABLE);

                var modelUser = col.FindOne(c => (c.UserName.Contains(login)));

                if (modelUser != null)
                {
                    return true;
                }

                return false;
            }
        }

        
    }
}