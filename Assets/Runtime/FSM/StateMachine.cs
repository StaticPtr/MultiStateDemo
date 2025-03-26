using System;
using System.Collections.Generic;
using System.Linq;
using InvalidOperationException = System.InvalidOperationException;

namespace Game.FSM
{
    public class StateMachine
    {
        public State? CurrentState { get; private set; }
        public State? LastState { get; private set; }

        protected readonly List<State> States = new();

        /// <summary>
        /// Adds a new state to the state machine, without changing the current state.
        /// </summary>
        /// <param name="state">The state to add.</param>
        public virtual void AddState(State state)
        {
            States.Add(state);
        }
        
        
        public virtual State ChangeState<T>(object? message = null)
            where T : State
        {
            return ChangeState(typeof(T), message);
        }

        public virtual State ChangeState(Type type, object? message = null)
        {
            if (GetStateOfType(type) is not { } targetState)
                throw new InvalidOperationException($"Could not find state of type \"{type.Name}\"");
            
            CurrentState?.OnLeaving();
            LastState = CurrentState;
            CurrentState = targetState;
            CurrentState.OnEnter(message);
            return CurrentState;
        }

        protected virtual State? GetStateOfType(Type type)
        {
            if (!IsTypeForState(type))
                throw new ArgumentException(nameof(type), $"{nameof(type)} must be a {nameof(State)}");

            return States.FirstOrDefault(candidate => type.IsAssignableFrom(candidate.GetType()));
        }

        protected bool IsTypeForState(Type type)
        {
            return typeof(State).IsAssignableFrom(type);
        }
    }
}