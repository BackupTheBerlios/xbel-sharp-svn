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
	public abstract class XbelMetadata
	{
		public static string Owner {
			get
			{
				throw new InvalidOperationException("Static method Owner.get can't be called directly in XbelMetadata.");
			}
		}
		public abstract void LoadFromXml(XmlElement element);
			
		public abstract void SaveToXml(XmlDocumentFragment fragment);
	}
	
	public class InvalidProcessorXmlOutputException: ApplicationException
	{
		public InvalidProcessorXmlOutputException(string message) : base(message)
		{
		}
		
		public static InvalidProcessorXmlOutputException Create(XbelMetadata processor, string output)
		{
			string msg = "Metadata Processor : \"" + processor.GetType().ToString() +
			             "\" saved invalid data :\n" + output;
			return new InvalidProcessorXmlOutputException(msg);
		}
	}
}
