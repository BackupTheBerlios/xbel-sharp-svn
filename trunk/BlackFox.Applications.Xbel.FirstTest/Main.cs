using System;
using System.IO;
using System.Xml;
using System.Collections;
using BlackFox.Classes.Xbel;

namespace BlackFox.Applications.XbelFirstTest
{
	/* TODO :
	 * Séparer le cas ou il n'y as aucune id et id="" ou alors ne pas le séparer
	 *   ->  pour les <desc/> mais il faut une gestion unifiée.
	 * XbelIdentifiedItem.StoreDescAsCDATA ?
	 * 
	 * DONE :
	 * XbelDocment -> Décendant de XbelFolder
	 * Implémenter <info/> et <metadata/>
	 * Utiliser un cache pour XbelDocument.IdTable.
	 * Crer une NUnit pour tout ce bordel.
	 * 
	 * ABANDONED :
	 * En Parallèle à XmlDocument, utiliser XmlReader / XmlWriter.
	 *   ->  A voir, car bon pour gérer <metadata/> il serait pratique de garder 
	 *   ->  les XmlNode(s)
	 */
	class MainClass
	{
		private const string logo = "Xbel# Test program\nUsage: XbelFirstTest.exe FileName";

		public static string GetFileNameFromArgs(string[] args)
		{
			string FileName = null;
			if (args.Length > 0) {
				if (File.Exists(args[0])) {
					FileName = args[0];
				} else {
					Console.WriteLine(logo);
					Console.WriteLine("File Not found : \"{0}\"", args[0]);
				}
			} else {
				Console.WriteLine(logo);
			}
			return FileName;
		}

		public static void Main(string[] args)
		{
			string FileName;
			
			FileName = GetFileNameFromArgs(args);
			if (FileName == null) {
				return;
			}
			
			XmlDocument xmldoc = new XmlDocument();
			xmldoc.Load(FileName);

			XbelDocument doc = new XbelDocument(xmldoc);
			Console.WriteLine(doc.ToString());

			Hashtable idtable = doc.IdTable;
			//Console.WriteLine((idtable["xslt"] as XbelBookmark).Href);

			/*************************************************/
			XmlDocument xmldoc2 = new XmlDocument();
			doc.SaveToXmlDocument(xmldoc2);
			MemoryStream st = new  MemoryStream();
			xmldoc2.Save(st);
			st.Position = 0;
			using (StreamReader sr = new StreamReader(st))
			{
				String line;
				while ((line = sr.ReadLine()) != null)
				{
					Console.WriteLine(line);
				}
			}
		}
	}
}
