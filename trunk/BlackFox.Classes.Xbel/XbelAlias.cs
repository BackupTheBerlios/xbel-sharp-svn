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
using System.Collections;

namespace BlackFox.Classes.Xbel
{
	/// <summary>
	/// An alias to a <see cref="XbelIdentifiedItem"/>.
	/// </summary>
	public class XbelAlias : XbelItem
	{
		public override string XmlElementName
		{
			get {
				return "alias";
			}
		}
		
		public string IdRef = "";
		
		/// <summary>
		/// The <see cref="XbelIdentifiedItem"/> identified by the Alias.
		/// </summary>
		/// <returns>
		/// Null if the IdRef is "", the item otherwise.
		/// </returns>
		/// <exception cref="InvalidOperationException">No item with item.Id == this.IdRef found.</exception>
		public XbelIdentifiedItem AliasFor {
			get {
				if (IdRef != "") {
					XbelIdentifiedItem item = Document.IdTable[IdRef] as XbelIdentifiedItem;
					if (item == null) {
						throw new InvalidOperationException("Alias points to an unknown item.");
					}
					return (item);
				} else {
					return null;
				}
			}
			set {
				this.IdRef = value.Id;
			}
		}
		
		public XbelAlias(XbelFolder Parent) : base(Parent)
		{
		}

		public XbelAlias(XbelDocument Doc) : base(Doc)
		{
		}
		
		public override void SaveToXmlNode(XmlNode node)
		{
			XmlAttribute refAttribute = node.OwnerDocument.CreateAttribute("ref");
			refAttribute.Value = IdRef;
			node.Attributes.Append(refAttribute);
		}
		
		public override void LoadFromXmlNode(XmlNode node)
		{
			if (node.Attributes["ref"] != null) {
				IdRef = node.Attributes["ref"].Value;
			}
		}
		
		public override string ToString()
		{
			if (AliasFor == null) {
				return "Unresolved alias to ref [" + IdRef + "]" ;
			} else {
				return "Alias to ref [" + IdRef + "] \"" + AliasFor.Title + "\"";
			}
		}
	}
}
