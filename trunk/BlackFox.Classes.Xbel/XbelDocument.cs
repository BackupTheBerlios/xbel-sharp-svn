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

[assembly:CLSCompliant(true)]

namespace BlackFox.Classes.Xbel
{
	/// <summary>
	/// A Xbel 1.0 document.
	/// </summary>
	public class XbelDocument : XbelFolder
	{
		private string version = "1.0";
		
		/// <summary>
		/// Version of the Xbel Document.
		/// </summary>
		/// <remarks>
		/// This class is made for version "1.0", high probablility of
		/// incompability with all other versions.
		/// </remarks>
		public string Version {
			get {
				return version;
			}
		}
		
		#region Constructors
		
		public XbelDocument() : base(null)
		{
			metadataProcessors = new Hashtable();
		}
		
		public XbelDocument(XmlDocument XmlDoc) : this()
		{
			LoadFromXmlDocument(XmlDoc);
		}
		
		public XbelDocument(string FileName) : this()
		{
			LoadFromFile(FileName);
		}
		
		#endregion
		
		#region Id Table
		
		private bool idTableCacheIsValid = false;
		private Hashtable idTableCache;
		
		/// <summary>
		/// A hashtable with all <see cref="XbelIdentifiedItem"/>s of the Document
		/// with their id as Key.
		/// Internaly used to resolve <see cref="XbelAlias"/> targets.
		/// </summary>
		public Hashtable IdTable
		{
			get {
				if (!idTableCacheIsValid) {
					idTableCache = new Hashtable();
					BuildIdTableForFolder(this, idTableCache);
				}
				return idTableCache;
			}
		}

		private void BuildIdTableForFolder(XbelFolder folder, Hashtable idTable)
		{
			foreach(XbelItem item in folder) {
				if (item is XbelIdentifiedItem) {
					if ((item as XbelIdentifiedItem).Id != "") {
						idTable.Add((item as XbelIdentifiedItem).Id, item);
					}
				}
				if (item is XbelFolder) {
					BuildIdTableForFolder((item as XbelFolder), idTable);
				}
			}
		}

		/// <summary>
		/// Tell the engine that the next call to IdTable should re-generate the table.
		/// </summary>
		/// <remarks>
		/// Called from <see cref="XbelFolder"/> Add, Insert, Remove, ... and other methods.
		/// </remarks>
		/// <returns>
		/// True if the cache was valid (and so this call was usefull), false otherwise.
		/// </returns>
		public bool InvalidateIdTableCache()
		{
			bool oldValue = idTableCacheIsValid;
			if (idTableCacheIsValid) {
				idTableCacheIsValid = false;
				idTableCache = null;
			}
			return oldValue;
		}
		
		#endregion
		
		#region Load & Save
		
		/// <summary>
		/// Save to an XML Document.
		/// </summary>
		public void SaveToFile(string fileName)
		{
			XmlDocument xmlDoc = new XmlDocument();
			SaveToXmlDocument(xmlDoc);
			xmlDoc.Save(fileName);
		}

		/// <summary>
		/// Save all nodes to an XML Document.
		/// </summary>		
		public void SaveToXmlDocument(XmlDocument xmlDoc)
		{
			// Element principal
			XmlElement n = xmlDoc.CreateElement("xbel");
			xmlDoc.AppendChild(n);
			
			// Version
			XmlAttribute a = xmlDoc.CreateAttribute("version");
			a.Value = Version;
			n.Attributes.Append(a);

			// Recursion
			this.SaveToXmlNode(n);
		}
		
		/// <summary>
		/// Load from an XML Document.
		/// </summary>
		public void LoadFromXmlDocument(XmlDocument xmlDoc)
		{
			XmlNode xmlRoot = xmlDoc.DocumentElement;
			
			if (xmlRoot.Attributes["version"] != null)
			{
				version = xmlRoot.Attributes["version"].Value;
			}
			
			//Root = new XbelFolder(this);
			this.LoadFromXmlNode(xmlRoot);	
		}
		
		/// <summary>
		/// Load from a Xbel XML file.
		/// </summary>
		public void LoadFromFile(string fileName)
		{
			XmlDocument XmlDoc = new XmlDocument();
			XmlDoc.Load(fileName);
			
			LoadFromXmlDocument(XmlDoc);
		}
		
		#endregion
		
		#region Metadata Processors
		
		private Hashtable metadataProcessors;
		
		public void AddMetadataProcessor(Type metadataProcessor)
		{
			if (metadataProcessor.BaseType == typeof(XbelMetadata)) {
				string owner = GetOwnerForMetadataProcessor(metadataProcessor);
				if (!metadataProcessors.Contains(owner)) {
					metadataProcessors.Add(owner, metadataProcessor);
				} else {
					// Error
				}
			} else {
				// Error
			}
		}
		
		public void RemoveMetadataProcessor(string owner)
		{
			metadataProcessors.Remove(owner);
		}
		
		public void RemoveMetadataProcessor(Type metadataProcessor)
		{
			RemoveMetadataProcessor(GetOwnerForMetadataProcessor(metadataProcessor));
		}
		
		private string GetOwnerForMetadataProcessor(Type metadataProcessor)
		{
			// Use invoke and reflexion to find the value of the Owner property
			return metadataProcessor.GetProperty("Owner").GetGetMethod().Invoke(null, null) as string;
		}
		
		/// <param name="Owner">The searched owner.</param>
		/// <returns>The Processor type if found, typeof(<see cref="XbelRawMetadata"/>) otherwise.</returns>
		public Type FindMetadataProcessor(string owner)
		{
			Type t;
			t = metadataProcessors[owner] as Type;
			if (t == null) {
				t = typeof(XbelRawMetadata);
			}
			return t;
		}
		
		#endregion
		
		/// <returns>A text version of the document with all his nodes.</returns>
		public override string ToString()
		{
			return "Xbel Document version " + Version + "\n" + base.ToString();
		}
		
		#region Alias Cleaning
		
		private void CleanAliases(XbelFolder folder)
		{
			foreach (XbelItem item in folder) {
				if (item is XbelAlias) {
					XbelAlias alias = item as XbelAlias;
					if ((alias.IdRef == "") || (Document.IdTable[alias.IdRef] == null)) {
						folder.Remove(item);
					}
				} else if (item is XbelFolder) {
					CleanAliases(item as XbelFolder);
				}
			}
		}
		
		public void CleanAliases()
		{
			CleanAliases(this);
		}
		
		#endregion
	}
}
