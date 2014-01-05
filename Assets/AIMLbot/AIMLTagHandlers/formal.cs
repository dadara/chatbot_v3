using AIMLbot;
using AIMLbot.Utils;
using System;
using System.Text;
using System.Xml;

namespace AIMLbot.AIMLTagHandlers
{
	public class formal : AIMLTagHandler
	{
		public formal(Bot bot, User user, SubQuery query, Request request, Result result, XmlNode templateNode) : base(bot, user, query, request, result, templateNode)
		{
		}

		protected override string ProcessChange()
		{
			if (this.templateNode.Name.ToLower() != "formal")
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (this.templateNode.InnerText.Length > 0)
			{
				string[] strArrays = this.templateNode.InnerText.ToLower().Split(new char[0]);
				string[] strArrays1 = strArrays;
				for (int i = 0; i < (int)strArrays1.Length; i++)
				{
					string str = strArrays1[i];
					string upper = str.Substring(0, 1);
					upper = upper.ToUpper();
					if (str.Length > 1)
					{
						upper = string.Concat(upper, str.Substring(1));
					}
					stringBuilder.Append(string.Concat(upper, " "));
				}
			}
			return stringBuilder.ToString().Trim();
		}
	}
}