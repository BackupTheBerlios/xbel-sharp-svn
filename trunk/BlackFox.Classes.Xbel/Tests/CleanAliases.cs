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
