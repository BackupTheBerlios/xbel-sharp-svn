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
	/// <summary>
	/// A bookmark to an URL.
	/// </summary>
	public class XbelBookmark : XbelIdentifiedItem
	{
		public override string XmlElementName
		{
			get {
				return "bookmark";
			}
		}
		
		public string Href = "";
		public string Info = "";
		//public string Desc = "";
		
		public DateTime Added;
		public DateTime Modified;
		public DateTime Visited;

		public XbelBookmark(XbelFolder Parent) : base(Parent)
		{
			Init();
		}
		
		public XbelBookmark(XbelDocument Doc) : base(Doc)
		{
			Init();
		}
		
		private void Init()
		{
			Added    = new System.DateTime();
			Modified = new System.DateTime();
			Visited  = new System.DateTime();
		}
		
		public override void LoadFromXmlNode(XmlNode node)
		{
			base.LoadFromXmlNode(node);
			
			if (node.Attributes["href"] != null) {
				Href = node.Attributes["href"].Value;
			}
		}
		
		public override void SaveToXmlNode(XmlNode node)
		{
			base.SaveToXmlNode(node);
			
			XmlAttribute a = node.OwnerDocument.CreateAttribute("href");
			a.Value = Href;
			node.Attributes.Append(a);
		}
		
		/// <summary>
		/// Returns the text version of the Bookmark in the form :
		/// [id] Title (href)
		/// </summary>
		public override string ToString()
		{
			return "[" + Id + "] " + Title + " (" + Desc + ")";
		}
	}
}
