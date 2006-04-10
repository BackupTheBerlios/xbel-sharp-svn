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
using System.Text;

namespace BlackFox.Classes.Xbel
{
	/// <summary>
	/// A folder with inside other <see cref="XbelFolder"/>, <see cref="XbelBookmark"/>,
	/// <see cref="XbelAlias"/> and <see cref="XbelSeparator"/> (In fact any <see cref="XbelItem"/> is Ok).
	/// </summary>
	public class XbelFolder : XbelIdentifiedItem, IEnumerable
	{
		#region Properties
		public override string XmlElementName
		{
			get {
				return "folder";
			}
		}

		public XbelBoolean Folded = XbelBoolean.Unknown;
		
		private ArrayList m_items;
		
		public XbelItem this[int index] 
		{
			get 
			{
				return (m_items[index] as XbelItem);
			}

			set 
			{
				m_items[index] = value;
			}
		}	

		public int Count 
		{
			get 
			{
				return m_items.Count;
			}
		}
		
		#endregion
		
		#region Constructors & Init
		
		public XbelFolder(XbelFolder parent) : base(parent)
		{
			Init();
		}
		
		public XbelFolder(XbelDocument doc) : base(doc)
		{
			Init();
		}
		
		private void Init()
		{
			Added = new System.DateTime();
			m_items = new ArrayList();
		}
		
		#endregion
		
		public IEnumerator GetEnumerator()
		{
			return m_items.GetEnumerator();
		}
		
		#region IList-like Methods
		
		public void Add(XbelItem item)
		{
			if (!Contains(item)) {
				m_items.Add(item);
				Document.InvalidateIdTableCache();
			} else {
				throw new InvalidOperationException("Items in a XbelFolder must be unique.");
			}
		}
		
		public void Insert(int index, XbelItem item) 
		{
			m_items.Insert(index, item);
			Document.InvalidateIdTableCache();
		}
		
		public void Remove(XbelItem item) 
		{
			m_items.Remove(item);
			Document.InvalidateIdTableCache();
		}
		
		public void RemoveAt(int index) 
		{
			m_items.RemoveAt(index);
			Document.InvalidateIdTableCache();
		}
		
		public void Sort()
		{
			Sort(true);
		}
		
		public void Sort(bool recursive) 
		{
			m_items.Sort(new XbelItemComparer());
			if (recursive) {
				foreach (XbelItem item in this) {
					if (item is XbelFolder) {
						(item as XbelFolder).Sort(true);
					}
				}
			}
		}
		
		public void Sort(IComparer comparer) 
		{
			m_items.Sort(comparer);
		}
		
		public int IndexOf(XbelItem item) 
		{
			return m_items.IndexOf(item);
		}
		
		public bool Contains(XbelItem item) 
		{
			return m_items.Contains(item);
		}
		
		public void Clear()
		{
			m_items.Clear();
			Document.InvalidateIdTableCache();
		}
		
		#endregion
		
		#region Old and Commented (Useless ???)
		/*
		internal void ResolveAliases(Hashtable IdTable)
		{
			foreach (XbelItem item in this) {
				if (item is XbelAlias) {
					(item as XbelAlias).Resolve(IdTable);
				} else if (item is XbelFolder) {
					(item as XbelFolder).ResolveAliases(IdTable);
				}
			}
		}
		
		internal void CollectIdentified(Hashtable IdTable)
		{
			foreach (XbelItem Item in this) {
				if ((Item is XbelIdentifiedItem) && ((Item as XbelIdentifiedItem).Id != "")) {
					IdTable.Add((Item as XbelIdentifiedItem).Id, Item);
				}
				if (Item is XbelFolder) {
					(Item as XbelFolder).CollectIdentified(IdTable);
				}
			}
		}
		*/
		
		#endregion
		
		#region Load & Save
		
		public override void LoadFromXmlNode(XmlNode node)
		{
			base.LoadFromXmlNode(node);
			
			if (node.Attributes["folded"] != null) {
				Folded = XbelBooleanConvert.FromString(node.Attributes["folded"].Value);
			} else {
				Folded = XbelBoolean.Unknown;
			}
			
			Document.InvalidateIdTableCache();
			LoadChildsFromXmlNode(node);
		}
		
		private void LoadChildsFromXmlNode(XmlNode node)
		{
			Clear();
			foreach(XmlNode ChildNode in node.ChildNodes) {
				XbelItem Item = null;
				
				if (ChildNode.Name == "folder") {
					Item = new XbelFolder(this);
				} else if (ChildNode.Name == "bookmark") {
					Item = new XbelBookmark(this);
				} else if (ChildNode.Name == "alias") {
					Item = new XbelAlias(this);
				} else if (ChildNode.Name == "separator") {
					Item = new XbelSeparator(this);
				}
				
				if (Item != null) {
					Add(Item);
					Item.LoadFromXmlNode(ChildNode);
				}
			}
		}

		public override void SaveToXmlNode(XmlNode node)
		{
			base.SaveToXmlNode(node);
			
			if (Folded != XbelBoolean.Unknown) {
				XmlAttribute a = node.OwnerDocument.CreateAttribute("folded");
				a.Value = XbelBooleanConvert.ToString(Folded);
				node.Attributes.Append(a);
			}
			
			SaveChildsToXmlNode(node);
		}
		
		private void SaveChildsToXmlNode(XmlNode node)
		{
			foreach (XbelItem item in this) {
				XmlElement e = node.OwnerDocument.CreateElement(item.XmlElementName);
				node.AppendChild(e);
				item.SaveToXmlNode(e);
			}
		}
		
		#endregion
		
		#region ToString
		
		/// <summary>
		/// Return a text version of the folder with all his childs nodes.
		/// </summary>
		public override string ToString()
		{
			StringBuilder Ret = new StringBuilder("[" + Id + "] " + Title);
			foreach(XbelItem Item in this) {
				Ret.Append("\n");
				Ret.Append(' ', Item.Level * 2);
				Ret.Append(Item.ToString());
			}
			
			return Ret.ToString();
		}
		#endregion
	}
}
