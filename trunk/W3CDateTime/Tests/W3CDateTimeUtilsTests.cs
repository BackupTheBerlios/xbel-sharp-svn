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
