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
using System.Collections;

namespace BlackFox.Classes.Xbel
{
	public class XbelItemComparer : IComparer
	{
		/// <summary>
		/// Compares two objects and returns a value indicating whether one is less than, equal to or greater than the other.
		/// </summary>
		/// <param name="x">First object to compare.</param>
		/// <param name="y">Second object to compare.</param>
		/// <returns>
		/// 	<list type="table">
		/// 		<listheader>
		/// 			<term>Value</term>
		/// 			<description>Condition</description>
		/// 		</listheader>
		/// 		<item>
		/// 			<term>Less than zero</term>
		/// 			<description>x is less than y.</description>
		/// 		</item>
		/// 		<item>
		/// 			<term>Zero</term>
		/// 			<description>x equals y.</description>
		/// 		</item>
		/// 		<item>
		/// 			<term>Greater than zero</term>
		/// 			<description>x is greater than y.</description>
		/// 		</item>
		/// 	</list>
		/// 	With three rules :
		/// 	<list type="number">
		/// 		<item><term>Normal Items are greater than IdentifedItems.</term></item>
		/// 		<item><term>IdentifiedItems are greater than Folders.</term></item>
		/// 		<item><term>IdentifiedItems are in alphabetical order.</term></item>
		/// 	</list>
		/// </returns>
		public int Compare(object x, object y)
		{
			if (!((x is XbelItem) && (y is XbelItem))) {
				throw new ArgumentException("Impossible de comparer autre chose que des XbelItem");
			}
			if ((x is XbelIdentifiedItem) && (y is XbelIdentifiedItem)) {
				if ((x is XbelFolder) && !(y is XbelFolder)) {
					return -1;
				} else if ((y is XbelFolder) && !(x is XbelFolder)) {
					return 1;
				} else {
					return (x as XbelIdentifiedItem).Title.CompareTo((y as XbelIdentifiedItem).Title);
				}
			} else if (x is XbelIdentifiedItem) {
				return -1;
			} else if (y is XbelIdentifiedItem) {
				return 1;
			} else {
				return 0;
			}
		}
	}
}
