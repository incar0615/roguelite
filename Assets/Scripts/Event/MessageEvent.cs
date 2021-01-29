using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    /// <summary>
    /// Base event for all EventManager events.
    /// </summary>
    public class MessageEvent
    {
        /// <summary>
        /// Events with Handled==false are requests for action.  If Handled==true
        /// then the event is an annoucement that an action occurred.
        /// </summary>
        public bool handled { get; set; }

        /// <summary>
        /// Sender handler
        /// </summary>
        public EventHandler handler { get; set; }

        /// <summary>
        /// Return a string
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0}, Handled {1}", base.ToString(), handled);
        }
    }

}