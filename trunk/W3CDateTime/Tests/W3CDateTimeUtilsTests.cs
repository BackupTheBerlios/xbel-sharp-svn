/*
 * W3CDateTimeUtils
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
using System.Globalization;
using BlackFox.Classes;

namespace BlackFox.Classes.Tests
{
	[TestFixture]
	public class W3CDateTimeUtilsTests
	{
		DateTime dt;
		
		[Ignore("Not implemented and not used i think : Bloat")]
		[Test]
		public void ymdhmss()
		{
			dt = W3CDateTimeUtils.W3CDateTimeToDateTime("2004-05-01T22:05:47.5+02:00");
			Assert.AreEqual("2004-05-01T20:05:47Z", W3CDateTimeUtils.DateTimeToW3CDateTime(dt), "TZ");
			
			dt = W3CDateTimeUtils.W3CDateTimeToDateTime("2004-05-01T20:05:47.5Z");
			Assert.AreEqual("2004-05-01T20:05:47Z", W3CDateTimeUtils.DateTimeToW3CDateTime(dt), "UTC");
		}
		
		[Test]
		public void ymdhms()
		{
			dt = W3CDateTimeUtils.W3CDateTimeToDateTime("2004-05-01T22:05:47+02:00");
			Assert.AreEqual("2004-05-01T20:05:47Z", W3CDateTimeUtils.DateTimeToW3CDateTime(dt), "TZ");
			
			dt = W3CDateTimeUtils.W3CDateTimeToDateTime("2004-05-01T20:05:47Z");
			Assert.AreEqual("2004-05-01T20:05:47Z", W3CDateTimeUtils.DateTimeToW3CDateTime(dt), "UTC");
		}

		[Test]
		public void ymdhm()
		{
			dt = W3CDateTimeUtils.W3CDateTimeToDateTime("2004-05-01T22:05+02:00");
			Assert.AreEqual("2004-05-01T20:05:00Z", W3CDateTimeUtils.DateTimeToW3CDateTime(dt), "TZ");
			
			dt = W3CDateTimeUtils.W3CDateTimeToDateTime("2004-05-01T20:05Z");
			Assert.AreEqual("2004-05-01T20:05:00Z", W3CDateTimeUtils.DateTimeToW3CDateTime(dt), "UTC");
		}

		[Test]
		public void ymdh()
		{
			dt = W3CDateTimeUtils.W3CDateTimeToDateTime("2004-05-01T22+02:00");
			Assert.AreEqual("2004-05-01T20:00:00Z", W3CDateTimeUtils.DateTimeToW3CDateTime(dt), "TZ");
			
			dt = W3CDateTimeUtils.W3CDateTimeToDateTime("2004-05-01T20Z");
			Assert.AreEqual("2004-05-01T20:00:00Z", W3CDateTimeUtils.DateTimeToW3CDateTime(dt), "UTC");
		}

		[Test]
		public void ymd()
		{
			dt = W3CDateTimeUtils.W3CDateTimeToDateTime("2004-05-01");
			Assert.AreEqual("2004-05-01T00:00:00Z", W3CDateTimeUtils.DateTimeToW3CDateTime(dt), "2004-05-01");
		}

		[Test]
		public void ym()
		{
			dt = W3CDateTimeUtils.W3CDateTimeToDateTime("2004-05");
			Assert.AreEqual("2004-05-01T00:00:00Z", W3CDateTimeUtils.DateTimeToW3CDateTime(dt), "2004-05");
		}

		[Test]
		public void y()
		{
			dt = W3CDateTimeUtils.W3CDateTimeToDateTime("2004");
			Assert.AreEqual("2004-01-01T00:00:00Z", W3CDateTimeUtils.DateTimeToW3CDateTime(dt), "2004");
		}
	}
}
