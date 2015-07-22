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
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class LocalEventSource : EventSource, IDisposable {
        protected internal List<Delegate> Delegates = new List<Delegate>();

        protected internal LocalEventSource() {
        }

        public LocalEventSource Events
        {
            get
            {
                return this;
            }
            set
            {
                return;
            }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (Delegates != null) {
                foreach (var i in Delegates) {
                    XTask.CurrentExecutingTask.RemoveEventHandler(i);
                }

                Delegates = null;

                // encourage a bit of cleanup
                // todo: uh, what in the world is this?
                Task.Factory.StartNew(XTask.Collect);
            }
        }

        public static LocalEventSource operator +(LocalEventSource eventSource, Delegate eventHandlerDelegate) {
            XTask.CurrentExecutingTask.AddEventHandler(eventHandlerDelegate);
            eventSource.Delegates.Add(eventHandlerDelegate);
            return eventSource;
        }

        public static LocalEventSource Add(LocalEventSource eventSource, Delegate eventHandlerDelegate) {
            XTask.CurrentExecutingTask.AddEventHandler(eventHandlerDelegate);
            eventSource.Delegates.Add(eventHandlerDelegate);
            return eventSource;
        }

        public static LocalEventSource operator -(LocalEventSource eventSource, Delegate eventHandlerDelegate) {
            XTask.CurrentExecutingTask.RemoveEventHandler(eventHandlerDelegate);
            eventSource.Delegates.Remove(eventHandlerDelegate);
            return eventSource;
        }

        public static LocalEventSource Subtract(LocalEventSource eventSource, Delegate eventHandlerDelegate) {
            XTask.CurrentExecutingTask.RemoveEventHandler(eventHandlerDelegate);
            eventSource.Delegates.Remove(eventHandlerDelegate);
            return eventSource;
        }

        ~LocalEventSource() {
            Dispose(false);
        }
    }
}