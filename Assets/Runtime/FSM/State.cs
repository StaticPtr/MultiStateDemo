using UnityEngine;

namespace Game.FSM
{
    public abstract class State : ScriptableObject
    {
        /// <summary>
        /// The finite state machine that this state belongs to
        /// </summary>
        public StateMachine? StateMachine { get; internal set; }
        
        /// <summary>
        /// Invoked just after the state becomes the active state in the state machine
        /// </summary>
        public virtual void OnEnter(object? message)
        {
        }

        /// <summary>
        /// Invoked just before the state changes to another state in the state machine
        /// </summary>
        public virtual void OnLeaving()
        {
        }

        /// <summary>
        /// Run every frame that the state is active within the state machine
        /// </summary>
        public virtual void OnUpdate(float deltaTimeSeconds)
        {
        }
    }
}