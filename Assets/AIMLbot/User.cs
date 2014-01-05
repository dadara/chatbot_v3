using AIMLbot.Utils;
using System;
using System.Collections.Generic;

namespace AIMLbot
{
	public class User
	{
		private string id;

		public Bot bot;

		private List<Result> Results = new List<Result>();

		public SettingsDictionary Predicates;

		public Result LastResult
		{
			get
			{
				if (this.Results.Count <= 0)
				{
					return null;
				}
				return this.Results[0];
			}
		}

		public string Topic
		{
			get
			{
				return this.Predicates.grabSetting("topic");
			}
		}

		public string UserID
		{
			get
			{
				return this.id;
			}
		}

		public User(string UserID, Bot bot)
		{
			if (UserID.Length <= 0)
			{
				throw new Exception("The UserID cannot be empty");
			}
			this.id = UserID;
			this.bot = bot;
			this.Predicates = new SettingsDictionary(this.bot);
			this.bot.DefaultPredicates.Clone(this.Predicates);
			this.Predicates.addSetting("topic", "*");
		}

		public void addResult(Result latestResult)
		{
			this.Results.Insert(0, latestResult);
		}

		public string getLastBotOutput()
		{
			if (this.Results.Count <= 0)
			{
				return "*";
			}
			return this.Results[0].RawOutput;
		}

		public string getResultSentence()
		{
			return this.getResultSentence(0, 0);
		}

		public string getResultSentence(int n)
		{
			return this.getResultSentence(n, 0);
		}

		public string getResultSentence(int n, int sentence)
		{
			if (n >= 0 & n < this.Results.Count)
			{
				Result item = this.Results[n];
				if (sentence >= 0 & sentence < item.InputSentences.Count)
				{
					return item.InputSentences[sentence];
				}
			}
			return string.Empty;
		}

		public string getThat()
		{
			return this.getThat(0, 0);
		}

		public string getThat(int n)
		{
			return this.getThat(n, 0);
		}

		public string getThat(int n, int sentence)
		{
			if (n >= 0 & n < this.Results.Count)
			{
				Result item = this.Results[n];
				if (sentence >= 0 & sentence < item.OutputSentences.Count)
				{
					return item.OutputSentences[sentence];
				}
			}
			return string.Empty;
		}
	}
}