using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    #region Dependencies
    public enum DealerState
    {
        Dealing,
        Waiting,
        Drawing,
    }
    public enum DealerAction
    {
        DealCards,
        RevealNextCard,
        CheckGameState,
    }

    public class DealerStateTransition
    {
        DealerState CurrentState { get; }
        DealerAction Action { get; }

        public DealerStateTransition(DealerState currentState, DealerAction action)
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
            DealerStateTransition other = obj as DealerStateTransition;
            return other != null && this.CurrentState == other.CurrentState && this.Action == other.Action;
        }
    }

    public interface IDealerStateMachine
    {
        DealerState CurrentState { get; set; }
        Dictionary<DealerStateTransition, DealerState> Transitions { get; }

        public DealerState GetNext(DealerAction action);
        public DealerState MoveNext(DealerAction action);
    }
    public class DealerStateMachine : IDealerStateMachine
    {
        public DealerState CurrentState { get; set; }
        public Dictionary<DealerStateTransition, DealerState> Transitions { get; }

        public DealerStateMachine(DealerState currentState)
        {
            CurrentState = currentState;
            Transitions = Utilities.DEALER_STATE_TRANSITIONS;
        }

        public DealerState GetNext(DealerAction action)
        {
            DealerStateTransition transition = new DealerStateTransition(CurrentState, action);
            DealerState nextState = CurrentState;
            if (Transitions.TryGetValue(transition, out nextState))
            {
                throw new StateTransitionException($"Invalid Transition -> {CurrentState} -> {action}");
            }
            return nextState;
        }

        public DealerState MoveNext(DealerAction action)
        {
            CurrentState = GetNext(action);
            return CurrentState;
        }
    }
    #endregion

    public class Dealer : DealerStateMachine
    {
        public List<StandardCard> DealtCards { get; private set; }

        public Dealer(DealerState _currentState = DealerState.Dealing) : base(_currentState)
        {
            DealtCards = new List<StandardCard>();
        }

        public void DrawCard(StandardCard card)
        {
            if (DealtCards.Count > 5) return;
            DealtCards.Add(card);
        }

        public void RemoveCards()
        {
            DealtCards.Clear();
        }
    }
}
