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

        public DatabaseController(WebSiteManager webSiteManager, LiteDatabase database)
        {
            WebSiteManager = webSiteManager;
            Database = database;
        }

        private WebSiteManager WebSiteManager { get; set; }

        private LiteDatabase Database { get; set; }

        #region Card

        public void SaveCard(Card card)
        {
            CardConverter cardConverter = new CardConverter();

            ModelCardLiteDb modelCardLiteDb = cardConverter.ConvertToModel(card);

            var col = Database.GetCollection<ModelCardLiteDb>(CARD_TABLE);

            col.EnsureIndex(x => x.Id);

            col.Insert(modelCardLiteDb);
        }

        public Card getCard(int id)
        {
            CardConverter cardConverter = new CardConverter();

            var col = Database.GetCollection<ModelCardLiteDb>(CARD_TABLE);

            var modelCard = col.FindOne(c => c.Id == id);

            Card card = cardConverter.ConvertBusiness(modelCard);

            return card;
        }

        public void DeleteCard(int id)
        {
            var col = Database.GetCollection<ModelCardLiteDb>(CARD_TABLE);

            var value = new BsonValue(id);
            col.Delete(value);
        }

        #endregion

        #region User

        public void RegisterUser(User user)
        {
            UserConverter userConverter = new UserConverter();

            ModelUserLiteDb modelUserLiteDb = userConverter.ConvertToModel(user);

            var col = Database.GetCollection<ModelUserLiteDb>(USER_TABLE);

            col.EnsureIndex(x => x.Id, true);

            col.Insert(modelUserLiteDb);
        }

        public User LogUser(string login, string password)
        {
            UserConverter userConverter = new UserConverter();

            var col = Database.GetCollection<ModelUserLiteDb>(USER_TABLE);

            var modelUser = col.FindOne(c => (c.UserName.Equals(login) || c.MailAdress.Equals(login)) && c.Password.Equals(password));

            User user = userConverter.ConvertToBusiness(modelUser);

            return user;
        }

        public void DeleteAccount(int id)
        {
            var col = Database.GetCollection<ModelUserLiteDb>(USER_TABLE);

            var value = new BsonValue(id);
            col.Delete(value);
        }

        #endregion

        #region Deck

        public void SaveDeck(Deck deck)
        {
            DeckConverter deckConverter = new DeckConverter();

            ModelDeckLiteDb modelDeckLiteDb = deckConverter.ConvertToModel(deck);

            var col = Database.GetCollection<ModelDeckLiteDb>(DECK_TABLE);

            col.EnsureIndex(x => x.Id);

            col.Insert(modelDeckLiteDb);
        }

        public Deck GetDeck(int id)
        {
            DeckConverter deckConverter = new DeckConverter();

            var col = Database.GetCollection<ModelDeckLiteDb>(DECK_TABLE);

            var modelDeck = col.FindOne(c => c.Id == id);

            Deck deck = deckConverter.ConvertBusiness(modelDeck);

            return deck;
        }

        public void DeleteDeck(int id)
        {
            var col = Database.GetCollection<ModelDeckLiteDb>(DECK_TABLE);

            var value = new BsonValue(id);
            col.Delete(value);
        }

        #endregion


    }
}
