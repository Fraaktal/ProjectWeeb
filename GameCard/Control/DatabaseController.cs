using System.IO;
using LiteDB;
using ProjectWeeb.GameCard.Business;
using ProjectWeeb.GameCard.Business.BusinessData;
using ProjectWeeb.GameCard.Business.ModelLiteDbClass;
using ProjectWeeb.GameCard.Control.Converter;
using ProjectWeeb.GameCard.Manager;

namespace ProjectWeeb.GameCard.Control
{
    public class DatabaseController
    {
        private const string CARD_TABLE = "ModelCardLiteDb";
        private const string USER_TABLE = "ModelUserLiteDb";
        private const string DECK_TABLE = "ModelDeckLiteDb";

        public DatabaseController(WebSiteManager webSiteManager)
        {
            WebSiteManager = webSiteManager;
        }

        private WebSiteManager WebSiteManager { get; set; }

        private string DatabasePath
        {
            get
            {
                string path = Path.Combine(Path.GetTempPath(), "ProjectWeeb","LiteDb");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return Path.Combine(path, "Weeb.db");
            }
        }

        #region Card

        public void SaveCard(Card card)
        {
            using (var database = new LiteDatabase(DatabasePath))
            {
                CardConverter cardConverter = new CardConverter();

                ModelCardLiteDb modelCardLiteDb = cardConverter.ConvertToModel(card);

                var col = database.GetCollection<ModelCardLiteDb>(CARD_TABLE);

                col.EnsureIndex(x => x.Id);

                col.Insert(modelCardLiteDb);
            }
        }

        public Card getCard(int id)
        {
            using (var database = new LiteDatabase(DatabasePath))
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
            using (var database = new LiteDatabase(DatabasePath))
            {
                var col = database.GetCollection<ModelCardLiteDb>(CARD_TABLE);

                var value = new BsonValue(id);
                col.Delete(value);
            }
        }

        #endregion

        #region User

        public void RegisterUser(User user)
        {
            using (var database = new LiteDatabase(DatabasePath))
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
            using (var database = new LiteDatabase(DatabasePath))
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
            using (var database = new LiteDatabase(DatabasePath))
            {
                var col = database.GetCollection<ModelUserLiteDb>(USER_TABLE);

                var value = new BsonValue(id);
                col.Delete(value);
            }
        }

        public bool DoesUserExist(string login)
        {
            using (var database = new LiteDatabase(DatabasePath))
            {
                var col = database.GetCollection<ModelUserLiteDb>(USER_TABLE);

                var modelUser = col.FindOne(c =>(c.UserName.Contains(login)));

                if (modelUser != null)
                {
                    return true;
                }

                return false;
            }
        }

        #endregion

        #region Deck

        public void SaveDeck(Deck deck)
        {
            using (var database = new LiteDatabase(DatabasePath))
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
            using (var database = new LiteDatabase(DatabasePath))
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
            using (var database = new LiteDatabase(DatabasePath))
            {
                var col = database.GetCollection<ModelDeckLiteDb>(DECK_TABLE);

                var value = new BsonValue(id);
                col.Delete(value);
            }
        }

        #endregion
    }
}