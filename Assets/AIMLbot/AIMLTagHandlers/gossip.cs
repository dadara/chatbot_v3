using AIMLbot;
using AIMLbot.Utils;
using System;
using System.Xml;

namespace AIMLbot.AIMLTagHandlers
{
	public class gossip : AIMLTagHandler
	{
		public gossip(Bot bot, User user, SubQuery query, Request request, Result result, XmlNode templateNode) : base(bot, user, query, request, result, templateNode)
		{
		}

		protected override string ProcessChange()
		{
			if (this.templateNode.Name.ToLower() == "gossip" && this.templateNode.InnerText.Length > 0)
			{
				Bot bot = this.bot;
				string[] userID = new string[] { "GOSSIP from user: ", this.user.UserID, ", '", this.templateNode.InnerText, "'" };
				bot.writeToLog(string.Concat(userID));
			}
			return string.Empty;
		}
	}
}