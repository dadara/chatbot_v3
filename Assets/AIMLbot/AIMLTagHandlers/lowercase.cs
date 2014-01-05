using AIMLbot;
using AIMLbot.Utils;
using System;
using System.Xml;

namespace AIMLbot.AIMLTagHandlers
{
	public class lowercase : AIMLTagHandler
	{
		public lowercase(Bot bot, User user, SubQuery query, Request request, Result result, XmlNode templateNode) : base(bot, user, query, request, result, templateNode)
		{
		}

		protected override string ProcessChange()
		{
			if (this.templateNode.Name.ToLower() != "lowercase")
			{
				return string.Empty;
			}
			return this.templateNode.InnerText.ToLower(this.bot.Locale);
		}
	}
}