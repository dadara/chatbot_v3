using AIMLbot;
using AIMLbot.Utils;
using System;
using System.Xml;

namespace AIMLbot.AIMLTagHandlers
{
	public class input : AIMLTagHandler
	{
		public input(Bot bot, User user, SubQuery query, Request request, Result result, XmlNode templateNode) : base(bot, user, query, request, result, templateNode)
		{
		}

		protected override string ProcessChange()
		{
			string resultSentence;
			if (this.templateNode.Name.ToLower() == "input")
			{
				if (this.templateNode.Attributes.Count == 0)
				{
					return this.user.getResultSentence();
				}
				if (this.templateNode.Attributes.Count == 1 && this.templateNode.Attributes[0].Name.ToLower() == "index" && this.templateNode.Attributes[0].Value.Length > 0)
				{
					try
					{
						string[] strArrays = this.templateNode.Attributes[0].Value.Split(",".ToCharArray());
						if ((int)strArrays.Length != 2)
						{
							int num = Convert.ToInt32(this.templateNode.Attributes[0].Value.Trim());
							if (num <= 0)
							{
								this.bot.writeToLog(string.Concat("ERROR! An input tag with a bady formed index (", this.templateNode.Attributes[0].Value, ") was encountered processing the input: ", this.request.rawInput));
							}
							else
							{
								resultSentence = this.user.getResultSentence(num - 1);
								return resultSentence;
							}
						}
						else
						{
							int num1 = Convert.ToInt32(strArrays[0].Trim());
							int num2 = Convert.ToInt32(strArrays[1].Trim());
							if (!(num1 > 0 & num2 > 0))
							{
								this.bot.writeToLog(string.Concat("ERROR! An input tag with a bady formed index (", this.templateNode.Attributes[0].Value, ") was encountered processing the input: ", this.request.rawInput));
							}
							else
							{
								resultSentence = this.user.getResultSentence(num1 - 1, num2 - 1);
								return resultSentence;
							}
						}
						return string.Empty;
					}
					catch
					{
						this.bot.writeToLog(string.Concat("ERROR! An input tag with a bady formed index (", this.templateNode.Attributes[0].Value, ") was encountered processing the input: ", this.request.rawInput));
						return string.Empty;
					}
					return resultSentence;
				}
			}
			return string.Empty;
		}
	}
}