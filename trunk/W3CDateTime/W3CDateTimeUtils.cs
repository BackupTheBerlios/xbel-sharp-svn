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
