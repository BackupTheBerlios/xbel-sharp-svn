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

namespace BlackFox.Classes.Xbel
{
	public class XbelSeparator : XbelItem
	{
		public override string XmlElementName
		{
			get {
				return "separator";
			}
		}
		
		public XbelSeparator(XbelFolder Parent) : base(Parent)
		{
		}
		
		public XbelSeparator(XbelDocument Doc) : base(Doc)
		{
		}
	
		public override void LoadFromXmlNode(XmlNode node)
		{
			// Separators have no properties
		}
		
		public override void SaveToXmlNode(XmlNode node)
		{
			// Separators have no properties
		}
		
		public override string ToString()
		{
			return "---- Separator ----" ;
		}
	}
}
