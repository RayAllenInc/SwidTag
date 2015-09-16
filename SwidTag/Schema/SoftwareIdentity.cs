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
    using System.Xml.Linq;

    public static class SoftwareIdentity {
        public static class Attributes {
            public static readonly XName Name = Namespace.Swid + "name";
            public static readonly XName Patch = Namespace.Swid + "patch";
            public static readonly XName Media = Namespace.Swid + "media";
            public static readonly XName Supplemental = Namespace.Swid + "supplemental";
            public static readonly XName TagVersion = Namespace.Swid + "tagVersion";
            public static readonly XName TagId = Namespace.Swid + "tagId";
            public static readonly XName Version = Namespace.Swid + "version";
            public static readonly XName VersionScheme = Namespace.Swid + "versionScheme";
            public static readonly XName Corpus = Namespace.Swid + "corpus";
            public static readonly XName Summary = Namespace.Swid + "summary";
            public static readonly XName Description = Namespace.Swid + "description";
            public static readonly XName ActivationStatus = Namespace.Swid + "activationStatus";
            public static readonly XName ChannelType = Namespace.Swid + "channelType";
            public static readonly XName ColloquialVersion = Namespace.Swid + "colloquialVersion";
            public static readonly XName Edition = Namespace.Swid + "edition";
            public static readonly XName EntitlementDataRequired = Namespace.Swid + "entitlementDataRequired";
            public static readonly XName EntitlementKey = Namespace.Swid + "entitlementKey";
            public static readonly XName Generator = Namespace.Swid + "generator";
            public static readonly XName PersistentId = Namespace.Swid + "persistentId";
            public static readonly XName Product = Namespace.Swid + "product";
            public static readonly XName ProductFamily = Namespace.Swid + "productFamily";
            public static readonly XName Revision = Namespace.Swid + "revision";
            public static readonly XName UnspscCode = Namespace.Swid + "unspscCode";
            public static readonly XName UnspscVersion = Namespace.Swid + "unspscVersion";
            public static readonly XName RegId = Namespace.Swid + "regId";
            public static readonly XName Role = Namespace.Swid + "role";
            public static readonly XName Thumbprint = Namespace.Swid + "thumbprint";
            public static readonly XName HRef = Namespace.Swid + "href";
            public static readonly XName Relationship = Namespace.Swid + "rel";
            public static readonly XName MediaType = Namespace.Swid + "type";
            public static readonly XName Ownership = Namespace.Swid + "ownership";
            public static readonly XName Use = Namespace.Swid + "use";
            public static readonly XName Artifact = Namespace.Swid + "artifact";
            public static readonly XName Type = Namespace.Swid + "type";
            public static readonly XName Key = Namespace.Swid + "key";
            public static readonly XName Root = Namespace.Swid + "root";
            public static readonly XName Location = Namespace.Swid + "location";
            public static readonly XName Size = Namespace.Swid + "size";
            public static readonly XName Pid = Namespace.Swid + "pid";
            public static readonly XName Date = Namespace.Swid + "date";
            public static readonly XName DeviceId = Namespace.Swid + "deviceId";
            public static readonly XName XmlLang = Namespace.Xml + "lang";
            internal static readonly XName UniqueId = Namespace.Swid + "uniqueId";
        }

        public static class Elements {
            public static readonly XName SoftwareIdentity = Namespace.Swid + "SoftwareIdentity";
            public static readonly XName Entity = Namespace.Swid + "Entity";
            public static readonly XName Link = Namespace.Swid + "Link";
            public static readonly XName Evidence = Namespace.Swid + "Evidence";
            public static readonly XName Payload = Namespace.Swid + "Payload";
            public static readonly XName Meta = Namespace.Swid + "Meta";
            public static readonly XName Directory = Namespace.Swid + "Directory";
            public static readonly XName File = Namespace.Swid + "File";
            public static readonly XName Process = Namespace.Swid + "Process";
            public static readonly XName Resource = Namespace.Swid + "Resource";

            public static readonly XName[] MetaElements = {
                Meta, Directory, File, Process, Resource
            };
        }
    }
}