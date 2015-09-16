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

namespace FearTheCowboy.Iso19770.Test {
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;
    using Schema;
    using Support;
    using Xunit;
    using Xunit.Abstractions;
    using SoftwareIdentity = Iso19770.SoftwareIdentity;

    public class PersistenceTests : Tests {
        public PersistenceTests(ITestOutputHelper outputHelper)
            : base(outputHelper) {
        }

        [Fact]
        public void ContextTest() {
            using (CaptureConsole) {
                ;
                Console.WriteLine(IdentityIndex.Context.ToString(Formatting.None).Replace("},", "},\r\n    "));
            }
        }

        [Fact]
        public void ConsoleTest() {
            using (CaptureConsole) {
                var tag = SoftwareIdentity.LoadHtml(File.ReadAllText("Samples\\SimpleTags.html"));
                Assert.NotNull(tag);
                Console.WriteLine(tag.SwidTagXml);
            }
        }

        [Fact]
        public void JSonTest() {
            using (CaptureConsole) {
                var tag = SoftwareIdentity.LoadJson(File.ReadAllText("Samples\\SimpleTag.json"));
                Assert.NotNull(tag);
                Console.WriteLine(tag.SwidTagXml);
                Console.WriteLine(tag.SwidTagJson);
            }
        }

        [Fact]
        public void RemoveLinkTest() {
            using (CaptureConsole) {
                var tag = SoftwareIdentity.LoadXml(File.ReadAllText("Samples\\swid.feed.xml"));

                Console.WriteLine(tag.SwidTagXml);
                tag.RemoveLink(tag.Links.FirstOrDefault().HRef);
                Console.WriteLine(tag.SwidTagXml);
                var j = tag.SwidTagJson;
                Console.WriteLine(j);
                var newtag = SoftwareIdentity.LoadJson(j);
                var xtag = SoftwareIdentity.LoadXml(tag.SwidTagXml);

                Console.WriteLine(newtag.SwidTagJson);
                Console.WriteLine(xtag.SwidTagJson);
                Assert.Equal(newtag.SwidTagJson, xtag.SwidTagJson);

                Console.WriteLine(newtag.SwidTagXml);
                Console.WriteLine(xtag.SwidTagXml);

                Console.WriteLine(xtag.SwidTagJson);

                var a1 = SoftwareIdentity.LoadXml(newtag.SwidTagXml);
                var a2 = SoftwareIdentity.LoadXml(xtag.SwidTagXml);

                var a3 = a1.SwidTagJson;
                var a4 = a2.SwidTagJson;

                var aa = SoftwareIdentity.LoadJson(a3);
                var bb = SoftwareIdentity.LoadJson(a4);
                var cc = aa.SwidTagXml;
                var dd = bb.SwidTagXml;

                Assert.Equal(cc, dd);
            }
        }

        [Fact]
        public void XmlTest() {
            using (CaptureConsole) {
                var tag = SoftwareIdentity.LoadXml(File.ReadAllText("Samples\\swid.feed.xml"));
                Assert.NotNull(tag);
                Console.WriteLine(tag.SwidTagXml);
                Console.WriteLine(tag.SwidTagJson);
            }
        }

        [Fact]
        public void ParameterizedDiscoveryTest() {
            using(CaptureConsole) {
                var tag = SoftwareIdentity.LoadXml(File.ReadAllText("Samples\\ParameterizedDiscovery.feed.swidtag"));
                Assert.NotNull(tag);
                Console.WriteLine(tag.SwidTagXml);
                Console.WriteLine(tag.SwidTagJson);
            }
        }
    }
}