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
    public static class JSonMembers {
        public static readonly string SoftwareIdentity = Elements.SoftwareIdentity.ToJsonId();
        public static readonly string Entity = Elements.Entity.ToJsonId();
        public static readonly string Link = Elements.Link.ToJsonId();
        public static readonly string Evidence = Elements.Evidence.ToJsonId();
        public static readonly string Payload = Elements.Payload.ToJsonId();
        public static readonly string Meta = Elements.Meta.ToJsonId();
        public static readonly string Directory = Elements.Directory.ToJsonId();
        public static readonly string File = Elements.File.ToJsonId();
        public static readonly string Process = Elements.Process.ToJsonId();
        public static readonly string Resource = Elements.Resource.ToJsonId();
        public static readonly string Name = Attributes.Name.ToJsonId();
        public static readonly string Patch = Attributes.Patch.ToJsonId();
        public static readonly string Media = Attributes.Media.ToJsonId();
        public static readonly string Supplemental = Attributes.Supplemental.ToJsonId();
        public static readonly string TagVersion = Attributes.TagVersion.ToJsonId();
        public static readonly string TagId = Attributes.TagId.ToJsonId();
        public static readonly string Version = Attributes.Version.ToJsonId();
        public static readonly string VersionScheme = Attributes.VersionScheme.ToJsonId();
        public static readonly string Corpus = Attributes.Corpus.ToJsonId();
        public static readonly string Summary = Attributes.Summary.ToJsonId();
        public static readonly string Description = Attributes.Description.ToJsonId();
        public static readonly string ActivationStatus = Attributes.ActivationStatus.ToJsonId();
        public static readonly string ChannelType = Attributes.ChannelType.ToJsonId();
        public static readonly string ColloquialVersion = Attributes.ColloquialVersion.ToJsonId();
        public static readonly string Edition = Attributes.Edition.ToJsonId();
        public static readonly string EntitlementDataRequired = Attributes.EntitlementDataRequired.ToJsonId();
        public static readonly string EntitlementKey = Attributes.EntitlementKey.ToJsonId();
        public static readonly string Generator = Attributes.Generator.ToJsonId();
        public static readonly string PersistentId = Attributes.PersistentId.ToJsonId();
        public static readonly string Product = Attributes.Product.ToJsonId();
        public static readonly string ProductFamily = Attributes.ProductFamily.ToJsonId();
        public static readonly string Revision = Attributes.Revision.ToJsonId();
        public static readonly string UnspscCode = Attributes.UnspscCode.ToJsonId();
        public static readonly string UnspscVersion = Attributes.UnspscVersion.ToJsonId();
        public static readonly string RegId = Attributes.RegId.ToJsonId();
        public static readonly string Role = Attributes.Role.ToJsonId();
        public static readonly string Thumbprint = Attributes.Thumbprint.ToJsonId();
        public static readonly string HRef = Attributes.HRef.ToJsonId();
        public static readonly string Relationship = Attributes.Relationship.ToJsonId();
        public static readonly string MediaType = Attributes.MediaType.ToJsonId();
        public static readonly string Ownership = Attributes.Ownership.ToJsonId();
        public static readonly string Use = Attributes.Use.ToJsonId();
        public static readonly string Artifact = Attributes.Artifact.ToJsonId();
        public static readonly string Type = Attributes.Type.ToJsonId();
        public static readonly string Key = Attributes.Key.ToJsonId();
        public static readonly string Root = Attributes.Root.ToJsonId();
        public static readonly string Location = Attributes.Location.ToJsonId();
        public static readonly string Size = Attributes.Size.ToJsonId();
        public static readonly string Pid = Attributes.Pid.ToJsonId();
        public static readonly string Date = Attributes.Date.ToJsonId();
        public static readonly string DeviceId = Attributes.DeviceId.ToJsonId();
    }
}