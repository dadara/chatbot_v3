using AIMLbot;
using AIMLbot.Utils;
using System;
using System.Xml;

namespace AIMLbot.AIMLTagHandlers
{
	public class bot : AIMLTagHandler
	{
		public bot(Bot bot, User user, SubQuery query, Request request, Result result, XmlNode templateNode) : base(bot, user, query, request, result, templateNode)
		{
		}

		protected override string ProcessChange()
		{
			if (!(this.templateNode.Name.ToLower() == "bot") || this.templateNode.Attributes.Count != 1 || !(this.templateNode.Attributes[0].Name.ToLower() == "name"))
			{
				return string.Empty;
			}
			string value = this.templateNode.Attributes["name"].Value;
			return this.bot.GlobalSettings.grabSetting(value);
		}
	}
}