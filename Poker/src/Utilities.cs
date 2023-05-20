using Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public static class Utilities
    {
        #region Constants
        public static readonly Dictionary<GameStateTransition, GameState> GAME_STATE_TRANSITIONS = new Dictionary<GameStateTransition, GameState>
        {
            // Basically. If the game (or anything) is in the state, and they do the assigned action, the state changes to the next.
            { new GameStateTransition(GameState.Inactive, GameAction.Start), GameState.Active },
            { new GameStateTransition(GameState.Active, GameAction.Finish), GameState.Inactive },
        };
        public static readonly Dictionary<PlayerStateTransition, PlayerState> PLAYER_STATE_TRANSITIONS = new Dictionary<PlayerStateTransition, PlayerState>
        {
            { new PlayerStateTransition(PlayerState.Playing, PlayerAction.Check), PlayerState.Waiting },
            { new PlayerStateTransition(PlayerState.Playing, PlayerAction.Call), PlayerState.Waiting },
            { new PlayerStateTransition(PlayerState.Playing, PlayerAction.Raise), PlayerState.Waiting },
            { new PlayerStateTransition(PlayerState.Playing, PlayerAction.Fold), PlayerState.Folded },
        };
        public static readonly Dictionary<DealerStateTransition, DealerState> DEALER_STATE_TRANSITIONS = new Dictionary<DealerStateTransition, DealerState>
        {
            { new DealerStateTransition(DealerState.Dealing, DealerAction.DealCards), DealerState.Drawing },
            { new DealerStateTransition(DealerState.Drawing, DealerAction.RevealNextCard), DealerState.Waiting },
            { new DealerStateTransition(DealerState.Waiting, DealerAction.CheckGameState), DealerState.Drawing },
        };
        #endregion
    }

    #region Exceptions
    public class StateTransitionException : Exception
    {
        public StateTransitionException() : base()
        {
        }

        public StateTransitionException(string? message) : base(message)
        {
        }

        public StateTransitionException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected StateTransitionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    #endregion

}
