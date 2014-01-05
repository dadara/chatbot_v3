using AIMLbot;
using AIMLbot.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace AIMLbot.AIMLTagHandlers
{
	public class random : AIMLTagHandler
	{
		public random(Bot bot, User user, SubQuery query, Request request, Result result, XmlNode templateNode) : base(bot, user, query, request, result, templateNode)
		{
			this.isRecursive = false;
		}

		protected override string ProcessChange()
		{
			if (this.templateNode.Name.ToLower() == "random" && this.templateNode.HasChildNodes)
			{
				List<XmlNode> xmlNodes = new List<XmlNode>();
				foreach (XmlNode childNode in this.templateNode.ChildNodes)
				{
					if (childNode.Name != "li")
					{
						continue;
					}
					xmlNodes.Add(childNode);
				}
				if (xmlNodes.Count > 0)
				{
					Random random = new Random();
					return xmlNodes[random.Next(xmlNodes.Count)].InnerXml;
				}
			}
			return string.Empty;
		}
	}
}