using LiteDB;
using ProjectWeeb.GameCard.Business.BusinessData;
using ProjectWeeb.GameCard.Business.ModelLiteDbClass;
using ProjectWeeb.GameCard.Control.Converter;
using ProjectWeeb.GameCard.Helper;

namespace ProjectWeeb.GameCard.Control.DAO
{
    public class CDeckDAO
    {
        private const string DECK_TABLE = "ModelDeckLiteDb";

        private static CDeckDAO _instance;

        private CDeckDAO()
        {

        }

        public static CDeckDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CDeckDAO();
            }

            return _instance;
        }

        public void SaveDeck(Deck deck)
        {
            using (var database = new LiteDatabase(WeebPathHelper.DatabasePath))
            {
                DeckConverter deckConverter = new DeckConverter();


                var col = database.GetCollection<ModelDeckLiteDb>(DECK_TABLE);

                if (col.FindOne(d => d.Id == deck.Id) == null)
                {
                    ModelDeckLiteDb modelDeckLiteDb = deckConverter.ConvertToModel(deck);

                    col.EnsureIndex(x => x.Id);

                    col.Insert(modelDeckLiteDb);
                }
                else
                {
                    ModelDeckLiteDb modelDeckLiteDb = deckConverter.ConvertToModelUpdate(deck);

                    col.Update(modelDeckLiteDb);
                }
            }
        }

        public Deck GetDeck(int id)
        {
            using (var database = new LiteDatabase(WeebPathHelper.DatabasePath))
            {
                DeckConverter deckConverter = new DeckConverter();

                var col = database.GetCollection<ModelDeckLiteDb>(DECK_TABLE);

                var modelDeck = col.FindOne(c => c.Id == id);

                if (modelDeck != null)
                {
                    return deckConverter.ConvertBusiness(modelDeck);
                }

                return null;
            }
        }

        public void DeleteDeck(int id)
        {
            using (var database = new LiteDatabase(WeebPathHelper.DatabasePath))
            {
                var col = database.GetCollection<ModelDeckLiteDb>(DECK_TABLE);

                var value = new BsonValue(id);
                col.Delete(value);
            }
        }

        public Deck GetDeckByUserIdAndName(int userId, string defaultdeck)
        {
            using (var database = new LiteDatabase(WeebPathHelper.DatabasePath))
            {
                DeckConverter deckConverter = new DeckConverter();

                var col = database.GetCollection<ModelDeckLiteDb>(DECK_TABLE);

                var modelDeck = col.FindOne(c => c.IdUser == userId && c.Name.Contains(defaultdeck));

                if (modelDeck != null)
                {
                    return deckConverter.ConvertBusiness(modelDeck);
                }

                return null;
            }
        }
    }
}
