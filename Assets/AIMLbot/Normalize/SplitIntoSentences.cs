using AIMLbot;
using System;
using System.Collections.Generic;

namespace AIMLbot.Normalize
{
	public class SplitIntoSentences
	{
		private Bot bot;

		private string inputString;

		public SplitIntoSentences(Bot bot, string inputString)
		{
			this.bot = bot;
			this.inputString = inputString;
		}

		public SplitIntoSentences(Bot bot)
		{
			this.bot = bot;
		}

		public string[] Transform(string inputString)
		{
			this.inputString = inputString;
			return this.Transform();
		}

		public string[] Transform()
		{
			string[] array = this.bot.Splitters.ToArray();
			string[] strArrays = this.inputString.Split(array, StringSplitOptions.RemoveEmptyEntries);
			List<string> strs = new List<string>();
			string[] strArrays1 = strArrays;
			for (int i = 0; i < (int)strArrays1.Length; i++)
			{
				string str = strArrays1[i].Trim();
				if (str.Length > 0)
				{
					strs.Add(str);
				}
			}
			return strs.ToArray();
		}
	}
}