using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    #region Dependencies
    public enum PlayerState
    {
        Folded,
        Waiting,
        Playing,
    }
    public enum PlayerAction
    {
        Call,
        Check,
        Raise,
        Fold,
    }

    public class PlayerStateTransition
    {
        PlayerState CurrentState { get; }
        PlayerAction Action { get; }

        public PlayerStateTransition(PlayerState currentState, PlayerAction action)
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
            PlayerStateTransition other = obj as PlayerStateTransition;
            return other != null && this.CurrentState == other.CurrentState && this.Action == other.Action;
        }
    }

    public interface IPlayerStateMachine
    {
        PlayerState CurrentState { get; set; }
        Dictionary<PlayerStateTransition, PlayerState> Transitions { get; }

        public PlayerState GetNext(PlayerAction action);
        public PlayerState MoveNext(PlayerAction action);
    }
    public class PlayerStateMachine : IPlayerStateMachine
    {
        public PlayerState CurrentState { get; set; }
        public Dictionary<PlayerStateTransition, PlayerState> Transitions { get; }

        public PlayerStateMachine(PlayerState currentState)
        {
            CurrentState = currentState;

            Transitions = Utilities.PLAYER_STATE_TRANSITIONS;
        }

        public PlayerState GetNext(PlayerAction action)
        {
            PlayerStateTransition transition = new PlayerStateTransition(CurrentState, action);
            PlayerState nextState = CurrentState;
            if (Transitions.TryGetValue(transition, out nextState))
            {
                throw new StateTransitionException($"Invalid Transition -> {CurrentState} -> {action}");
            }
            return nextState;
        }

        public PlayerState MoveNext(PlayerAction action)
        {
            CurrentState = GetNext(action);
            return CurrentState;
        }
    }
    #endregion

    public class Player : PlayerStateMachine
    {
        public List<StandardCard> HeldCards { get; set; }

        public string Name { get; private set; }
        public int Score { get; private set; }

        public Player(string _name, int _score, PlayerState _currentState = PlayerState.Waiting) : base(_currentState)
        {
            Name = _name;
            Score = _score;

            HeldCards = new List<StandardCard>();
        }

        public void GiveCard(StandardCard card)
        {
            if (HeldCards.Count > 2) return;
            HeldCards.Add(card);
        }

        public void RemoveCards()
        {
            HeldCards.Clear();
        }
    }
}
