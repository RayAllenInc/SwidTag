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

    public class SoftwareMetadata : Meta {
        internal SoftwareMetadata(XElement element) : base(element) {
            if (element.Name != Schema.SoftwareIdentity.Elements.Meta) {
                throw new ArgumentException("Element is not of type 'SoftwareMetadata'", "element");
            }
        }

        internal SoftwareMetadata()
            : base(new XElement(Schema.SoftwareIdentity.Elements.Meta)) {
        }

        public string ActivationStatus
        {
            get
            {
                return GetAttribute(Schema.SoftwareIdentity.Attributes.ActivationStatus);
            }
            set
            {
                SetAttribute(Schema.SoftwareIdentity.Attributes.ActivationStatus, value);
            }
        }

        public string ChannelType
        {
            get
            {
                return GetAttribute(Schema.SoftwareIdentity.Attributes.ChannelType);
            }
            set
            {
                SetAttribute(Schema.SoftwareIdentity.Attributes.ChannelType, value);
            }
        }

        public string Description
        {
            get
            {
                return GetAttribute(Schema.SoftwareIdentity.Attributes.Description);
            }
            set
            {
                SetAttribute(Schema.SoftwareIdentity.Attributes.Description, value);
            }
        }

        public string ColloquialVersion
        {
            get
            {
                return GetAttribute(Schema.SoftwareIdentity.Attributes.ColloquialVersion);
            }
            set
            {
                SetAttribute(Schema.SoftwareIdentity.Attributes.ColloquialVersion, value);
            }
        }

        public string Edition
        {
            get
            {
                return GetAttribute(Schema.SoftwareIdentity.Attributes.Edition);
            }
            set
            {
                SetAttribute(Schema.SoftwareIdentity.Attributes.Edition, value);
            }
        }

        public string EntitlementKey
        {
            get
            {
                return GetAttribute(Schema.SoftwareIdentity.Attributes.EntitlementKey);
            }
            set
            {
                SetAttribute(Schema.SoftwareIdentity.Attributes.EntitlementKey, value);
            }
        }

        public string Generator
        {
            get
            {
                return GetAttribute(Schema.SoftwareIdentity.Attributes.Generator);
            }
            set
            {
                SetAttribute(Schema.SoftwareIdentity.Attributes.Generator, value);
            }
        }

        public string PersistentId
        {
            get
            {
                return GetAttribute(Schema.SoftwareIdentity.Attributes.PersistentId);
            }
            set
            {
                SetAttribute(Schema.SoftwareIdentity.Attributes.PersistentId, value);
            }
        }

        public string Product
        {
            get
            {
                return GetAttribute(Schema.SoftwareIdentity.Attributes.Product);
            }
            set
            {
                SetAttribute(Schema.SoftwareIdentity.Attributes.Product, value);
            }
        }

        public string ProductFamily
        {
            get
            {
                return GetAttribute(Schema.SoftwareIdentity.Attributes.ProductFamily);
            }
            set
            {
                SetAttribute(Schema.SoftwareIdentity.Attributes.ProductFamily, value);
            }
        }

        public string Revision
        {
            get
            {
                return GetAttribute(Schema.SoftwareIdentity.Attributes.Revision);
            }
            set
            {
                SetAttribute(Schema.SoftwareIdentity.Attributes.Revision, value);
            }
        }

        public string UnspscCode
        {
            get
            {
                return GetAttribute(Schema.SoftwareIdentity.Attributes.UnspscCode);
            }
            set
            {
                SetAttribute(Schema.SoftwareIdentity.Attributes.UnspscCode, value);
            }
        }

        public string UnspscVersion
        {
            get
            {
                return GetAttribute(Schema.SoftwareIdentity.Attributes.UnspscVersion);
            }
            set
            {
                SetAttribute(Schema.SoftwareIdentity.Attributes.UnspscVersion, value);
            }
        }

        public bool? EntitlementDataRequired
        {
            get
            {
                return GetAttribute(Schema.SoftwareIdentity.Attributes.EntitlementDataRequired).IsTruePreserveNull();
            }
            set
            {
                if (value != null) {
                    SetAttribute(Schema.SoftwareIdentity.Attributes.EntitlementDataRequired, value.ToString());
                }
            }
        }

        public override string ToString() {
            return Attributes.ToString();
        }
    }
}