using AIMLbot;
using AIMLbot.Utils;
using System;
using System.Xml;

namespace AIMLbot.AIMLTagHandlers
{
	public class get : AIMLTagHandler
	{
		public get(Bot bot, User user, SubQuery query, Request request, Result result, XmlNode templateNode) : base(bot, user, query, request, result, templateNode)
		{
		}

		protected override string ProcessChange()
		{
			if (!(this.templateNode.Name.ToLower() == "get") || this.bot.GlobalSettings.Count <= 0 || this.templateNode.Attributes.Count != 1 || !(this.templateNode.Attributes[0].Name.ToLower() == "name"))
			{
				return string.Empty;
			}
			return this.user.Predicates.grabSetting(this.templateNode.Attributes[0].Value);
		}
	}
}