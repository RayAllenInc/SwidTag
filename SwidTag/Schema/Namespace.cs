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

namespace FearTheCowboy.Iso19770.Schema {
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Xml.Linq;

    public static class Namespace {
        public static readonly XNamespace XmlNs = XNamespace.Get("http://www.w3.org/2000/xmlns/");
        public static readonly XNamespace XmlDsig = XNamespace.Get("http://www.w3.org/2000/09/xmldsig");
        public static readonly XNamespace Xml = XNamespace.Get("http://www.w3.org/XML/1998/namespace");
        public static readonly XNamespace Xs = XNamespace.Get("http://www.w3.org/2001/XMLSchema");
        public static readonly XNamespace Swid = XNamespace.Get("http://standards.iso.org/iso/19770/-2/2015/schema.xsd");
        // public static readonly XNamespace SwidtagCurrent = XNamespace.Get("http://standards.iso.org/iso/19770/-2/2015-current/schema.xsd");

        public static readonly XNamespace Discovery = XNamespace.Get("http://packagemanagement.org/discovery");
        public static readonly XNamespace Installation = XNamespace.Get("http://packagemanagement.org/installation");

        internal static readonly Dictionary<XNamespace, string> Declarations =
            new Dictionary<XNamespace, string>(typeof (Namespace).GetFields(BindingFlags.Static | BindingFlags.Public)
                .Where(each => each.FieldType == typeof (XNamespace))
                .ToDictionary(each => (XNamespace)each.GetValue(null), each => each.Name.ToLowerInvariant()));
    }
}