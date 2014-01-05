using AIMLbot;
using AIMLbot.Normalize;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace AIMLbot.Utils
{
	public class SettingsDictionary
	{
		private Dictionary<string, string> settingsHash = new Dictionary<string, string>();

		private List<string> orderedKeys = new List<string>();

		protected Bot bot;

		public int Count
		{
			get
			{
				return this.orderedKeys.Count;
			}
		}

		public XmlDocument DictionaryAsXML
		{
			get
			{
				XmlDocument xmlDocument = new XmlDocument();
				XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", "");
				xmlDocument.AppendChild(xmlDeclaration);
				XmlNode xmlNodes = xmlDocument.CreateNode(XmlNodeType.Element, "root", "");
				xmlDocument.AppendChild(xmlNodes);
				foreach (string orderedKey in this.orderedKeys)
				{
					XmlNode xmlNodes1 = xmlDocument.CreateNode(XmlNodeType.Element, "item", "");
					XmlAttribute xmlAttribute = xmlDocument.CreateAttribute("name");
					xmlAttribute.Value = orderedKey;
					XmlAttribute item = xmlDocument.CreateAttribute("value");
					item.Value = this.settingsHash[orderedKey];
					xmlNodes1.Attributes.Append(xmlAttribute);
					xmlNodes1.Attributes.Append(item);
					xmlNodes.AppendChild(xmlNodes1);
				}
				return xmlDocument;
			}
		}

		public string[] SettingNames
		{
			get
			{
				string[] strArrays = new string[this.orderedKeys.Count];
				this.orderedKeys.CopyTo(strArrays, 0);
				return strArrays;
			}
		}

		public SettingsDictionary(Bot bot)
		{
			this.bot = bot;
		}

		public void addSetting(string name, string value)
		{
			string str = MakeCaseInsensitive.TransformInput(name);
			if (str.Length > 0)
			{
				this.removeSetting(str);
				this.orderedKeys.Add(str);
				this.settingsHash.Add(MakeCaseInsensitive.TransformInput(str), value);
			}
		}

		public void clearSettings()
		{
			this.orderedKeys.Clear();
			this.settingsHash.Clear();
		}

		public void Clone(SettingsDictionary target)
		{
			foreach (string orderedKey in this.orderedKeys)
			{
				target.addSetting(orderedKey, this.grabSetting(orderedKey));
			}
		}

		public bool containsSettingCalled(string name)
		{
			string str = MakeCaseInsensitive.TransformInput(name);
			if (str.Length <= 0)
			{
				return false;
			}
			return this.orderedKeys.Contains(str);
		}

		public string grabSetting(string name)
		{
			string str = MakeCaseInsensitive.TransformInput(name);
			if (!this.containsSettingCalled(str))
			{
				return string.Empty;
			}
			return this.settingsHash[str];
		}

		public void loadSettings(string pathToSettings)
		{
			if (pathToSettings.Length <= 0)
			{
				throw new FileNotFoundException();
			}

			if (!(new FileInfo(pathToSettings)).Exists)
			{
				throw new FileNotFoundException();
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(pathToSettings);
			this.loadSettings(xmlDocument);
		}

		public void loadSettings(XmlDocument settingsAsXML)
		{
			this.clearSettings();
			foreach (XmlNode childNode in settingsAsXML.DocumentElement.ChildNodes)
			{
				if (!((childNode.Name == "item") & childNode.Attributes.Count == 2) || !((childNode.Attributes[0].Name == "name") & (childNode.Attributes[1].Name == "value")))
				{
					continue;
				}
				string value = childNode.Attributes["name"].Value;
				string str = childNode.Attributes["value"].Value;
				if (value.Length <= 0)
				{
					continue;
				}
				this.addSetting(value, str);
			}
		}

		private void removeFromHash(string name)
		{
			string str = MakeCaseInsensitive.TransformInput(name);
			this.settingsHash.Remove(str);
		}

		public void removeSetting(string name)
		{
			string str = MakeCaseInsensitive.TransformInput(name);
			this.orderedKeys.Remove(str);
			this.removeFromHash(str);
		}

		public void updateSetting(string name, string value)
		{
			string str = MakeCaseInsensitive.TransformInput(name);
			if (this.orderedKeys.Contains(str))
			{
				this.removeFromHash(str);
				this.settingsHash.Add(MakeCaseInsensitive.TransformInput(str), value);
			}
		}
	}
}