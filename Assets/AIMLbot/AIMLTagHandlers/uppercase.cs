using AIMLbot;
using AIMLbot.Utils;
using System;
using System.Xml;

namespace AIMLbot.AIMLTagHandlers
{
	public class uppercase : AIMLTagHandler
	{
		public uppercase(Bot bot, User user, SubQuery query, Request request, Result result, XmlNode templateNode) : base(bot, user, query, request, result, templateNode)
		{
		}

		protected override string ProcessChange()
		{
			if (this.templateNode.Name.ToLower() != "uppercase")
			{
				return string.Empty;
			}
			return this.templateNode.InnerText.ToUpper(this.bot.Locale);
		}
	}
}