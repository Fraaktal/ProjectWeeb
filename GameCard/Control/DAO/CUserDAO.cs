using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public User LogUser(string login, string password)
        {
            using (var database = new LiteDatabase(WeebPathHelper.DatabasePath))
            {
                UserConverter userConverter = new UserConverter();

                var col = database.GetCollection<ModelUserLiteDb>(USER_TABLE);

                var modelUser = col.FindOne(c =>
                    c.UserName.Contains(login) && c.Password.Contains(password));

                User user = null;

                if (modelUser != null)
                {
                    user = userConverter.ConvertToBusiness(modelUser);
                }

                return user;
            }
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