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
	/// A basic item in an <see cref="XbelDocument"/>.
	/// </summary>
	public abstract class XbelItem
	{
		#region XmlElementName property
		
		/// <summary>
		/// Name of the Item in Xml form.
		/// </summary>
		public abstract string XmlElementName
		{
			get;
		}
		
		#endregion
		
		#region Parent property
		
		private XbelFolder parent;
		
		/// <summary>
		/// The folder that this item is in.
		/// </summary>
		/// <remarks>
		/// The root folder has <code>Parent == null</code>
		/// </remarks>
		public XbelFolder Parent {
			get {
				return parent;
			}
		}
		
		#endregion
		
		#region Level property
		
		/// <summary>
		/// Level in the tree.
		/// </summary>
		/// <remarks>
		/// The root element is level 0.
		/// Calculated at each call recursivelly from parents nodes.
		/// </remarks>
		public int Level {
			get {
				if (Parent == null) {
					return 0;
				} else {
					return Parent.Level + 1;
				}
			}
		}
		
		#endregion
		
		#region Document property

		private XbelDocument doc;

		/// <summary>
		/// The document that this item is in.
		/// </summary>		
		public XbelDocument Document {
			get {
				return doc;
			}
		}
		
		#endregion

		#region Constructors

		/// <summary>
		/// Create an instance with Parent as Parent in the same
		/// <see cref="XbelDocument"/> as Parent.
		/// </summary>
		protected XbelItem(XbelFolder parent)
		{
			if (parent != null) {
				this.parent = parent;
				this.doc = parent.Document;
			} else {
				throw new ArgumentException("Parent is null.");
			}
		}
		
		/// <summary>
		/// Create an instance with <code>Parent == null</code> in the
		/// specified <see cref="XbelDocument"/>.
		/// If Document is null and this item is a XbelDocument then it create the
		/// instance with Document == himself
		/// </summary>
		protected XbelItem(XbelDocument document)
		{
			if (document != null) {
				this.doc = document;
				this.parent = null;				
			} else {
				if (this is XbelDocument) {
					this.doc = this as XbelDocument;
					this.parent = null;
				} else {
					throw new ArgumentException("Document is null and \"this\" isn't an XbelDocument.");
				}
			}
		}
		
		#endregion

		#region Load & Save

		/// <summary>
		/// Load the item from an <see cref="XmlNode"/>.
		/// </summary>
		public abstract void LoadFromXmlNode(XmlNode node);
		
		/// <summary>
		/// Save the item to an <see cref="XmlNode"/>.
		/// </summary>
		public abstract void SaveToXmlNode(XmlNode node);
		
		#endregion
	}
}
