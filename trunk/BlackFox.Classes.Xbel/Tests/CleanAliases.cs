using System;
using System.Xml;
using NUnit.Framework;

namespace BlackFox.Classes.Xbel.Tests
{
	[TestFixture]
	public class CleanAliasesTests
	{
		private XbelDocument doc;
		private XbelBookmark item;
		private XbelAlias validAlias;
		
		[SetUp]
		public void Init()
		{
			doc = new XbelDocument();
			doc.AddMetadataProcessor(typeof(TestProcessor));
			
			item = new XbelBookmark(doc);
			item.Id = "item1";
			doc.Add(item);
			
			validAlias = new XbelAlias(doc);
			validAlias.IdRef = "item1";
			doc.Add(validAlias);
		}
		
		[Test]
		public void AliasToUnknown()
		{
			XbelAlias buggyAlias = new XbelAlias(doc);
			buggyAlias.IdRef = "unknown item";
			
			doc.CleanAliases();
			Assert.IsFalse(doc.Contains(buggyAlias), "Le document ne devrais plus contenir cet alias");
			
			Assert.AreSame(validAlias.AliasFor, item, "Item légitime 'item1' non référencé par validAlias");
		}
		
		[Test]
		public void AliasToNothing()
		{
			XbelAlias buggyAlias = new XbelAlias(doc);
			buggyAlias.IdRef = "";
			
			doc.CleanAliases();
			Assert.IsFalse(doc.Contains(buggyAlias), "Le document ne devrais plus contenir cet alias");
			
			Assert.AreSame(validAlias.AliasFor, item, "Item légitime 'item1' non référencé par validAlias");
		}
	}
}
