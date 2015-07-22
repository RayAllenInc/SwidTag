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

namespace FearTheCowboy.Iso19770 {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Utility;

    public class ElementIndexer<K, T> : IEnumerable<T> where T : BaseElement {
        private Func<XElement, T> _constructor;
        private XElement _element;
        private XName _elementType;
        private XName _index;

        internal ElementIndexer(XName elementType, XElement element, XName index, Func<XElement, T> constructor) {
            _elementType = elementType;
            _element = element;
            _index = index;
            _constructor = constructor;
        }

        public T this[K index]
        {
            get
            {
                return default(T);
            }
        }

        public IEnumerator<T> GetEnumerator() {
            return _element.Elements(_elementType).Select(each => _constructor(each)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public void Remove(K index) {
            var elements = _element.Elements(_elementType).Where(each => each.GetAttribute(_index) == index.ToString()).ToArray();
            foreach (var element in elements) {
                element.Remove();
            }
        }
    }
}