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

namespace FearTheCowboy.Iso19770.Utility {
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Schema;

    internal static class XmlExtensions {
        private static int _counter = 0;

        /// <summary>
        ///     Gets the attribute value for a given element.
        /// </summary>
        /// <param name="element">the element that possesses the attribute</param>
        /// <param name="attribute">the attribute to find</param>
        /// <returns>the string value of the element. Returns null if the element or attribute does not exist.</returns>
        internal static string GetAttribute(this XElement element, XName attribute) {
            if (element == null || attribute == null || string.IsNullOrWhiteSpace(attribute.ToString())) {
                return null;
            }

            var a = element.Attribute(attribute);
            if (a == null) {
                if (attribute.Namespace == element.Name.Namespace) {
                    a = element.Attribute(attribute.LocalName);
                }
            }
            return a == null ? null : a.Value;
        }

        /// <summary>
        ///     Adds a new attribute to the element
        ///     Does not permit modification of an existing attribute.
        ///     Does not add empty or null attributes or values.
        /// </summary>
        /// <param name="element">The element to add the attribute to</param>
        /// <param name="attribute">The attribute to add</param>
        /// <param name="value">the value of the attribute to add</param>
        /// <returns>The element passed in. (Permits fluent usage)</returns>
        internal static XElement SetAttribute(this XElement element, XName attribute, string value) {
            if (element == null) {
                return null;
            }

            // we quietly ignore attempts to add empty data or attributes.
            if (attribute == null || string.IsNullOrWhiteSpace(attribute.ToString())) {
                return element;
            }

            if (element.Name.Namespace == attribute.Namespace || string.IsNullOrWhiteSpace(attribute.NamespaceName) || attribute.Namespace == Namespace.XmlNs || attribute.Namespace == Namespace.Xml) {
                element.SetAttributeValue(attribute.LocalName, value);
            } else {
                element.EnsureNamespaceAtTop(attribute.Namespace);
                element.SetAttributeValue(attribute, value);
            }

            return element;
        }

        internal static IEnumerable<XAttribute> AllAttributes(this XElement element) {
            return element.Attributes().Select(each => {
                if (string.IsNullOrWhiteSpace(each.Name.NamespaceName)) {
                    return new XAttribute(element.Name.Namespace + each.Name.LocalName, each.Value);
                }
                return each;
            });
        }

        internal static void EnsureNamespaceAtTop(this XElement element, XNamespace ns) {
            if (ns == null || string.IsNullOrEmpty(ns.NamespaceName)) {
                return;
            }

            while (true) {
                // look for a namespace declaration for this namespace on each element going up.
                if (element.Attributes().Any(each => each.IsNamespaceDeclaration && each.Value == ns.NamespaceName)) {
                    return;
                }

                // if we haven't reached the top, keep going.
                if (element.Parent != null) {
                    element = element.Parent;
                    continue;
                }

                // ok, we're at the top
                if (Namespace.Declarations.ContainsKey(ns)) {
                    // is this namespace one of the commonly known ones?
                    element.SetAttributeValue(Namespace.XmlNs + Namespace.Declarations[ns], ns.NamespaceName);
                    break;
                }

                // nope. give it an arbitrary id.
                element.SetAttributeValue(Namespace.XmlNs + ("pp" + (++_counter)), ns.NamespaceName);
                break;
            }
        }
    }
}