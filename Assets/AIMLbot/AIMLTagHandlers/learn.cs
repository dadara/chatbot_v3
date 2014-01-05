using AIMLbot;
using AIMLbot.Utils;
using System;
using System.IO;
using System.Xml;

namespace AIMLbot.AIMLTagHandlers
{
	public class learn : AIMLTagHandler
	{
		public learn(Bot bot, User user, SubQuery query, Request request, Result result, XmlNode templateNode) : base(bot, user, query, request, result, templateNode)
		{
		}

		protected override string ProcessChange()
		{
			if (this.templateNode.Name.ToLower() == "learn" && this.templateNode.InnerText.Length > 0)
			{
				string innerText = this.templateNode.InnerText;
				if ((new FileInfo(innerText)).Exists)
				{
					XmlDocument xmlDocument = new XmlDocument();
					try
					{
						xmlDocument.Load(innerText);
						this.bot.loadAIMLFromXML(xmlDocument, innerText);
					}
					catch
					{
						this.bot.writeToLog(string.Concat("ERROR! Attempted (but failed) to <learn> some new AIML from the following URI: ", innerText));
					}
				}
			}
			return string.Empty;
		}
	}
}