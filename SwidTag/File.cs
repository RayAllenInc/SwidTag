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
    using Schema;

    /// <summary>
    ///     Represents an individual file
    /// </summary>
    public class File : FilesystemItem {
        internal File(XElement element)
            : base(element) {
            if (element.Name != Schema.SoftwareIdentity.Elements.File) {
                throw new ArgumentException("Element is not of type 'File'", "element");
            }
        }

        internal File(string filename)
            : base(new XElement(Schema.SoftwareIdentity.Elements.File)) {
            Name = filename;
        }

        /// <summary>
        ///     From the swidtag schema:
        ///     The file size in bytes of the file
        /// </summary>
        public long? Size
        {
            get
            {
                var sz = GetAttribute(Schema.SoftwareIdentity.Attributes.Size);
                if (sz != null) {
                    long result;
                    if (Int64.TryParse(sz, out result)) {
                        return result;
                    }
                }
                return null;
            }
            set
            {
                if (value != null) {
                    SetAttribute(Schema.SoftwareIdentity.Attributes.Size, value.ToString());
                }
            }
        }

        /// <summary>
        ///     From the swidtag schema:
        ///     The file version
        /// </summary>
        public string Version
        {
            get
            {
                return GetAttribute(Schema.SoftwareIdentity.Attributes.Version);
            }
            set
            {
                SetAttribute(Schema.SoftwareIdentity.Attributes.Version, value);
            }
        }
    }
}