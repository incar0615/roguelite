using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    public abstract class EventHandler : MonoBehaviour, IEventHandler
    {
        /// <summary>
        /// Subscribe to events
        ///
        /// @example
        ///   EventManager.Instance.AddListener<MoveResolvedEvent>(OnMoveResolved);
        /// </summary>
        public abstract void SubscribeEvents();

        /// <summary>
        /// Unsubscribe from events
        ///
        /// @example
        ///   EventManager.Instance.RemoveListener<MoveResolvedEvent>(OnMoveResolved);
        /// </summary>
        public abstract void UnsubscribeEvents();

        protected virtual void OnEnable()
        {
            SubscribeEvents();
        }

        protected virtual void OnDisable()
        {
            UnsubscribeEvents();
        }
    }

}
