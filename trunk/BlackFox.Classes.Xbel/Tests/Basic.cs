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
