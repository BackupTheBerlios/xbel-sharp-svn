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

namespace BlackFox.Classes.Xbel
{
	/// <summary>
	/// Some properties - as the folded propery in  <see cref="XbelFolder"/> - has boolean-like
	/// values of "yes" or "no". This enum is used to store the value.
	/// </summary>
	public enum XbelBoolean
	{
		Yes,
		No,
		Unknown
	}
	
	public sealed class XbelBooleanConvert
	{
		private static string[] textValues = {"yes", "no", ""};
		
		private XbelBooleanConvert() {}
		
		public static string ToString(XbelBoolean value)
		{
			return textValues[(int)value];
		}
		
		public static XbelBoolean FromString(string value)
		{
			if (value == textValues[0]) {
				return XbelBoolean.Yes;
			} else if (value == textValues[1]) {
				return XbelBoolean.No;
			} else {
				return XbelBoolean.Unknown;
			}
		}
		
		public static XbelBoolean FromBoolean(bool value)
		{
			if (value) {
				return XbelBoolean.Yes;
			} else {
				return XbelBoolean.No;
			}
		}
		
		public static bool ToBoolean(XbelBoolean value)
		{
			return ToBoolean(value, false);
		}
		
		public static bool ToBoolean(XbelBoolean value, bool defaultValue)
		{
			if (value == XbelBoolean.Yes) {
				return true;
			} else if (value == XbelBoolean.No) {
				return false;
			} else {
				return defaultValue;
			}
		}
	}
}
