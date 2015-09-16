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

    public static class Discovery {
        public static class Attributes {
            public static readonly XName Name = Namespace.Discovery + "name";
            // Feed Link Extended attributes: 
            public static readonly XName MinimumName = Namespace.Discovery + "min-name";
            public static readonly XName MaximumName = Namespace.Discovery + "max-name";
            public static readonly XName MinimumVersion = Namespace.Discovery + "min-version";
            public static readonly XName MaximumVersion = Namespace.Discovery + "max-version";
            public static readonly XName Keyword = Namespace.Discovery + "keyword";
            // Package Link Extended Attributes 
            public static readonly XName Version = Namespace.Discovery + "version";
            public static readonly XName Latest = Namespace.Discovery + "latest";
            public static readonly XName TargetFilename = Namespace.Discovery + "targetFilename";
            public static readonly XName Type = Namespace.Discovery + "type";

            // parameterized discovery 
            public static readonly XName Values = Namespace.Discovery + "values";
            public static readonly XName Required = Namespace.Discovery + "required";
        }

        public static class Elements {
            // parameterized discovery 
            public static readonly XName Parameter = Namespace.Discovery + "parameter";
        }
    }
}