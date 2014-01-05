using AIMLbot;
using AIMLbot.Utils;
using System;
using System.Xml;

namespace AIMLbot.AIMLTagHandlers
{
	public class srai : AIMLTagHandler
	{
		public srai(Bot bot, User user, SubQuery query, Request request, Result result, XmlNode templateNode) : base(bot, user, query, request, result, templateNode)
		{
		}

		protected override string ProcessChange()
		{
			if (!(this.templateNode.Name.ToLower() == "srai") || this.templateNode.InnerText.Length <= 0)
			{
				return string.Empty;
			}
			Request request = new Request(this.templateNode.InnerText, this.user, this.bot)
			{
				StartedOn = this.request.StartedOn
			};
			Result result = this.bot.Chat(request);
			this.request.hasTimedOut = request.hasTimedOut;
			return result.Output;
		}
	}
}