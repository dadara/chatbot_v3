using AIMLbot;
using AIMLbot.Utils;
using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Xml;

namespace AIMLbot.AIMLTagHandlers
{
	public class condition : AIMLTagHandler
	{
		public condition(Bot bot, User user, SubQuery query, Request request, Result result, XmlNode templateNode) : base(bot, user, query, request, result, templateNode)
		{
			this.isRecursive = false;
		}

		protected override string ProcessChange()
		{
			string innerXml;
			if (this.templateNode.Name.ToLower() == "condition")
			{
				if (this.templateNode.Attributes.Count != 2)
				{
					if (this.templateNode.Attributes.Count == 1)
					{
						if (this.templateNode.Attributes[0].Name == "name")
						{
							string value = this.templateNode.Attributes[0].Value;
							IEnumerator enumerator = this.templateNode.ChildNodes.GetEnumerator();
							try
							{
								while (enumerator.MoveNext())
								{
									XmlNode current = (XmlNode)enumerator.Current;
									if (current.Name.ToLower() != "li")
									{
										continue;
									}
									if (current.Attributes.Count != 1)
									{
										if (current.Attributes.Count != 0)
										{
											continue;
										}
										innerXml = current.InnerXml;
										return innerXml;
									}
									else
									{
										if (current.Attributes[0].Name.ToLower() != "value")
										{
											continue;
										}
										string str = this.user.Predicates.grabSetting(value);
										if (!(new Regex(current.Attributes[0].Value.Replace(" ", "\\s").Replace("*", "[\\sA-Z0-9]+"), RegexOptions.IgnoreCase)).IsMatch(str))
										{
											continue;
										}
										innerXml = current.InnerXml;
										return innerXml;
									}
								}
								return string.Empty;
							}
							finally
							{
								IDisposable disposable = enumerator as IDisposable;
								if (disposable != null)
								{
									disposable.Dispose();
								}
							}
						}
						else
						{
							return string.Empty;
						}
					}
					else if (this.templateNode.Attributes.Count == 0)
					{
						IEnumerator enumerator1 = this.templateNode.ChildNodes.GetEnumerator();
						try
						{
							while (enumerator1.MoveNext())
							{
								XmlNode xmlNodes = (XmlNode)enumerator1.Current;
								if (xmlNodes.Name.ToLower() != "li")
								{
									continue;
								}
								if (xmlNodes.Attributes.Count != 2)
								{
									if (xmlNodes.Attributes.Count != 0)
									{
										continue;
									}
									innerXml = xmlNodes.InnerXml;
									return innerXml;
								}
								else
								{
									string value1 = "";
									string str1 = "";
									if (xmlNodes.Attributes[0].Name == "name")
									{
										value1 = xmlNodes.Attributes[0].Value;
									}
									else if (xmlNodes.Attributes[0].Name == "value")
									{
										str1 = xmlNodes.Attributes[0].Value;
									}
									if (xmlNodes.Attributes[1].Name == "name")
									{
										value1 = xmlNodes.Attributes[1].Value;
									}
									else if (xmlNodes.Attributes[1].Name == "value")
									{
										str1 = xmlNodes.Attributes[1].Value;
									}
									if (!(value1.Length > 0 & str1.Length > 0))
									{
										continue;
									}
									string str2 = this.user.Predicates.grabSetting(value1);
									if (!(new Regex(str1.Replace(" ", "\\s").Replace("*", "[\\sA-Z0-9]+"), RegexOptions.IgnoreCase)).IsMatch(str2))
									{
										continue;
									}
									innerXml = xmlNodes.InnerXml;
									return innerXml;
								}
							}
							return string.Empty;
						}
						finally
						{
							IDisposable disposable1 = enumerator1 as IDisposable;
							if (disposable1 != null)
							{
								disposable1.Dispose();
							}
						}
					}
					else
					{
						return string.Empty;
					}
					return innerXml;
				}
				else
				{
					string value2 = "";
					string value3 = "";
					if (this.templateNode.Attributes[0].Name == "name")
					{
						value2 = this.templateNode.Attributes[0].Value;
					}
					else if (this.templateNode.Attributes[0].Name == "value")
					{
						value3 = this.templateNode.Attributes[0].Value;
					}
					if (this.templateNode.Attributes[1].Name == "name")
					{
						value2 = this.templateNode.Attributes[1].Value;
					}
					else if (this.templateNode.Attributes[1].Name == "value")
					{
						value3 = this.templateNode.Attributes[1].Value;
					}
					if (value2.Length > 0 & value3.Length > 0)
					{
						string str3 = this.user.Predicates.grabSetting(value2);
						if ((new Regex(value3.Replace(" ", "\\s").Replace("*", "[\\sA-Z0-9]+"), RegexOptions.IgnoreCase)).IsMatch(str3))
						{
							return this.templateNode.InnerXml;
						}
					}
				}
			}
			return string.Empty;
		}
	}
}