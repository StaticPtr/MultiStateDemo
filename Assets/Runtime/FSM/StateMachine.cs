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

        public event Action<StateMachine>? OnStateChanged;

        protected readonly List<State> States = new();

        /// <summary>
        /// Adds a new state to the state machine, without changing the current state.
        /// </summary>
        /// <param name="state">The state to add.</param>
        public virtual void AddState(State state)
        {
            if (state.StateMachine is not null)
                throw new InvalidOperationException("State already belongs to a state machine");
            
            state.StateMachine = this;
            States.Add(state);
        }
        
        
        public State ChangeState<T>(object? message = null)
            where T : State
        {
            return ChangeState(typeof(T), message);
        }

        public State ChangeState(Type type, object? message = null)
        {
            if (GetStateOfType(type) is not { } targetState)
                throw new InvalidOperationException($"Could not find state of type \"{type.Name}\"");

            ChangeState(targetState, message);
            return targetState;
        }

        public virtual void ChangeState(State state, object? message = null)
        {
            if (!States.Contains(state))
                throw new ArgumentException("State is not in state machine", nameof(state));
            
            CurrentState?.OnLeaving();
            
            LastState = CurrentState;
            CurrentState = state;
            CurrentState.OnEnter(message);
            
            OnStateChanged?.Invoke(this);
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