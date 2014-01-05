using AIMLbot;
using AIMLbot.Utils;
using System;
using System.Xml;

namespace AIMLbot.AIMLTagHandlers
{
	public class version : AIMLTagHandler
	{
		public version(Bot bot, User user, SubQuery query, Request request, Result result, XmlNode templateNode) : base(bot, user, query, request, result, templateNode)
		{
		}

		protected override string ProcessChange()
		{
			if (this.templateNode.Name.ToLower() != "version")
			{
				return string.Empty;
			}
			return this.bot.GlobalSettings.grabSetting("version");
		}
	}
}