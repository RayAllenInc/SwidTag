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
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;
    using Common.Collections;
    using JsonLD.Core;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Schema;
    using Sgml;
    using Utility;
    using Formatting = Newtonsoft.Json.Formatting;
    using StringExtensions = Common.Core.StringExtensions;

    public class SoftwareIdentity : BaseElement {
        private static readonly JsonLdOptions _options = new JsonLdOptions() {
            useNamespaces = true,
            documentLoader = new ContextDownloader(),
        };

        private readonly XDocument _document;

        public SoftwareIdentity(XDocument document)
            : base(document.Root) {
            _document = document;
            _document.Declaration = Xml.Declaration;
        }

        public SoftwareIdentity()
            : this(new XDocument(Xml.Declaration, new XElement(Elements.SoftwareIdentity))) {
        }

        public SoftwareIdentity(XElement xmlDocument)
            : this(new XDocument(Xml.Declaration, xmlDocument)) {
        }

        public string SwidTagJson
        {
            get
            {
                JObject result = SetStandardContext(new JObject());
                var element = _document.Root;
                if (element != null) {
                    foreach (var attr in element.Attributes().Where(attr => !attr.IsNamespaceDeclaration)) {
                        result.Add(attr.Name.ToJsonId(element.Name.Namespace), attr.Value);
                    }

                    foreach (var elementGroup in element.Elements().GroupBy(each => each.Name)) {
                        var identity = IdentityIndex[elementGroup.Key];

                        if (identity != null) {
                            // we know what type this is

                            if (identity.Index != null) {
                                // we know what attribute we want to use as an index.
                                OutputKeyedArray(elementGroup, identity, result);
                            } else {
                                // OutputArray(elementGroup, identity, result);
                            }
                        } else {
                            //no idea what this is at this point.
                        }
                    }

                    if (Meta.Any()) {
                        var meta = new JObject();
                        foreach (var attr in Meta.SelectMany(m => m.Element.Attributes())) {
                            meta.Add(attr.Name.ToJsonId(element.Name.Namespace), attr.Value);
                        }
                        result.Add(Elements.Meta.ToJsonId(), meta);
                    }
                    // return result.ToString();
                    var doc = SetStandardContext(Compact(result)).ToString(Formatting.Indented);
                    return doc.Replace(@"""swid:", @"""");
                }

                return null;
            }
        }

        public string SwidTagXml
        {
            get
            {
                var stringBuilder = new StringBuilder();

                var settings = new XmlWriterSettings {
                    OmitXmlDeclaration = false,
                    Indent = true,
                    NewLineOnAttributes = true,
                    NamespaceHandling = NamespaceHandling.OmitDuplicates
                    ,
                    Encoding = Encoding.Unicode
                    ,
                    ConformanceLevel = ConformanceLevel.Document
                };

                using (var xmlWriter = XmlWriter.Create(stringBuilder, settings)) {
                    _document.Save(xmlWriter);
                }

                return stringBuilder.ToString();
            }
        }

        private static void OutputKeyedArray(IGrouping<XName, XElement> elementGroup, IdentityIndex.Identity identity, JObject result) {
            var container = new JObject();
            foreach (var eachElement in elementGroup) {
                var item = new JObject();
                foreach (var attr in eachElement.AllAttributes().Where(attr => attr.Name != identity.Index && !attr.IsNamespaceDeclaration)) {
                    item.Add(IdentityIndex[attr.Name].JsonName, attr.Value);
                }
                var indexValue = eachElement.GetAttribute(identity.Index);

                if (indexValue != null) {
                    container.Add(indexValue, item);
                }
            }
            result.Add(identity.JsonName, container);
        }

        private static void OutputArray(IGrouping<XName, XElement> elementGroup, IdentityIndex.Identity identity, JObject result) {
            int n = 0;
            var container = new JObject();
            foreach (var eachElement in elementGroup) {
                var item = new JObject();
                foreach (var attr in eachElement.AllAttributes().Where(attr => !attr.IsNamespaceDeclaration)) {
                    if (IdentityIndex[attr.Name] != null) {
                        item.Add(IdentityIndex[attr.Name].JsonName, attr.Value);
                    } else {
                        item.Add(attr.Name.LocalName, attr.Value);
                    }
                }
                container.Add((n++).ToString(), item);
            }
            result.Add(identity.JsonName, container);
        }

        public static SoftwareIdentity Load(TextReader reader) {
            return null;
        }

        public static SoftwareIdentity LoadXml(string swidTagXml) {
            try {
                using (var reader = XmlReader.Create(new StringReader(swidTagXml), new XmlReaderSettings())) {
                    var document = XDocument.Load(reader);
                    if (IsSwidtag(document.Root)) {
                        return new SoftwareIdentity(document);
                    }
                }
            } catch (Exception e) {
                e.Dump();
            }
            return null;
        }

        public static JObject Normalize(string swidTagText) {
            return Expand(JToken.ReadFrom(new JsonTextReader(new StringReader(swidTagText))));
        }

        public static JObject SetStandardContext(JObject obj) {
            obj.Remove("@context");
            obj.Add("@context", JToken.FromObject("http://packagemanagement.org/discovery"));
            return obj;
        }

        public static JObject Compact(JToken swidTag) {
            // first, compact it to the canonical context
            return JsonLdProcessor.Compact(swidTag, IdentityIndex.Context, _options);
        }

        public static JObject Expand(JToken swidTag) {
            // then, expand it out so we can walk it with strong types
            var expanded = JsonLdProcessor.Expand(Compact(swidTag), _options).FirstOrDefault().ToJObject();

            return SetStandardContext(expanded);
        }

        public static SoftwareIdentity LoadJson(string swidTagJson) {
            try {
                var swidTag = new SoftwareIdentity();
                Meta meta = null;

                var expanded = Normalize(swidTagJson);

                foreach (var member in expanded) {
                    var memberName = member.Key;
                    if (member.Value.Type == JTokenType.Array) {
                        foreach (var element in member.Value.Cast<JObject>()) {
                            var index = element.Index();
                            var value = element.Val();

                            if (index != null) {
                                if (value != null) {
                                    if (memberName == JSonMembers.Meta) {
                                        meta = meta ?? swidTag.AddMeta();
                                        meta.SetAttribute(index, value);
                                    }
                                } else {
                                    var identity = IdentityIndex[memberName];

                                    if (memberName == identity.JsonName) {
                                        try {
                                            // create the new element
                                            var e = new XElement(identity.XmlName);
                                            swidTag.Element.Add(e);

                                            foreach (var property in element.Properties().Where(each => each.Name != "@index")) {
                                                e.SetAttribute(IdentityIndex[property.Name].XmlName, property.PropertyValue());
                                            }

                                            // set the index of the element
                                            if (identity.Index != null) {
                                                e.SetAttribute(identity.Index, index);
                                            }
                                        } catch (Exception e) {
                                            e.Dump();
                                        }
                                    }
                                }
                            } else {
                                swidTag.SetAttribute(memberName.ToXName(), value);
                            }
                        }
                        continue;
                    }
                    // Console.WriteLine("'{0}' -- '{1}'", memberName, member.Value.Type);
                }
                return swidTag;
            } catch (Exception e) {
                e.Dump();
                return null;
            }
        }

        public static SoftwareIdentity LoadHtml(string swidTagHtml) {
            try {
                using (var reader = new SgmlReader {
                    DocType = "HTML",
                    WhitespaceHandling = WhitespaceHandling.All,
                    StripDocType = true,
                    InputStream = new StringReader(swidTagHtml),
                    CaseFolding = CaseFolding.ToLower
                }) {
                    var document = XDocument.Load(reader);

                    if (document.Root != null && document.Root.Name.LocalName == "html") {
                        var swidTag = new SoftwareIdentity {
                            Name = "Anonymous",
                            Version = "1.0",
                            VersionScheme = Schema.VersionScheme.MultipartNumeric
                        };

                        var html = document.Root;
                        var ns = html.Name.Namespace;

                        var head = html.Element(ns + "head");
                        if (head != null) {
                            var links = head.Elements(ns + "link");

                            foreach (var link in links) {
                                var href = link.Attribute("href");
                                var rel = link.Attribute("rel");

                                if (href != null && rel != null) {
                                    var l = swidTag.AddLink(new Uri(href.Value), rel.Value);
                                    foreach (var attr in link.Attributes()) {
                                        l.SetAttribute(attr.Name, attr.Value);
                                    }
                                }
                            }
                        }
                        return swidTag;
                    }
                }
            } catch (Exception e) {
                e.Dump();
            }
            return null;
        }

        public static bool IsSwidtag(XElement xmlDocument) {
            return xmlDocument.Name == Elements.SoftwareIdentity;
        }

        public bool IsApplicable(Hashtable environment) {
            return MediaQuery.IsApplicable(AppliesToMedia, environment);
        }

        #region Attributes

        public bool? IsCorpus
        {
            get
            {
                return StringExtensions.IsTruePreserveNull(GetAttribute(Schema.Attributes.Corpus));
            }
            set
            {
                if (value != null) {
                    SetAttribute(Schema.Attributes.Corpus, value.ToString());
                }
            }
        }

        public string Name
        {
            get
            {
                return GetAttribute(Schema.Attributes.Name);
            }
            set
            {
                SetAttribute(Schema.Attributes.Name, value);
            }
        }

        public string Version
        {
            get
            {
                return GetAttribute(Schema.Attributes.Version);
            }
            set
            {
                SetAttribute(Schema.Attributes.Version, value);
            }
        }

        public string VersionScheme
        {
            get
            {
                return GetAttribute(Schema.Attributes.VersionScheme);
            }
            set
            {
                SetAttribute(Schema.Attributes.VersionScheme, value);
            }
        }

        public string TagVersion
        {
            get
            {
                return GetAttribute(Schema.Attributes.TagVersion);
            }
            set
            {
                SetAttribute(Schema.Attributes.TagVersion, value);
            }
        }

        public string TagId
        {
            get
            {
                // was called 'UniqueId' until very late in the ISO process. Check there for the value if it's not got a tagid.
                return GetAttribute(Schema.Attributes.TagId) ?? GetAttribute(Schema.Attributes.UniqueId);
            }
            set
            {
                SetAttribute(Schema.Attributes.TagId, value);
            }
        }

        public bool? IsPatch
        {
            get
            {
                return StringExtensions.IsTruePreserveNull(GetAttribute(Schema.Attributes.Patch));
            }
            set
            {
                if (value != null) {
                    SetAttribute(Schema.Attributes.Patch, value.ToString());
                }
            }
        }

        public bool? IsSupplemental
        {
            get
            {
                return StringExtensions.IsTruePreserveNull(GetAttribute(Schema.Attributes.Supplemental));
            }
            set
            {
                if (value != null) {
                    SetAttribute(Schema.Attributes.Supplemental, value.ToString());
                }
            }
        }

        public string AppliesToMedia
        {
            get
            {
                return GetAttribute(Schema.Attributes.Media);
            }
            set
            {
                SetAttribute(Schema.Attributes.Media, value);
            }
        }

        #endregion

        #region Elements

        public IEnumerable<SoftwareMetadata> Meta
        {
            get
            {
                return Element.Elements(Elements.Meta).Select(each => new SoftwareMetadata(each)).ReEnumerable();
            }
        }

        public SoftwareMetadata AddMeta() {
            return AddElement(new SoftwareMetadata());
        }

        public IEnumerable<Link> Links
        {
            get
            {
                return Element.Elements(Elements.Link).Select(each => new Link(each)).ReEnumerable();
            }
        }

        private ElementIndexer<Uri, Link> _links;

        public ElementIndexer<Uri, Link> Links2
        {
            get
            {
                return _links ?? (_links = new ElementIndexer<Uri, Link>(Elements.Link, Element, Schema.Attributes.HRef, element => new Link(element)));
            }
        }

        public void RemoveLink(Uri referenceUri) {
            foreach (var element in Element.Elements(Elements.Link).ToArray()) {
                var href = element.GetAttribute(Schema.Attributes.HRef);
                if (href != null && href == referenceUri.AbsoluteUri) {
                    element.Remove();
                }
            }
        }

        public Link AddLink(Uri referenceUri, string relationship) {
            return AddElement(new Link(referenceUri, relationship));
        }

        public IEnumerable<Entity> Entities
        {
            get
            {
                return Element.Elements(Elements.Entity).Select(each => new Entity(each)).ReEnumerable();
            }
        }

        public Entity AddEntity(string name, string regId, string role) {
            return AddElement(new Entity(name, regId, role));
        }

        /// <summary>
        ///     A ResourceCollection for the 'Payload' of the SwidTag.
        ///     This value is null if the swidtag does not contain a Payload element.
        ///     from swidtag XSD:
        ///     The items that may be installed on a device when the software is
        ///     installed.  Note that Payload may be a superset of the items
        ///     installed and, depending on optimization systems for a device,
        ///     may or may not include every item that could be created or
        ///     executed on a device when software is installed.
        ///     In general, payload will be used to indicate the files that
        ///     may be installed with a software product and will often be a
        ///     superset of those files (i.e. if a particular optional
        ///     component is not installed, the files associated with that
        ///     component may be included in payload, but not installed on
        ///     the device).
        /// </summary>
        public Payload Payload
        {
            get
            {
                return Element.Elements(Elements.Payload).Select(each => new Payload(each)).FirstOrDefault();
            }
        }

        /// <summary>
        ///     Adds a Payload resource collection element.
        /// </summary>
        /// <returns>The ResourceCollection added. If the Payload already exists, returns the current Payload.</returns>
        public Payload AddPayload() {
            // should we just detect and add the evidence element when a provider is adding items to the evidence
            // instead of requiring someone to explicity add the element?
            if (Element.Elements(Elements.Payload).Any()) {
                return Payload;
            }
            return AddElement(new Payload());
        }

        /// <summary>
        ///     An Evidence element representing the discovered for a swidtag.
        ///     This value is null if the swidtag does not contain an Evidence element
        ///     from swidtag XSD:
        ///     The element is used to provide results from a scan of a system
        ///     where software that does not have a SWID tag is discovered.
        ///     This information is not provided by the software creator, and
        ///     is instead created when a system is being scanned and the
        ///     evidence for why software is believed to be installed on the
        ///     device is provided in the Evidence element.
        /// </summary>
        public Evidence Evidence
        {
            get
            {
                return Element.Elements(Elements.Evidence).Select(each => new Evidence(each)).FirstOrDefault();
            }
        }

        /// <summary>
        ///     Adds an Evidence element.
        /// </summary>
        /// <returns>The added Evidence element. If the Evidence element already exists, returns the current element.</returns>
        public Evidence AddEvidence() {
            // should we just detect and add the evidence element when a provider is adding items to the evidence
            // instead of requiring someone to explicity add the element?
            if (Element.Elements(Elements.Evidence).Any()) {
                return Evidence;
            }
            return AddElement(new Evidence());
        }

        #endregion
    }
}