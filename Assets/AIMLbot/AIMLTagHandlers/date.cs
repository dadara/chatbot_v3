using AIMLbot;
using AIMLbot.Utils;
using System;
using System.Xml;

namespace AIMLbot.AIMLTagHandlers
{
	public class date : AIMLTagHandler
	{
		public date(Bot bot, User user, SubQuery query, Request request, Result result, XmlNode templateNode) : base(bot, user, query, request, result, templateNode)
		{
		}

		protected override string ProcessChange()
		{
			if (this.templateNode.Name.ToLower() != "date")
			{
				return string.Empty;
			}
			return DateTime.Now.ToString(this.bot.Locale);
		}
	}
}