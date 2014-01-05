using AIMLbot;
using AIMLbot.Normalize;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace AIMLbot.Utils
{
	[Serializable]
	public class Node
	{
		private Dictionary<string, Node> children = new Dictionary<string, Node>();

		public string template = string.Empty;

		public string filename = string.Empty;

		public string word = string.Empty;

		public int NumberOfChildNodes
		{
			get
			{
				return this.children.Count;
			}
		}

		public Node()
		{
		}

		public void addCategory(string path, string template, string filename)
		{
			if (template.Length == 0)
			{
				string[] strArrays = new string[] { "The category with a pattern: ", path, " found in file: ", filename, " has an empty template tag. ABORTING" };
				throw new XmlException(string.Concat(strArrays));
			}
			if (path.Trim().Length == 0)
			{
				this.template = template;
				this.filename = filename;
				return;
			}
			string[] strArrays1 = path.Trim().Split(" ".ToCharArray());
			string str = MakeCaseInsensitive.TransformInput(strArrays1[0]);
			string str1 = path.Substring(str.Length, path.Length - str.Length).Trim();
			if (this.children.ContainsKey(str))
			{
				this.children[str].addCategory(str1, template, filename);
				return;
			}
			Node node = new Node()
			{
				word = str
			};
			node.addCategory(str1, template, filename);
			this.children.Add(node.word, node);
		}

		public string evaluate(string path, SubQuery query, Request request, MatchState matchstate, StringBuilder wildcard)
		{
			if (request.StartedOn.AddMilliseconds(request.bot.TimeOut) < DateTime.Now)
			{
				Bot bot = request.bot;
				string[] userID = new string[] { "WARNING! Request timeout. User: ", request.user.UserID, " raw input: \"", request.rawInput, "\"" };
				bot.writeToLog(string.Concat(userID));
				request.hasTimedOut = true;
				return string.Empty;
			}
			path = path.Trim();
			if (this.children.Count == 0)
			{
				if (path.Length > 0)
				{
					this.storeWildCard(path, wildcard);
				}
				return this.template;
			}
			if (path.Length == 0)
			{
				return this.template;
			}
			string[] strArrays = path.Split(" \r\n\t".ToCharArray());
			string str = MakeCaseInsensitive.TransformInput(strArrays[0]);
			string str1 = path.Substring(str.Length, path.Length - str.Length);
			if (this.children.ContainsKey("_"))
			{
				Node item = this.children["_"];
				StringBuilder stringBuilder = new StringBuilder();
				this.storeWildCard(strArrays[0], stringBuilder);
				string str2 = item.evaluate(str1, query, request, matchstate, stringBuilder);
				if (str2.Length > 0)
				{
					if (stringBuilder.Length > 0)
					{
						switch (matchstate)
						{
							case MatchState.UserInput:
							{
								query.InputStar.Add(stringBuilder.ToString());
								stringBuilder.Remove(0, stringBuilder.Length);
								break;
							}
							case MatchState.That:
							{
								query.ThatStar.Add(stringBuilder.ToString());
								break;
							}
							case MatchState.Topic:
							{
								query.TopicStar.Add(stringBuilder.ToString());
								break;
							}
						}
					}
					return str2;
				}
			}
			if (this.children.ContainsKey(str))
			{
				MatchState matchState = matchstate;
				if (str == "<THAT>")
				{
					matchState = MatchState.That;
				}
				else if (str == "<TOPIC>")
				{
					matchState = MatchState.Topic;
				}
				Node node = this.children[str];
				StringBuilder stringBuilder1 = new StringBuilder();
				string str3 = node.evaluate(str1, query, request, matchState, stringBuilder1);
				if (str3.Length > 0)
				{
					if (stringBuilder1.Length > 0)
					{
						switch (matchstate)
						{
							case MatchState.UserInput:
							{
								query.InputStar.Add(stringBuilder1.ToString());
								stringBuilder1.Remove(0, stringBuilder1.Length);
								break;
							}
							case MatchState.That:
							{
								query.ThatStar.Add(stringBuilder1.ToString());
								stringBuilder1.Remove(0, stringBuilder1.Length);
								break;
							}
							case MatchState.Topic:
							{
								query.TopicStar.Add(stringBuilder1.ToString());
								stringBuilder1.Remove(0, stringBuilder1.Length);
								break;
							}
						}
					}
					return str3;
				}
			}
			if (this.children.ContainsKey("*"))
			{
				Node item1 = this.children["*"];
				StringBuilder stringBuilder2 = new StringBuilder();
				this.storeWildCard(strArrays[0], stringBuilder2);
				string str4 = item1.evaluate(str1, query, request, matchstate, stringBuilder2);
				if (str4.Length > 0)
				{
					if (stringBuilder2.Length > 0)
					{
						switch (matchstate)
						{
							case MatchState.UserInput:
							{
								query.InputStar.Add(stringBuilder2.ToString());
								stringBuilder2.Remove(0, stringBuilder2.Length);
								break;
							}
							case MatchState.That:
							{
								query.ThatStar.Add(stringBuilder2.ToString());
								break;
							}
							case MatchState.Topic:
							{
								query.TopicStar.Add(stringBuilder2.ToString());
								break;
							}
						}
					}
					return str4;
				}
			}
			if (!(this.word == "_") && !(this.word == "*"))
			{
				wildcard = new StringBuilder();
				return string.Empty;
			}
			this.storeWildCard(strArrays[0], wildcard);
			return this.evaluate(str1, query, request, matchstate, wildcard);
		}

		private void storeWildCard(string word, StringBuilder wildcard)
		{
			if (wildcard.Length > 0)
			{
				wildcard.Append(" ");
			}
			wildcard.Append(word);
		}
	}
}