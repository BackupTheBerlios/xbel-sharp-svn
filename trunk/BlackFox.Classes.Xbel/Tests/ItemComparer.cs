using System;
using System.Collections;
using NUnit.Framework;

namespace BlackFox.Classes.Xbel.Tests
{
	[TestFixture]
	public class ItemComparerTests
	{
		private XbelDocument doc;
		private IComparer comp;
		
		[SetUp] public void Init()
		{
			doc = new XbelDocument();
			comp = new XbelItemComparer();
		}
		
		[Test]
		public void Compare()
		{
			XbelIdentifiedItem bm1 = new XbelBookmark(doc);
			XbelIdentifiedItem bm2 = new XbelBookmark(doc);
			
			XbelIdentifiedItem fold1 = new XbelFolder(doc);
			XbelIdentifiedItem fold2 = new XbelFolder(doc);
			XbelItem sep = new XbelSeparator(doc);
			
			bm1.Title = "aaa";
			bm2.Title = "bbb";
			
			fold1.Title = "aaa";
			fold2.Title = "bbb";
			
			Assert.AreEqual(-1, comp.Compare(bm1, bm2));
			Assert.AreEqual(-1, comp.Compare(fold1, fold2));
			Assert.AreEqual(-1, comp.Compare(fold1, bm2));
			Assert.AreEqual(-1, comp.Compare(fold2, bm1));
			Assert.AreEqual(-1, comp.Compare(bm1, sep));
			Assert.AreEqual(-1, comp.Compare(fold1, sep));
		}
		
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void CompareIntAndInt()
		{
			comp.Compare(3, 5);
		}
		
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void CompareIntAndItem()
		{
			XbelIdentifiedItem bm1 = new XbelBookmark(doc);
			comp.Compare(3, bm1);
		}
		
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void CompareItemAndInt()
		{
			XbelIdentifiedItem bm1 = new XbelBookmark(doc);
			comp.Compare(bm1, 5);
		}
	}
}
