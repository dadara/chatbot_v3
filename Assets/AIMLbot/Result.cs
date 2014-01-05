using AIMLbot.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace AIMLbot
{
	public class Result
	{
		public Bot bot;

		public User user;

		public Request request;

		public List<string> NormalizedPaths = new List<string>();

		public TimeSpan Duration;

		public List<SubQuery> SubQueries = new List<SubQuery>();

		public List<string> OutputSentences = new List<string>();

		public List<string> InputSentences = new List<string>();

		public string Output
		{
			get
			{
				if (this.OutputSentences.Count > 0)
				{
					return this.RawOutput;
				}
				if (this.request.hasTimedOut)
				{
					return this.bot.TimeOutMessage;
				}
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string normalizedPath in this.NormalizedPaths)
				{
					stringBuilder.Append(string.Concat(normalizedPath, Environment.NewLine));
				}
				Bot bot = this.bot;
				string[] rawInput = new string[] { "The bot could not find any response for the input: ", this.RawInput, " with the path(s): ", Environment.NewLine, stringBuilder.ToString(), " from the user with an id: ", this.user.UserID };
				bot.writeToLog(string.Concat(rawInput));
				return string.Empty;
			}
		}

		public string RawInput
		{
			get
			{
				return this.request.rawInput;
			}
		}

		public string RawOutput
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string outputSentence in this.OutputSentences)
				{
					string str = outputSentence.Trim();
					if (!this.checkEndsAsSentence(str))
					{
						str = string.Concat(str, ".");
					}
					stringBuilder.Append(string.Concat(str, " "));
				}
				return stringBuilder.ToString().Trim();
			}
		}

		public Result(User user, Bot bot, Request request)
		{
			this.user = user;
			this.bot = bot;
			this.request = request;
			this.request.result = this;
		}

		private bool checkEndsAsSentence(string sentence)
		{
			bool flag;
			List<string>.Enumerator enumerator = this.bot.Splitters.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					string current = enumerator.Current;
					if (!sentence.Trim().EndsWith(current))
					{
						continue;
					}
					flag = true;
					return flag;
				}
				return false;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return flag;
		}

		public override string ToString()
		{
			return this.Output;
		}
	}
}