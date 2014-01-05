using AIMLbot;
using AIMLbot.Utils;
using System;
using System.Text.RegularExpressions;

namespace AIMLbot.Normalize
{
	public class StripIllegalCharacters : TextTransformer
	{
		public StripIllegalCharacters(Bot bot, string inputString) : base(bot, inputString)
		{
		}

		public StripIllegalCharacters(Bot bot) : base(bot)
		{
		}

		protected override string ProcessChange()
		{
			return this.bot.Strippers.Replace(this.inputString, " ");
		}
	}
}