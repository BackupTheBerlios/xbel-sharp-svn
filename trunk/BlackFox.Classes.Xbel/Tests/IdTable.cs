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
