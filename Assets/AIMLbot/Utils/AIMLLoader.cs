using AIMLbot;
using AIMLbot.Normalize;
using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;

namespace AIMLbot.Utils
{
	public class AIMLLoader
	{
		private Bot bot;

		public AIMLLoader(Bot bot)
		{
			this.bot = bot;
		}

		private XmlNode FindNode(string name, XmlNode node)
		{
			XmlNode xmlNodes;
			IEnumerator enumerator = node.ChildNodes.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					XmlNode current = (XmlNode)enumerator.Current;
					if (current.Name != name)
					{
						continue;
					}
					xmlNodes = current;
					return xmlNodes;
				}
				return null;
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return xmlNodes;
		}

		public string generatePath(XmlNode node, string topicName, bool isUserInput)
		{
			string str;
			XmlNode xmlNodes = this.FindNode("pattern", node);
			XmlNode xmlNodes1 = this.FindNode("that", node);
			string innerText = "*";
			str = (!object.Equals(null, xmlNodes) ? xmlNodes.InnerText : string.Empty);
			if (!object.Equals(null, xmlNodes1))
			{
				innerText = xmlNodes1.InnerText;
			}
			return this.generatePath(str, innerText, topicName, isUserInput);
		}

		public string generatePath(string pattern, string that, string topicName, bool isUserInput)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string empty = string.Empty;
			string str = "*";
			string str1 = "*";
			if (!(this.bot.TrustAIML & !isUserInput))
			{
				empty = this.Normalize(pattern, isUserInput).Trim();
				str = this.Normalize(that, isUserInput).Trim();
				str1 = this.Normalize(topicName, isUserInput).Trim();
			}
			else
			{
				empty = pattern.Trim();
				str = that.Trim();
				str1 = topicName.Trim();
			}
			if (empty.Length <= 0)
			{
				return string.Empty;
			}
			if (str.Length == 0)
			{
				str = "*";
			}
			if (str1.Length == 0)
			{
				str1 = "*";
			}
			if (str.Length > this.bot.MaxThatSize)
			{
				str = "*";
			}
			stringBuilder.Append(empty);
			stringBuilder.Append(" <that> ");
			stringBuilder.Append(str);
			stringBuilder.Append(" <topic> ");
			stringBuilder.Append(str1);
			return stringBuilder.ToString();
		}

		public void loadAIML()
		{
			this.loadAIML(this.bot.PathToAIML);
		}

		public void loadAIML(string path)
		{
			if (!Directory.Exists(path))
			{
				throw new FileNotFoundException(string.Concat("The directory specified as the path to the AIML files (", path, ") cannot be found by the AIMLLoader object. Please make sure the directory where you think the AIML files are to be found is the same as the directory specified in the settings file."));
			}
			this.bot.writeToLog(string.Concat("Starting to process AIML files found in the directory ", path));
			string[] files = Directory.GetFiles(path, "*.aiml");
			if ((int)files.Length <= 0)
			{
				throw new FileNotFoundException(string.Concat("Could not find any .aiml files in the specified directory (", path, "). Please make sure that your aiml file end in a lowercase aiml extension, for example - myFile.aiml is valid but myFile.AIML is not."));
			}
			string[] strArrays = files;
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				this.loadAIMLFile(strArrays[i]);
			}
			this.bot.writeToLog(string.Concat("Finished processing the AIML files. ", Convert.ToString(this.bot.Size), " categories processed."));
		}

		public void loadAIMLFile(string filename)
		{
			this.bot.writeToLog(string.Concat("Processing AIML file: ", filename));
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(filename);
			this.loadAIMLFromXML(xmlDocument, filename);
		}

		public void loadAIMLFromXML(XmlDocument doc, string filename)
		{
			foreach (XmlNode childNode in doc.DocumentElement.ChildNodes)
			{
				if (childNode.Name != "topic")
				{
					if (childNode.Name != "category")
					{
						continue;
					}
					this.processCategory(childNode, filename);
				}
				else
				{
					this.processTopic(childNode, filename);
				}
			}
		}

		public string Normalize(string input, bool isUserInput)
		{
			string str;
			StringBuilder stringBuilder = new StringBuilder();
			ApplySubstitutions applySubstitution = new ApplySubstitutions(this.bot);
			StripIllegalCharacters stripIllegalCharacter = new StripIllegalCharacters(this.bot);
			string str1 = applySubstitution.Transform(input);
			string[] strArrays = str1.Split(" \r\n\t".ToCharArray());
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				string str2 = strArrays[i];
				if (!isUserInput)
				{
					str = (str2 == "*" || str2 == "_" ? str2 : stripIllegalCharacter.Transform(str2));
				}
				else
				{
					str = stripIllegalCharacter.Transform(str2);
				}
				stringBuilder.Append(string.Concat(str.Trim(), " "));
			}
			return stringBuilder.ToString().Replace("  ", " ");
		}

		private void processCategory(XmlNode node, string filename)
		{
			this.processCategory(node, "*", filename);
		}

		private void processCategory(XmlNode node, string topicName, string filename)
		{
			XmlNode xmlNodes = this.FindNode("pattern", node);
			XmlNode xmlNodes1 = this.FindNode("template", node);
			if (object.Equals(null, xmlNodes))
			{
				throw new XmlException(string.Concat("Missing pattern tag in a node found in ", filename));
			}
			if (object.Equals(null, xmlNodes1))
			{
				throw new XmlException(string.Concat("Missing template tag in the node with pattern: ", xmlNodes.InnerText, " found in ", filename));
			}
			string str = this.generatePath(node, topicName, false);
			if (str.Length <= 0)
			{
				Bot bot = this.bot;
				string[] outerXml = new string[] { "WARNING! Attempted to load a new category with an empty pattern where the path = ", str, " and template = ", xmlNodes1.OuterXml, " produced by a category in the file: ", filename };
				bot.writeToLog(string.Concat(outerXml));
			}
			else
			{
				try
				{
					this.bot.Graphmaster.addCategory(str, xmlNodes1.OuterXml, filename);
					Bot size = this.bot;
					size.Size = size.Size + 1;
				}
				catch
				{
					Bot bot1 = this.bot;
					string[] strArrays = new string[] { "ERROR! Failed to load a new category into the graphmaster where the path = ", str, " and template = ", xmlNodes1.OuterXml, " produced by a category in the file: ", filename };
					bot1.writeToLog(string.Concat(strArrays));
				}
			}
		}

		private void processTopic(XmlNode node, string filename)
		{
			string value = "*";
			if (node.Attributes.Count == 1 & (node.Attributes[0].Name == "name"))
			{
				value = node.Attributes["name"].Value;
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.Name != "category")
				{
					continue;
				}
				this.processCategory(childNode, value, filename);
			}
		}
	}
}