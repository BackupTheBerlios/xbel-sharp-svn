using System;
using NUnit.Framework;

namespace BlackFox.Classes.Xbel.Tests
{
	[TestFixture]
	public class IdTableTests
	{
		private XbelDocument doc;
		private XbelBookmark item;
		[SetUp] public void Init()
		{
			doc = new XbelDocument();
			item = new XbelBookmark(doc);
			doc.Add(item);
		}

		[TearDown] public void Dispose()
		{
			doc = null;
			item = null;
		}
		
		[Test]
		public void ChangeIdOfItemAlreadyHere()
		{
			item.Id = "identified_item";
			Assert.AreSame(item, doc.IdTable["identified_item"]);
			
			doc.Remove(item);
			Assert.IsNull(doc.IdTable["identified_item"]);
		}

		[Test]
		public void AddANewItemWithId()
		{
			XbelBookmark item2 = new XbelBookmark(doc);
			item2.Id = "identified_item2";
			doc.Add(item2);
			Assert.AreSame(item2, doc.IdTable["identified_item2"]);
			
			doc.Remove(item2);
			Assert.IsNull(doc.IdTable["identified_item2"]);
		}

		[Test]
		public void TwoItems()
		{
			XbelBookmark item2 = new XbelBookmark(doc);
			item2.Id = "identified_item2";
			doc.Add(item2);
			
			item.Id = "identified_item1";
			
			Assert.AreSame(item, doc.IdTable["identified_item1"]);
			Assert.AreSame(item2, doc.IdTable["identified_item2"]);
			
			doc.Remove(item2);
			Assert.IsNull(doc.IdTable["identified_item2"]);
			Assert.AreSame(item, doc.IdTable["identified_item1"]);
			
			doc.Remove(item);
			Assert.IsNull(doc.IdTable["identified_item1"]);
		}
	}
}
