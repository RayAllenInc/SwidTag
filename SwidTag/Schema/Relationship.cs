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
    public static class Relationship {
        public const string Requires = "requires";
        public const string InstallationMedia = "installationmedia";
        public const string Component = "component";
        public const string Supplemental = "supplemental";
        public const string Parent = "parent";
        public const string Ancestor = "ancestor";
        // Package Discovery Relationships:
        public const string Feed = "feed"; // should point to a swidtag the represents a feed of packages
        public const string Package = "package"; // should point ot a swidtag that represents an installation package
    }
}