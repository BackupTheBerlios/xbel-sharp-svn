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
