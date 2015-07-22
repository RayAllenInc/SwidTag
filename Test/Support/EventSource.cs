// 
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//  http://www.apache.org/licenses/LICENSE-2.0
//  
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//  

namespace FearTheCowboy.Iso19770.Test.Support {
    using System;

    public class EventSource {
        public static EventSource Instance = new EventSource();

        protected internal EventSource() {
        }

        /// <summary>
        ///     Adds an event handler delegate to the current tasktask
        /// </summary>
        /// <param name="eventSource"> </param>
        /// <param name="eventHandlerDelegate"> </param>
        /// <returns> </returns>
        public static EventSource operator +(EventSource eventSource, Delegate eventHandlerDelegate) {
            XTask.CurrentExecutingTask.AddEventHandler(eventHandlerDelegate);
            return eventSource;
        }

        public static EventSource Add(EventSource eventSource, Delegate eventHandlerDelegate) {
            XTask.CurrentExecutingTask.AddEventHandler(eventHandlerDelegate);
            return eventSource;
        }

        public static EventSource operator -(EventSource eventSource, Delegate eventHandlerDelegate) {
            XTask.CurrentExecutingTask.RemoveEventHandler(eventHandlerDelegate);
            return eventSource;
        }

        public static EventSource Subtract(EventSource eventSource, Delegate eventHandlerDelegate) {
            XTask.CurrentExecutingTask.RemoveEventHandler(eventHandlerDelegate);
            return eventSource;
        }
    }
}