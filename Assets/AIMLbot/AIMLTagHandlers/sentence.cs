using AIMLbot;
using AIMLbot.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace AIMLbot.AIMLTagHandlers
{
	public class sentence : AIMLTagHandler
	{
		public sentence(Bot bot, User user, SubQuery query, Request request, Result result, XmlNode templateNode) : base(bot, user, query, request, result, templateNode)
		{
		}

		protected override string ProcessChange()
		{
			if (this.templateNode.Name.ToLower() != "sentence")
			{
				return string.Empty;
			}
			if (this.templateNode.InnerText.Length <= 0)
			{
				XmlNode node = AIMLTagHandler.getNode("<star/>");
				star _star = new star(this.bot, this.user, this.query, this.request, this.result, node);
				this.templateNode.InnerText = _star.Transform();
				if (this.templateNode.InnerText.Length <= 0)
				{
					return string.Empty;
				}
				return this.ProcessChange();
			}
			StringBuilder stringBuilder = new StringBuilder();
			char[] charArray = this.templateNode.InnerText.Trim().ToCharArray();
			bool flag = true;
			for (int i = 0; i < (int)charArray.Length; i++)
			{
				string str = Convert.ToString(charArray[i]);
				if (this.bot.Splitters.Contains(str))
				{
					flag = true;
				}
				if (!(new Regex("[a-zA-Z]")).IsMatch(str))
				{
					stringBuilder.Append(str);
				}
				else if (!flag)
				{
					stringBuilder.Append(str.ToLower(this.bot.Locale));
				}
				else
				{
					stringBuilder.Append(str.ToUpper(this.bot.Locale));
					flag = false;
				}
			}
			return stringBuilder.ToString();
		}
	}
}