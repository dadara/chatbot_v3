using AIMLbot;
using AIMLbot.Utils;
using System;
using System.Collections.Generic;
using System.Xml;

namespace AIMLbot.AIMLTagHandlers
{
	public class star : AIMLTagHandler
	{
		public star(Bot bot, User user, SubQuery query, Request request, Result result, XmlNode templateNode) : base(bot, user, query, request, result, templateNode)
		{
		}

		protected override string ProcessChange()
		{
			string item;
			if (this.templateNode.Name.ToLower() == "star")
			{
				if (this.query.InputStar.Count <= 0)
				{
					this.bot.writeToLog(string.Concat("A star tag tried to reference an empty InputStar collection when processing the input: ", this.request.rawInput));
				}
				else
				{
					if (this.templateNode.Attributes.Count == 0)
					{
						return this.query.InputStar[0];
					}
					if (this.templateNode.Attributes.Count == 1 && this.templateNode.Attributes[0].Name.ToLower() == "index")
					{
						try
						{
							int num = Convert.ToInt32(this.templateNode.Attributes[0].Value);
							num--;
							if (!(num >= 0 & num < this.query.InputStar.Count))
							{
								this.bot.writeToLog(string.Concat("InputStar out of bounds reference caused by input: ", this.request.rawInput));
								return string.Empty;
							}
							else
							{
								item = this.query.InputStar[num];
							}
						}
						catch
						{
							this.bot.writeToLog(string.Concat("Index set to non-integer value whilst processing star tag in response to the input: ", this.request.rawInput));
							return string.Empty;
						}
						return item;
					}
				}
			}
			return string.Empty;
		}
	}
}