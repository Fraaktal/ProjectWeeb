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

                ModelDeckLiteDb modelDeckLiteDb = deckConverter.ConvertToModel(deck);

                var col = database.GetCollection<ModelDeckLiteDb>(DECK_TABLE);

                col.EnsureIndex(x => x.Id);

                col.Insert(modelDeckLiteDb);
            }
        }

        public Deck GetDeck(int id)
        {
            using (var database = new LiteDatabase(WeebPathHelper.DatabasePath))
            {
                DeckConverter deckConverter = new DeckConverter();

                var col = database.GetCollection<ModelDeckLiteDb>(DECK_TABLE);

                var modelDeck = col.FindOne(c => c.Id == id);

                Deck deck = deckConverter.ConvertBusiness(modelDeck);

                return deck;
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
    }
}
