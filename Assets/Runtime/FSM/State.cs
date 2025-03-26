using UnityEngine;

namespace Game.FSM
{
    public abstract class State
    {
        /// <summary>
        /// The finite state machine that this state belongs to
        /// </summary>
        public readonly StateMachine StateMachine;
        
        protected State(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
        
        /// <summary>
        /// Invoked just after the state becomes the active state in the state machine
        /// </summary>
        public virtual void OnEnter()
        {
            Debug.Log($"Enter state {GetType()}");
        }

        /// <summary>
        /// Invoked just before the state changes to another state in the state machine
        /// </summary>
        public virtual void OnLeaving()
        {
            Debug.Log($"Leaving state {GetType()}");
        }

        /// <summary>
        /// Run every frame that the state is active within the state machine
        /// </summary>
        public virtual void Update()
        {
            
        }
    }
}