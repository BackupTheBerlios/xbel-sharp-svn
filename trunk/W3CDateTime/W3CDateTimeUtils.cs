using System;
using System.Globalization;

namespace BlackFox.Classes
{
	/// <summary>
	/// Description of W3CDateTime.	
	/// </summary>
	public class W3CDateTimeUtils
	{
		static string[] W3CDateTimePatternsUTC = {
			"yyyy'-'MM'-'dd'T'HH'Z'",
			"yyyy'-'MM'-'dd'T'HH':'mm'Z'",
			"yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'",
		};

		static string[] W3CDateTimePatternsTZ = {
			"yyyy'-'MM'-'dd'T'HHzzz",
			"yyyy'-'MM'-'dd'T'HH':'mmzzz",
			"yyyy'-'MM'-'dd'T'HH':'mm':'sszzz"
		};
		
		static string[] W3CDateTimePatternsBasic = {
			"yyyy",
			"yyyy'-'MM",
			"yyyy'-'MM'-'dd"
		};
		
		public static DateTime W3CDateTimeToDateTime(String w3cDateTime)
		{
			Console.WriteLine("Toto");
			DateTime dt;
			
			if (w3cDateTime.IndexOf('T') != -1) {
				if (w3cDateTime.IndexOf('Z') != -1) {
					dt = DateTime.ParseExact(w3cDateTime,
					                         W3CDateTimePatternsUTC,
					                         DateTimeFormatInfo.InvariantInfo,
					                         DateTimeStyles.NoCurrentDateDefault);
					dt = dt.ToLocalTime();
				} else {
					dt = DateTime.ParseExact(w3cDateTime,
					                         W3CDateTimePatternsTZ,
					                         DateTimeFormatInfo.InvariantInfo,
					                         DateTimeStyles.NoCurrentDateDefault);
				}
			} else {
				dt = DateTime.ParseExact(w3cDateTime,
				                         W3CDateTimePatternsBasic,
				                         DateTimeFormatInfo.InvariantInfo,
				                         DateTimeStyles.NoCurrentDateDefault);
				dt = dt.ToLocalTime();
			}
			return dt;
		}
		
		public static string DateTimeToW3CDateTime(DateTime value)
		{
			return DateTimeToW3CDateTime(value, W3CDateTimePatternsUTC[2]);
		}
		
		public static string DateTimeToW3CDateTime(DateTime value, string format)
		{
			DateTime UTCvalue = value.ToUniversalTime();
			return UTCvalue.ToString(format, DateTimeFormatInfo.InvariantInfo);
		}
	}
}
