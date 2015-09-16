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
    using System.Globalization;
    using System.Xml;
    using System.Xml.Linq;
    using Schema;

    /// <summary>
    ///     From the schema:
    ///     The element is used to provide results from a scan of a system
    ///     where software that does not have a SWID tag is discovered.
    ///     This information is not provided by the software creator, and
    ///     is instead created when a system is being scanned and the
    ///     evidence for why software is believed to be installed on the
    ///     device is provided in the Evidence element.
    /// </summary>
    public class Evidence : ResourceCollection {
        internal Evidence(XElement element)
            : base(element) {
            if (element.Name != Schema.SoftwareIdentity.Elements.Evidence) {
                throw new ArgumentException("Element is not of type 'Evidence'", "element");
            }
        }

        internal Evidence()
            : base(new XElement(Schema.SoftwareIdentity.Elements.Evidence)) {
        }

        /// <summary>
        ///     Date the evidence was gathered.
        /// </summary>
        public DateTime? Date
        {
            get
            {
                var v = GetAttribute(Schema.SoftwareIdentity.Attributes.Date);
                if (v != null) {
                    try {
                        return XmlConvert.ToDateTime(v, XmlDateTimeSerializationMode.Utc);
                    } catch {
                    }
                }
                return null;
            }
            set
            {
                if (value == null) {
                    return;
                }
                var v = (DateTime)value;

                SetAttribute(Schema.SoftwareIdentity.Attributes.Date, v.ToUniversalTime().ToString("o", CultureInfo.CurrentCulture));
            }
        }

        /// <summary>
        ///     Identifier for the device the evidence was gathered from.
        /// </summary>
        public string DeviceId
        {
            get
            {
                return GetAttribute(Schema.SoftwareIdentity.Attributes.DeviceId);
            }
            set
            {
                SetAttribute(Schema.SoftwareIdentity.Attributes.DeviceId, value);
            }
        }
    }
}