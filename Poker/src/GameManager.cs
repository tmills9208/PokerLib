using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public enum GameState
    {
        Inactive,
        Active
    }
    public enum GameAction
    {
        Start,
        Update,
        Finish
    }

    public class GameStateTransition
    {
        public GameState CurrentState { get; }
        public GameAction Action { get; }

        public GameStateTransition(GameState currentState, GameAction action)
        {
            CurrentState = currentState;
            Action = action;
        }

        public override int GetHashCode()
        {
            return 17 + 31 * CurrentState.GetHashCode() + 31 * Action.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            GameStateTransition other = obj as GameStateTransition;
            return other != null && this.CurrentState == other.CurrentState && this.Action == other.Action;
        }
    }
    public interface IGameStateMachine
    {
        GameState CurrentState { get; set; }
        Dictionary<GameStateTransition, GameState> Transitions { get; }

        public GameState GetNext(GameAction action);
        public GameState MoveNext(GameAction action);
    }
    public class GameStateMachine : IGameStateMachine
    {
        public GameState CurrentState { get; set; }
        public Dictionary<GameStateTransition, GameState> Transitions { get; }

        public GameStateMachine()
        {
            CurrentState = GameState.Inactive;
            Transitions = Utilities.GAME_STATE_TRANSITIONS;
        }

        public GameState GetNext(GameAction action)
        {
            GameStateTransition transition = new GameStateTransition(CurrentState, action);
            GameState nextState = CurrentState;
            if (Transitions.TryGetValue(transition, out nextState))
            {
                throw new StateTransitionException($"Invalid Transition -> {CurrentState} -> {action}");
            }
            return nextState;
        }

        public GameState MoveNext(GameAction action)
        {
            CurrentState = GetNext(action);
            return CurrentState;
        }
    }

    public class GameManager : GameStateMachine
    {
        public StandardCardDeck StandardCardDeck { get; private set; }
        public Dealer Dealer { get; private set; }
        public List<Player> Players { get; private set; }

        public GameManager(Player[] players) : base()
        {
            StandardCardDeck = new StandardCardDeck();
            Dealer = new Dealer();

            Players = players.ToList();
        }

        public void Run()
        {
            while (CurrentState == GameState.Active)
            {
                Update();
            }
        }

        public void Start()
        {
            StandardCardDeck.ShuffleDeck();
            StandardCardDeck.DealCardsToPlayers(Players.ToArray());
            for (int i = 0; i < 3; i++)
            {
                Dealer.DrawCard(StandardCardDeck.CurrentDeck.First());
            }
            MoveNext(GameAction.Start);
            Run();
        }

        public void Update()
        {
            if (Dealer.DealtCards.Count > 5)
            {
                MoveNext(GameAction.Finish);
                return;
            }

            bool playersWaiting = Players.Exists(player => player.CurrentState == PlayerState.Waiting || player.CurrentState == PlayerState.Folded);
            if (!playersWaiting) return;

            Dealer.DrawCard(StandardCardDeck.CurrentDeck.First());
        }

        public void Finish()
        {
            // Get players who havent folded yet.
            // Check who has the highest winning combination.
        }
    }
}
