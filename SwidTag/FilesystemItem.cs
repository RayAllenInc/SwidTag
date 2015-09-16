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
    using System.Xml.Linq;
    using Common.Core;
    using Schema;

    /// <summary>
    ///     Represents an individual file or directory.
    /// </summary>
    public class FilesystemItem : Meta {
        internal FilesystemItem(XElement element)
            : base(element) {
        }

        /// <summary>
        ///     From the swidtag schema:
        ///     Files that are considered important or required for the use of
        ///     a software component.  Typical key files would be those which,
        ///     if not available on a system, would cause the software not to
        ///     execute.
        ///     Key files will typically be used to validate that software
        ///     referenced by the SWID tag is actually installed on a specific
        ///     computing device
        /// </summary>
        public bool? IsKey
        {
            get
            {
                return GetAttribute(Schema.SoftwareIdentity.Attributes.Key).IsTruePreserveNull();
            }
            set
            {
                if (value != null) {
                    SetAttribute(Schema.SoftwareIdentity.Attributes.Key, value.ToString());
                }
            }
        }

        /// <summary>
        ///     From the swidtag schema:
        ///     The directory or location where a file was found or can expected
        ///     to be located.  does not include the filename itself.  This can
        ///     be relative path from the 'root' attribute.
        /// </summary>
        public string Location
        {
            get
            {
                return GetAttribute(Schema.SoftwareIdentity.Attributes.Location);
            }
            set
            {
                SetAttribute(Schema.SoftwareIdentity.Attributes.Location, value);
            }
        }

        /// <summary>
        ///     From the swidtag schema:
        ///     The filename without any path characters
        /// </summary>
        public string Name
        {
            get
            {
                return GetAttribute(Schema.SoftwareIdentity.Attributes.Name);
            }
            set
            {
                SetAttribute(Schema.SoftwareIdentity.Attributes.Name, value);
            }
        }

        /// <summary>
        ///     From the swidtag schema:
        ///     A system-specific root folder that the 'location'
        ///     attribute is an offset from. If this is not specified
        ///     the assumption is the 'root' is the same folder as
        ///     the location of the SWIDTAG.
        /// </summary>
        public string Root
        {
            get
            {
                return GetAttribute(Schema.SoftwareIdentity.Attributes.Root);
            }
            set
            {
                SetAttribute(Schema.SoftwareIdentity.Attributes.Root, value);
            }
        }
    }
}