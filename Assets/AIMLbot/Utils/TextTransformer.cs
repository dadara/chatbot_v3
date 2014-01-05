using AIMLbot;
using System;

namespace AIMLbot.Utils
{
	public abstract class TextTransformer
	{
		protected string inputString;

		public Bot bot;

		public string InputString
		{
			get
			{
				return this.inputString;
			}
			set
			{
				this.inputString = value;
			}
		}

		public string OutputString
		{
			get
			{
				return this.Transform();
			}
		}

		public TextTransformer(Bot bot, string inputString)
		{
			this.bot = bot;
			this.inputString = inputString;
		}

		public TextTransformer(Bot bot)
		{
			this.bot = bot;
			this.inputString = string.Empty;
		}

		public TextTransformer()
		{
			this.bot = null;
			this.inputString = string.Empty;
		}

		protected abstract string ProcessChange();

		public string Transform(string input)
		{
			this.inputString = input;
			return this.Transform();
		}

		public string Transform()
		{
			if (this.inputString.Length <= 0)
			{
				return string.Empty;
			}
			return this.ProcessChange();
		}
	}
}