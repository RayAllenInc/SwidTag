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
    public static class MediaType {
        public const string PackageReference = "application/vnd.packagemanagement-canonicalid";
        public const string SwidTagXml = "application/swid-tag+xml";
        public const string SwidTagJsonLd = "application/swid-tag+json";
        public const string MsiPackage = "application/vnd.ms.msi-package";
        public const string MsuPackage = "application/vnd.ms.msu-package";
        public const string ExePackage = "application/vnd.packagemanagement.exe-package";
        public const string NuGetPackage = "application/vnd.packagemanagement.nuget-package";
        public const string ChocolateyPackage = "application/vnd.packagemanagement.chocolatey-package";
    }
}