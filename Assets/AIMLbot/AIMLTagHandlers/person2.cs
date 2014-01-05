using AIMLbot;
using AIMLbot.Normalize;
using AIMLbot.Utils;
using System;
using System.Xml;

namespace AIMLbot.AIMLTagHandlers
{
	public class person2 : AIMLTagHandler
	{
		public person2(Bot bot, User user, SubQuery query, Request request, Result result, XmlNode templateNode) : base(bot, user, query, request, result, templateNode)
		{
		}

		protected override string ProcessChange()
		{
			if (this.templateNode.Name.ToLower() != "person2")
			{
				return string.Empty;
			}
			if (this.templateNode.InnerText.Length > 0)
			{
				return ApplySubstitutions.Substitute(this.bot, this.bot.Person2Substitutions, this.templateNode.InnerText);
			}
			XmlNode node = AIMLTagHandler.getNode("<star/>");
			star _star = new star(this.bot, this.user, this.query, this.request, this.result, node);
			this.templateNode.InnerText = _star.Transform();
			if (this.templateNode.InnerText.Length <= 0)
			{
				return string.Empty;
			}
			return this.ProcessChange();
		}
	}
}