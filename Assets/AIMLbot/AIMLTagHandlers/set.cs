using AIMLbot;
using AIMLbot.Utils;
using System;
using System.Xml;

namespace AIMLbot.AIMLTagHandlers
{
	public class set : AIMLTagHandler
	{
		public set(Bot bot, User user, SubQuery query, Request request, Result result, XmlNode templateNode) : base(bot, user, query, request, result, templateNode)
		{
		}

		protected override string ProcessChange()
		{
			if (!(this.templateNode.Name.ToLower() == "set") || this.bot.GlobalSettings.Count <= 0 || this.templateNode.Attributes.Count != 1 || !(this.templateNode.Attributes[0].Name.ToLower() == "name"))
			{
				return string.Empty;
			}
			if (this.templateNode.InnerText.Length <= 0)
			{
				this.user.Predicates.removeSetting(this.templateNode.Attributes[0].Value);
				return string.Empty;
			}
			this.user.Predicates.addSetting(this.templateNode.Attributes[0].Value, this.templateNode.InnerText);
			return this.user.Predicates.grabSetting(this.templateNode.Attributes[0].Value);
		}
	}
}