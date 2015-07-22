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