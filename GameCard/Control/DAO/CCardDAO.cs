using LiteDB;
using ProjectWeeb.GameCard.Business.BusinessData;
using ProjectWeeb.GameCard.Business.ModelLiteDbClass;
using ProjectWeeb.GameCard.Control.Converter;
using ProjectWeeb.GameCard.Helper;

namespace ProjectWeeb.GameCard.Control.DAO
{
    public class CCardDAO
    {
        private const string CARD_TABLE = "ModelCardLiteDb";

        private static CCardDAO _instance;

        private CCardDAO()
        {

        }

        public static CCardDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CCardDAO();
            }

            return _instance;
        }

        public void SaveCard(Card card)
        {
            using (var database = new LiteDatabase(WeebPathHelper.DatabasePath))
            {
                CardConverter cardConverter = new CardConverter();

                ModelCardLiteDb modelCardLiteDb = cardConverter.ConvertToModel(card);

                var col = database.GetCollection<ModelCardLiteDb>(CARD_TABLE);

                col.EnsureIndex(x => x.Id);

                col.Insert(modelCardLiteDb);
            }
        }

        public Card GetCard(int id)
        {
            using (var database = new LiteDatabase(WeebPathHelper.DatabasePath))
            {
                CardConverter cardConverter = new CardConverter();

                var col = database.GetCollection<ModelCardLiteDb>(CARD_TABLE);

                var modelCard = col.FindOne(c => c.Id == id);

                Card card = cardConverter.ConvertBusiness(modelCard);

                return card;
            }
        }

        public void DeleteCard(int id)
        {
            using (var database = new LiteDatabase(WeebPathHelper.DatabasePath))
            {
                var col = database.GetCollection<ModelCardLiteDb>(CARD_TABLE);

                var value = new BsonValue(id);
                col.Delete(value);
            }
        }
    }
}