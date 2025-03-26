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

        /// <summary>
        /// Convenience method for adding a new state to the state machine.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if <see cref="T"/> is not a <see cref="State"/>.</exception>
        public void AddState<T>()
            where T : State
        {
            AddState(typeof(T));
        }

        /// <summary>
        /// Convenience method for adding a new state to the state machine.
        /// </summary>
        /// <param name="type">The type of the state to instantiate.</param>
        /// <exception cref="ArgumentException">Thrown if <see cref="type"/> is not a <see cref="State"/>.</exception>
        public void AddState(Type type)
        {
            if (!IsTypeForState(type))
                throw new ArgumentException(nameof(type), $"{nameof(type)} must be a {nameof(State)}");

            State newState = (State)Activator.CreateInstance(type, this);
            AddState(newState);
        }
        
        public virtual void ChangeState<T>()
            where T : State
        {
            ChangeState(typeof(T));
        }

        public virtual void ChangeState(Type type)
        {
            if (GetStateOfType(type) is not { } targetState)
                throw new InvalidOperationException($"Could not find state of type \"{type.Name}\"");
            
            CurrentState?.OnLeaving();
            LastState = CurrentState;
            CurrentState = targetState;
            CurrentState.OnEnter();
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