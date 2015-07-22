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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using Common.Core;
    using Newtonsoft.Json.Linq;
    using XmlExtensions = Utility.XmlExtensions;

    public class IdentityIndex {
        public static readonly JObject Context;
        private static readonly Dictionary<string, Identity> _viaJsonName = new Dictionary<string, Identity>();
        private static readonly Dictionary<XName, Identity> _viaXname = new Dictionary<XName, Identity>();

        static IdentityIndex() {
            Context = new JObject();
            foreach (var decl in Namespace.Declarations.Where(each => each.Value.IndexOf("xml") != 0)) {
                Context.Add(decl.Value, decl.Key.NamespaceName + "#");
            }

            // elements 
            AddIdentity(Elements.Link, Attributes.HRef);
            AddIdentity(Elements.Directory, Attributes.Name);
            AddIdentity(Elements.File, Attributes.Name);
            AddIdentity(Elements.Entity, Attributes.RegId);
            AddIdentity(Elements.Process, Attributes.Name);
            AddIdentity(Elements.Resource, Attributes.Type);
            AddIdentity(Elements.Meta, null);

            // attributes
            foreach (var field in new[] {typeof (Attributes), typeof (Installation), typeof (Discovery)}.SelectMany(type => type.GetFields(BindingFlags.Static | BindingFlags.Public).Where(each => each.FieldType == typeof (XName)))) {
                try {
                    AddIdentity((XName)field.GetValue(null));
                } catch (Exception e) {
                    e.Dump();
                }
            }

            Context = new JObject(new JProperty("@context", Context));
        }

        internal Identity this[string name]
        {
            get
            {
                if (_viaJsonName.ContainsKey(name)) {
                    return _viaJsonName[name];
                }
                return null;
            }
        }

        internal Identity this[XName name]
        {
            get
            {
                if (_viaXname.ContainsKey(name)) {
                    return _viaXname[name];
                }
                return null;
            }
        }

        private static void AddIdentity(XName name, XName index) {
            var i = new Identity {
                XmlName = name,
                JsonName = name.ToJsonId(),
                Index = index
            };

            Context.Add(name.LocalName, new JObject {
                {"@id", Namespace.Declarations[name.Namespace] + ":" + name.LocalName},
                {"@container", "@index"}
            });

            Context.Add(name.LocalName.ToLowerInvariant(), new JObject {
                {"@id", Namespace.Declarations[name.Namespace] + ":" + name.LocalName},
                {"@container", "@index"}
            });

            if (index != null) {
                if (Context.Property(index.LocalName) == null) {
                    Context.Add(index.LocalName, new JObject {
                        {"@id", Namespace.Declarations[index.Namespace] + ":" + index.LocalName},
                        {"@type", LookupType(index)}
                    });
                }
            }

            if (!_viaJsonName.ContainsKey(i.JsonName)) {
                _viaJsonName.Add(i.JsonName, i);
            }
            if (!_viaXname.ContainsKey(i.XmlName)) {
                _viaXname.Add(i.XmlName, i);
            }
        }

        private static string LookupType(XName name) {
            string type = null;
            if (name.Namespace == Namespace.Swid) {
                var element = Schema.Swidtag.XPathSelectElement(@"//xs:attribute[@name=""{0}""]".format(name.LocalName), Schema.NamespaceManager);
                if (element != null) {
                    type = XmlExtensions.GetAttribute(element, "type");

                    if (type == "xs:anyURI") {
                        type = "@id";
                    }

                    if (!type.StartsWith("xs:")) {
                        type = "swid:" + type;
                    }
                }
            }
            return type ?? "xs:string";
        }

        private static void AddIdentity(XName name) {
            var i = new Identity {
                XmlName = name,
                JsonName = name.ToJsonId(),
                Index = null
            };

            if (Context.Property(name.LocalName) == null) {
                string type = null;
                if (name.Namespace == Namespace.Swid) {
                    var element = Schema.Swidtag.XPathSelectElement(@"//xs:attribute[@name=""{0}""]".format(name.LocalName), Schema.NamespaceManager);
                    if (element != null) {
                        type = XmlExtensions.GetAttribute(element, "type");

                        if (!type.StartsWith("xs:")) {
                            type = "swid:" + type;
                        }
                    }
                }

                Context.Add(name.LocalName, new JObject {
                    {"@id", Namespace.Declarations[name.Namespace] + ":" + name.LocalName},
                    {"@type", type ?? "xs:string"}
                });
            }

            if (!_viaJsonName.ContainsKey(i.JsonName)) {
                _viaJsonName.Add(i.JsonName, i);
            }
            if (!_viaXname.ContainsKey(i.XmlName)) {
                _viaXname.Add(i.XmlName, i);
            }
        }

        internal class Identity {
            internal XName Index;
            internal string JsonName;
            internal XName XmlName;
        }
    }
}