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
	/// An <see cref="XbelItem"/> identified with an Title and an Id, who could be refered
	/// by an <see cref="XbelAlias"/>.
	/// Identified Items are also the hosts for &lt;info/&gt; and &lt;metadata&gt;.
	/// </summary>
	/// <remarks>
	/// Corresponding items in XML : &lt;xbel&gt;, &lt;folder&gt;, &lt;bookmark&gt;
	/// </remarks>
	public abstract class XbelIdentifiedItem : XbelItem
	{
		private string id = "";
		private string title = "";
		private string desc = null;
		
		private XbelMetadataCollection infos;
		/// <summary>
		/// Host &lt;metadata&gt;s lments.
		/// </summary>
		public XbelMetadataCollection Infos {
			get {
				return infos;
			}
		}
		
		private DateTime added; // TODO: Need a way to be nullable (Ahhh... C# 2.0 "private DateTime? added;")
		
		public DateTime Added {
			get {
				return added;
			}
			set {
				added = value;
			}
		}
		
		
		/// <summary>
		/// Unique identifier of the item.
		/// </summary>
		public string Id {
			get {
				return id;
			}
			set {
				id = value;
				Document.InvalidateIdTableCache();
			}
		}
		
		/// <summary>
		/// Title of the item.
		/// </summary>
		public string Title {
			get {
				return title;
			}
			set {
				title = value;
			}
		}
		
		public string Desc {
			get {
				return desc;
			}
			set {
				desc = value;
			}
		}
		
		#region Constructors
		
		protected XbelIdentifiedItem(XbelFolder parent) : base(parent)
		{
			infos = new XbelMetadataCollection();
		}
		
		protected XbelIdentifiedItem(XbelDocument doc) : base(doc)
		{
			infos = new XbelMetadataCollection();
		}
		
		#endregion
		
		#region Load & Save
		
		private void SaveInfos(XmlElement element)
		{
			foreach (XbelMetadata metadata in infos) {
				// Obtention des donnés
				XmlDocumentFragment df = element.OwnerDocument.CreateDocumentFragment();
				metadata.SaveToXml(df);
				
				//Vérification
				if ( (df.ChildNodes.Count > 1) || !(df.ChildNodes[0] is XmlElement) || (df.ChildNodes[0].Name != "metadata") ) {
					throw InvalidProcessorXmlOutputException.Create(metadata, df.OuterXml);
				}
				
				// Ajout au document
				element.AppendChild(df);
			}
		}
		
		private void LoadInfos(XmlElement element)
		{
			Infos.Clear();
			if (element != null) {
				foreach (XmlElement ChildNode in element.ChildNodes) {
					string owner = ChildNode.Attributes["owner"].Value;
					if (ChildNode.Name == "metadata" && owner != null) {
						Type t = Document.FindMetadataProcessor(owner);
						XbelMetadata processor = Activator.CreateInstance(t) as XbelMetadata;
						processor.LoadFromXml(ChildNode);
						infos.Add(processor);
					}
				}
			}
		}
		
		public override void LoadFromXmlNode(XmlNode node)
		{
			
			if (node.Attributes["id"] != null) {
				id = node.Attributes["id"].Value;
			} else {
				id = "";
			}
			if (node["title"] != null) {
				title = node["title"].InnerText;
			} else {
				title = "";
			}
			if (node["desc"] != null) {
				desc = node["desc"].InnerText;
			} else {
				desc = null;
			}
			if 
			LoadInfos(node["info"]);
		}
		
		public override void SaveToXmlNode(XmlNode node)
		{
			/*
			 * Les id ne sont retranscrits que si ils ne sont pas une chaine vide
			 */
			if (id != "") {
				XmlAttribute idAttribute = node.OwnerDocument.CreateAttribute("id");
				idAttribute.Value = id;
				node.Attributes.Append(idAttribute);
			}
			
			/*
			 * Les descriptions ne sont retranscrites que si elles existaient et se résumaient
			 * à plus que des espaces ou autres chaines innutiles.
			 */
			if (desc != null && desc.Trim() != "") {
				XmlElement descNode = node.OwnerDocument.CreateElement("desc");
				//desc.AppendChild(Node.OwnerDocument.CreateCDataSection(Desc));
				descNode.InnerText = desc;
				node.AppendChild(descNode);				
			}
			
			/*
			 * Si il existe des Metadata (qui se situent dans une node <info>) on les sauve.
			 */
			 if (Infos.Count > 0) {
			 	XmlElement infoNode = node.OwnerDocument.CreateElement("info");
			 	SaveInfos(infoNode);
			 	node.AppendChild(infoNode);
			 }
			 
			XmlElement e = node.OwnerDocument.CreateElement("title");
			e.InnerText = Title;
			node.AppendChild(e);			
		}
		
		#endregion
		
	}
}
