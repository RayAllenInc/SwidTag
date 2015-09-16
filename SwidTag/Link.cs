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
    using System.Xml.Linq;
    using Common.Core;
    using Schema;

    /// <summary>
    ///     From the schema:
    ///     A reference to any another item (can include details that are
    ///     related to the SWID tag such as details on where software
    ///     downloads can be found, vulnerability database associations,
    ///     use rights, etc).
    ///     This is modeled directly to match the HTML [LINK] element; it is
    ///     critical for streamlining software discovery scenarios that
    ///     these are kept consistent.
    /// </summary>
    public class Link : BaseElement {
        internal Link(XElement element)
            : base(element) {
            if (element.Name != Schema.SoftwareIdentity.Elements.Link) {
                throw new ArgumentException("Element is not of type 'Link'", "element");
            }
        }

        internal Link(Uri href, string relationship)
            : base(new XElement(Schema.SoftwareIdentity.Elements.Link)) {
            HRef = href;
            Relationship = relationship;
        }

        /// <summary>
        ///     For installation media (rel="installationmedia") - dictates the
        ///     canonical name for the file.
        ///     Items with the same artifact name should be considered mirrors
        ///     of each other (so download from wherever works).
        /// </summary>
        public string Artifact
        {
            get
            {
                return GetAttribute(Schema.SoftwareIdentity.Attributes.Artifact);
            }
            set
            {
                SetAttribute(Schema.SoftwareIdentity.Attributes.Artifact, value);
            }
        }

        /// <summary>
        ///     From the schema:
        ///     The link to the item being referenced.
        ///     The href can point to several different things, and can be any
        ///     of the following:
        ///     - a RELATIVE URI (no scheme) - which is interpreted depending on
        ///     context (ie, "./folder/supplemental.swidtag" )
        ///     - a physical file location with any system-acceptable
        ///     URI scheme (ie, file:// http:// https:// ftp:// ... etc )
        ///     - an URI with "swid:" as the scheme, which refers to another
        ///     swid by tagId. This URI would need to be resolved in the
        ///     context of the system by software that can lookup other
        ///     swidtags.( ie, "swid:2df9de35-0aff-4a86-ace6-f7dddd1ade4c" )
        ///     - an URI with "swidpath:" as the scheme, which refers to another
        ///     swid by an XPATH query.  This URI would need to be resolved in
        ///     the context of the system by software that can lookup other
        ///     swidtags, and select the appropriate one based on an XPATH
        ///     query. Examples:
        ///     swidpath://SoftwareIdentity[Entity/@regid='http://contoso.com']
        ///     would retrieve all swidtags that had an entity where the
        ///     regid was Contoso
        ///     swidpath://SoftwareIdentity[Meta/@persistentId='b0c55172-38e9-4e36-be86-92206ad8eddb']
        ///     would retrieve swidtags that matched a specific persistentId
        ///     See XPATH query standard : http://www.w3.org/TR/xpath20/
        /// </summary>
        public Uri HRef
        {
            get
            {
                var v = GetAttribute(Schema.SoftwareIdentity.Attributes.HRef);
                Uri result;
                if (v != null && Uri.TryCreate(v, UriKind.Absolute, out result)) {
                    return result;
                }
                return null;
            }
            set
            {
                SetAttribute(Schema.SoftwareIdentity.Attributes.HRef, value.ToString());
            }
        }

        /// <summary>
        ///     An attribute defined by the W3C Media Queries Recommendation
        ///     (see http://www.w3.org/TR/css3-mediaqueries/).
        ///     A hint to the consumer of the link to what the target item is
        ///     applicable for.
        /// </summary>
        public string Media
        {
            get
            {
                return GetAttribute(Schema.SoftwareIdentity.Attributes.Media);
            }
            set
            {
                SetAttribute(Schema.SoftwareIdentity.Attributes.Media, value);
            }
        }

        /// <summary>
        ///     Determines the relative strength of ownership of the target piece of software.
        /// </summary>
        public string Ownership
        {
            get
            {
                return GetAttribute(Schema.SoftwareIdentity.Attributes.Ownership);
            }
            set
            {
                SetAttribute(Schema.SoftwareIdentity.Attributes.Ownership, value);
            }
        }

        /// <summary>
        ///     The relationship between this SWID and the target file.
        /// </summary>
        public string Relationship
        {
            get
            {
                return GetAttribute(Schema.SoftwareIdentity.Attributes.Relationship);
            }
            set
            {
                SetAttribute(Schema.SoftwareIdentity.Attributes.Relationship, value);
            }
        }

        /// <summary>
        ///     The IANA MediaType for the target file; this provides the consumer
        ///     with intelligence of what to expect.
        ///     See http://www.iana.org/assignments/media-types/media-types.xhtml
        ///     for more details on link type.
        /// </summary>
        public string MediaType
        {
            get
            {
                return GetAttribute(Schema.SoftwareIdentity.Attributes.MediaType);
            }
            set
            {
                SetAttribute(Schema.SoftwareIdentity.Attributes.MediaType, value);
            }
        }

        /// <summary>
        ///     Determines if the target software is a hard requirement or not
        /// </summary>
        public string Use
        {
            get
            {
                return GetAttribute(Schema.SoftwareIdentity.Attributes.Use);
            }
            set
            {
                SetAttribute(Schema.SoftwareIdentity.Attributes.Use, value);
            }
        }

        #region  Discovery

        public string MinimumName
        {
            get
            {
                return GetAttribute(Discovery.Attributes.MinimumName);
            }
            set
            {
                SetAttribute(Discovery.Attributes.MinimumName, value);
            }
        }

        public string MaximumName
        {
            get
            {
                return GetAttribute(Discovery.Attributes.MaximumName);
            }
            set
            {
                SetAttribute(Discovery.Attributes.MaximumName, value);
            }
        }

        public string MinimumVersion
        {
            get
            {
                return GetAttribute(Discovery.Attributes.MinimumVersion);
            }
            set
            {
                SetAttribute(Discovery.Attributes.MinimumVersion, value);
            }
        }

        public string MaximumVersion
        {
            get
            {
                return GetAttribute(Discovery.Attributes.MaximumVersion);
            }
            set
            {
                SetAttribute(Discovery.Attributes.MaximumVersion, value);
            }
        }

        public string Keyword
        {
            get
            {
                return GetAttribute(Discovery.Attributes.Keyword);
            }
            set
            {
                SetAttribute(Discovery.Attributes.Keyword, value);
            }
        }

        public string Version
        {
            get
            {
                return GetAttribute(Discovery.Attributes.Version);
            }
            set
            {
                SetAttribute(Discovery.Attributes.Version, value);
            }
        }

        public string Latest
        {
            get
            {
                return GetAttribute(Discovery.Attributes.Latest);
            }
            set
            {
                SetAttribute(Discovery.Attributes.Latest, value);
            }
        }

        public string Type
        {
            get
            {
                return GetAttribute(Discovery.Attributes.Type);
            }
            set
            {
                SetAttribute(Discovery.Attributes.Type, value);
            }
        }

        #endregion


        #region installation
        public string InstallParameters
        {
            get
            {
                return GetAttribute(Installation.Elements.Parameters);
            }
            set
            {
                SetAttribute(Installation.Elements.Parameters, value);
            }
        }

        public string InstallScript
        {
            get
            {
                return GetAttribute(Installation.Elements.Script);
            }
            set
            {
                SetAttribute(Installation.Elements.Script, value);
            }
        }
        #endregion


        public override string ToString() {
            return $"{Relationship}:{HRef}";
        }
    }
}