using AIMLbot;
using AIMLbot.Utils;
using System;
using System.Xml;

namespace AIMLbot.AIMLTagHandlers
{
	public class sr : AIMLTagHandler
	{
		public sr(Bot bot, User user, SubQuery query, Request request, Result result, XmlNode templateNode) : base(bot, user, query, request, result, templateNode)
		{
		}

		protected override string ProcessChange()
		{
			if (this.templateNode.Name.ToLower() != "sr")
			{
				return string.Empty;
			}
			XmlNode node = AIMLTagHandler.getNode("<star/>");
			star _star = new star(this.bot, this.user, this.query, this.request, this.result, node);
			string str = _star.Transform();
			XmlNode xmlNodes = AIMLTagHandler.getNode(string.Concat("<srai>", str, "</srai>"));
			srai _srai = new srai(this.bot, this.user, this.query, this.request, this.result, xmlNodes);
			return _srai.Transform();
		}
	}
}