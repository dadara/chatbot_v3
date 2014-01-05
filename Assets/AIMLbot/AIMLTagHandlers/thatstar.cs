using AIMLbot;
using AIMLbot.Utils;
using System;
using System.Collections.Generic;
using System.Xml;

namespace AIMLbot.AIMLTagHandlers
{
	public class thatstar : AIMLTagHandler
	{
		public thatstar(Bot bot, User user, SubQuery query, Request request, Result result, XmlNode templateNode) : base(bot, user, query, request, result, templateNode)
		{
		}

		protected override string ProcessChange()
		{
			string item;
			if (this.templateNode.Name.ToLower() == "thatstar")
			{
				if (this.templateNode.Attributes.Count == 0)
				{
					if (this.query.ThatStar.Count > 0)
					{
						return this.query.ThatStar[0];
					}
					this.bot.writeToLog(string.Concat("ERROR! An out of bounds index to thatstar was encountered when processing the input: ", this.request.rawInput));
				}
				else if (this.templateNode.Attributes.Count == 1 && this.templateNode.Attributes[0].Name.ToLower() == "index" && this.templateNode.Attributes[0].Value.Length > 0)
				{
					try
					{
						int num = Convert.ToInt32(this.templateNode.Attributes[0].Value.Trim());
						if (this.query.ThatStar.Count <= 0)
						{
							this.bot.writeToLog(string.Concat("ERROR! An out of bounds index to thatstar was encountered when processing the input: ", this.request.rawInput));
						}
						else if (num <= 0)
						{
							this.bot.writeToLog(string.Concat("ERROR! An input tag with a bady formed index (", this.templateNode.Attributes[0].Value, ") was encountered processing the input: ", this.request.rawInput));
						}
						else
						{
							item = this.query.ThatStar[num - 1];
							return item;
						}
						return string.Empty;
					}
					catch
					{
						this.bot.writeToLog(string.Concat("ERROR! A thatstar tag with a bady formed index (", this.templateNode.Attributes[0].Value, ") was encountered processing the input: ", this.request.rawInput));
						return string.Empty;
					}
					return item;
				}
			}
			return string.Empty;
		}
	}
}