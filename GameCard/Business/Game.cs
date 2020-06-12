using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using LiteDB.Engine;
using Microsoft.AspNetCore.SignalR.Client;
using ProjectWeeb.GameCard.Business.BusinessData;
using ProjectWeeb.GameCard.Manager;

namespace ProjectWeeb.GameCard.Business
{
    public class Game
    {
        public Game(Player p1)
        {
            Player1 = p1;
            Date = DateTime.Now;

            IsGameStarted = false;

            BattleField = new BattleField();

            GameConnection = new HubConnectionBuilder().WithUrl("http://trucmachin/GameHub").Build();
#if DEBUG
            GameConnection = new HubConnectionBuilder().WithUrl("http://localhost:50322/GameHub").Build();
#endif
            Connect();

            ListenControllerEvent();
        }
        public int CurrentPlayerIdTurn { get; set; }

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        private Timer Timer { get; set; }

        public string GameId
        {
            get { return GameConnection?.ConnectionId; }
        }

        public DateTime Date { get; set; }

        public bool AllPlayerConnected
        {
            get { return Player1 != null && Player2 != null; }
        }

        public bool IsGameStarted { get; set; }

        public BattleField BattleField { get; set; }

        public HubConnection GameConnection { get; set; }

        private async void Connect()
        {
            try
            {
                await GameConnection.StartAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async void Disconnect()
        {
            try
            {
                await GameConnection.StopAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void ListenControllerEvent()
        {
            GameConnection.On<int, string>("PlayerConnected", PlayerConnected);

            GameConnection.On<int, int, int, int>("CardPlayed", CardPlayed);
        }

        private async Task PlayerConnected(int idPlayer, string connectionId)
        {
            if (IsGameStarted)
            {
                await ReconnectPlayer(idPlayer,connectionId);
            }
            else
            {
                await ConnectPlayer(idPlayer, connectionId);
            }

        }

        private async Task CardPlayed(int idUser, int idCard, int position, int positionInHand)
        {
            int[] handCards = null;
            if (idUser == Player1.IdUser)
            {
                Card card = CardManager.GetInstance().GetCardById(idCard);
                BattleField.SetCardForCurrentPlayer(card,position);
                Player1.CurrentHand.RemoveAt(positionInHand);
                var pSide = BattleField.ComputePlayer1Side();
                handCards = GetCardsIds(Player1.CurrentHand);
                await GameConnection.InvokeAsync("CardSuccesfullyPlayed", Player1.ConnectionId, Player2.ConnectionId, pSide, handCards);
            }
            else
            {
                Card card = CardManager.GetInstance().GetCardById(idCard);
                BattleField.SetCardForCurrentPlayer(card, position);
                Player2.CurrentHand.RemoveAt(positionInHand);
                var pSide = BattleField.ComputePlayer2Side();
                handCards = GetCardsIds(Player2.CurrentHand);
                await GameConnection.InvokeAsync("CardSuccesfullyPlayed", Player2.ConnectionId, Player1.ConnectionId, pSide, handCards);
            }
        }

        private async Task ConnectPlayer(int idPlayer, string connectionId)
        {
            int[] handCards = null;
            List<Card> drawnCards = null;

            if (idPlayer == Player1.IdUser)
            {
                Player1.ConnectionId = connectionId;
                drawnCards = DrawCards(Player1.DrawPile, 5);

                foreach (var drawnCard in drawnCards)
                {
                    Player1.CurrentHand.Add(drawnCard);
                }

                handCards = GetCardsIds(Player1.CurrentHand);
            }
            else if (idPlayer == Player2.IdUser)
            {
                Player2.ConnectionId = connectionId;
                drawnCards = DrawCards(Player2.DrawPile, 5);

                foreach (var drawnCard in drawnCards)
                {
                    Player2.CurrentHand.Add(drawnCard);
                }

                handCards = GetCardsIds(Player2.CurrentHand);
            }

            await GameConnection.InvokeAsync("InitializeGame", connectionId, handCards);

            if (AllPlayerConnected)
            {
                CurrentPlayerIdTurn = Player1.IdUser;
                int[] p1handCards = GetCardsIds(Player1.CurrentHand);
                await GameConnection.InvokeAsync("SetPlayerTurn", Player1.ConnectionId, Player2.ConnectionId, p1handCards);
            }
        }
        
        private async Task ReconnectPlayer(int idPlayer, string connectionId)
        {
            //TODO
        }

        private int[] GetCardsIds(List<Card> cards)
        {
            int[] result = new int[cards.Count];
            for (int i = 0; i < cards.Count; i++)
            {
                result[i] = cards.ElementAt(i).CardId;
            }

            return result;
        }

        private List<Card> DrawCards(List<Card> cards, int amount)
        {
            List<Card> result = new List<Card>();
            for (int i = 0; i < amount; i++)
            {
                Card card = cards.Last();
                result.Add(card);
                cards.RemoveAt(cards.Count-1);
            }

            return result;
        }

        private void SetTimer()
        {
            Timer = new System.Timers.Timer(60000);
            // Hook up the Elapsed event for the timer. 
            Timer.Elapsed += EndTurnOfCurrentPlayer;
            Timer.AutoReset = false;
            Timer.Enabled = true;
        }

        private async void EndTurnOfCurrentPlayer(object sender, ElapsedEventArgs e)
        {
            if (Player1.IdUser == CurrentPlayerIdTurn)
            {
                CurrentPlayerIdTurn = Player2.IdUser;
                await GameConnection.InvokeAsync("SetPlayerTurn", Player2.ConnectionId, Player1.ConnectionId);
            }
            else
            {
                CurrentPlayerIdTurn = Player1.IdUser;
                await GameConnection.InvokeAsync("SetPlayerTurn", Player1.ConnectionId, Player2.ConnectionId);
            }
        }

        public void JoinPlayer(Player player)
        {
            Player2 = player;
            GameConnection.InvokeAsync("PlayerConnectedToGame", player.IdUser, GameId);
        }

    }
}
