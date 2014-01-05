using System;

namespace AIMLbot
{
	public class Request
	{
		public string rawInput;

		public DateTime StartedOn;

		public User user;

		public Bot bot;

		public Result result;

		public bool hasTimedOut;

		public Request(string rawInput, User user, Bot bot)
		{
			this.rawInput = rawInput;
			this.user = user;
			this.bot = bot;
			this.StartedOn = DateTime.Now;
		}
	}
}