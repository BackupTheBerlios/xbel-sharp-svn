using System;
using System.Xml;
using NUnit.Framework;

namespace BlackFox.Classes.Xbel.Tests
{
	public class TestProcessor : XbelMetadata
	{
		private string m_test;
		
		new public static string Owner {
			get {
				return "Test";
			}
		}
		
		public string Test
		{
			get {
				return m_test;
			}
		}
		
		public override void LoadFromXml(XmlElement element)
		{
			m_test = element.Attributes["test"].Value;
		}
		
		public override void SaveToXml(XmlDocumentFragment fragment)
		{
			fragment.InnerXml = "<metadata owner=\"Test\">This was a test</metadata>";
		}
	}
	
	
	[TestFixture]
	public class MetadataProcessor
	{
		private XbelDocument doc;
		//private XbelBookmark item;
		
		[SetUp]
		public void Init()
		{
			doc = new XbelDocument();
			doc.AddMetadataProcessor(typeof(TestProcessor));
			//item = new XbelBookmark(doc);
			//doc.Add(item);
		}

		[TearDown]
		public void Dispose()
		{
			doc = null;
		}

		[Test]
		public void Find()
		{
			Assert.AreSame(doc.FindMetadataProcessor("Not Found"), typeof(XbelRawMetadata),
			               "Find unexistant processor");
			Type t = doc.FindMetadataProcessor("Test");
			Assert.AreSame(t, typeof(TestProcessor), "Type added but not found");
		}
		
		[Test]
		public void RemoveByOwner()
		{
			doc.RemoveMetadataProcessor("Test");
			Assert.AreSame(doc.FindMetadataProcessor("Test"), typeof(XbelRawMetadata));
		}
		
		[Test]
		public void RemoveByType()
		{
			doc.RemoveMetadataProcessor(typeof(TestProcessor));
			Assert.AreSame(doc.FindMetadataProcessor("Test"), typeof(XbelRawMetadata));
		}
		
		[Test]
		public void RemoveUnexistantProcessor()
		{
			doc.RemoveMetadataProcessor("Not Found");
		}
		
		[Test]
		public void LoadFromXml_TestProcessor()
		{
			string xmltxt = @"<?xml version=""1.0""?><xbel version=""2.0""><info><metadata owner=""Test"" test=""Ok"" /><metadata owner=""void"" /></info></xbel>";
			XmlDocument xmldoc = new XmlDocument();
			xmldoc.LoadXml(xmltxt);
			doc.LoadFromXmlDocument(xmldoc);
			Assert.AreEqual(doc.Infos.Count, 2, "Il n'y as pas que deux ellements metadata");
			Assert.IsTrue(doc.Infos[0] is TestProcessor, "Le metadata n'as pas été reconnu comme un \"Testprocessor\"");
			Assert.IsTrue((doc.Infos[0] as TestProcessor).Test == "Ok", "L'attribut n'as pas été trouvé");
		}
		
		[Test]
		public void LoadFromXml_XbelRawMetadata()
		{
			string xmltxt = @"<?xml version=""1.0""?><xbel version=""2.0""><info><metadata owner=""void"" param=""val"">Content</metadata></info></xbel>";
			
			XmlDocument xmldoc = new XmlDocument();
			xmldoc.LoadXml(xmltxt);
			doc.LoadFromXmlDocument(xmldoc);
			
			Assert.AreEqual(doc.Infos.Count, 1, "Il n'y as pas qu'un ellement metadata");
			Assert.IsTrue(doc.Infos[0] is XbelRawMetadata, "Le metadata n'as pas été reconnu comme un \"XbelRawMetadata\"");
			Assert.IsTrue((doc.Infos[0] as XbelRawMetadata).Content == "<metadata owner=\"void\" param=\"val\">Content</metadata>", "Unexpected Content");
		}
		
		[Test]
		public void SaveToXml_XbelRawMetadata()
		{
			string xmltxt = @"<?xml version=""1.0""?><xbel version=""2.0""><info><metadata owner=""void"" param=""val"">Content</metadata></info></xbel>";
			
			XmlDocument xmldoc = new XmlDocument();
			xmldoc.LoadXml(xmltxt);
			doc.LoadFromXmlDocument(xmldoc);
			
			XmlDocumentFragment df = xmldoc.CreateDocumentFragment();
			(doc.Infos[0] as XbelMetadata).SaveToXml(df);
			Assert.IsTrue(df.OuterXml == "<metadata owner=\"void\" param=\"val\">Content</metadata>", "Saved data unexpected");
		}

	}
}
