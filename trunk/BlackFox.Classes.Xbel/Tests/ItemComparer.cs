/*
 * Xbel library
 * Copyright (C) 2004 Roncaglia Julien (Black Fox) <black-fox@virtualblackfox.net>
 * 
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */

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
