using System;
using NUnit.Framework;

namespace BlackFox.Classes.Xbel.Tests
{
	[TestFixture]
	public class BasicTests
	{
		private XbelDocument doc;
		private XbelBookmark item;
		
		[SetUp] public void Init()
		{
			doc = new XbelDocument();
			item = new XbelBookmark(doc);
			doc.Add(item);
		}

		[Test]
		public void Remove_Contains()
		{
			Assert.IsTrue(doc.Contains(item));
			doc.Remove(item);
			Assert.IsFalse(doc.Contains(item));
		}
		
		[Test]
		public void Remove_Count()
		{
			Assert.AreEqual(doc.Count, 1);
			doc.Remove(item);
			Assert.AreEqual(doc.Count, 0);
		}
		
		[Test]
		public void IndexOf()
		{
			int index = doc.IndexOf(item);
			Assert.AreEqual(index, 0);
			Assert.AreSame(item, doc[index]);
		}
		
		[Test]
		public void RemoveAt()
		{
			int index = doc.IndexOf(item);
			doc.RemoveAt(index);
			Assert.AreEqual(doc.Count, 0);			
		}
	}
}
