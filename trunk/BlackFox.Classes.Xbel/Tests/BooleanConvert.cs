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
	public class BooleanConvert
	{
		[Test]
		public void ConvertToString()
		{
			Assert.IsTrue(XbelBooleanConvert.ToString(XbelBoolean.Yes) == "yes");
			Assert.IsTrue(XbelBooleanConvert.ToString(XbelBoolean.No) == "no");
			Assert.IsTrue(XbelBooleanConvert.ToString(XbelBoolean.Unknown) == "");
		}
		
		[Test]
		public void ConvertFromString()
		{
			Assert.IsTrue(XbelBooleanConvert.FromString("yes") == XbelBoolean.Yes);
			Assert.IsTrue(XbelBooleanConvert.FromString("no") == XbelBoolean.No);
			Assert.IsTrue(XbelBooleanConvert.FromString("fssdfdsf") == XbelBoolean.Unknown);
		}
	}
}
