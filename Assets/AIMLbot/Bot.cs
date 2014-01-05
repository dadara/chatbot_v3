using AIMLbot.AIMLTagHandlers;
using AIMLbot.Normalize;
using AIMLbot.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace AIMLbot
{
	public class Bot
	{
		public SettingsDictionary GlobalSettings;

		public SettingsDictionary GenderSubstitutions;

		public SettingsDictionary Person2Substitutions;

		public SettingsDictionary PersonSubstitutions;

		public SettingsDictionary Substitutions;

		public SettingsDictionary DefaultPredicates;

		private Dictionary<string, TagHandler> CustomTags;

		private Dictionary<string, Assembly> LateBindingAssemblies = new Dictionary<string, Assembly>();

		public List<string> Splitters = new List<string>();

		private List<string> LogBuffer = new List<string>();

		public bool isAcceptingUserInput = true;

		public DateTime StartedOn = DateTime.Now;

		public int Size;

		public Node Graphmaster;

		public bool TrustAIML = true;

		public int MaxThatSize = 256;

		public string LastLogMessage = string.Empty;

		SubQuery subQuery;

		public string AdminEmail
		{
			get
			{
				return this.GlobalSettings.grabSetting("adminemail");
			}
			set
			{
				if (value.Length <= 0)
				{
					this.GlobalSettings.addSetting("adminemail", "");
					return;
				}
				if (!(new Regex("^(([^<>()[\\]\\\\.,;:\\s@\\\"]+(\\.[^<>()[\\]\\\\.,;:\\s@\\\"]+)*)|(\\\".+\\\"))@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))$")).IsMatch(value))
				{
					throw new Exception("The AdminEmail is not a valid email address");
				}
				this.GlobalSettings.addSetting("adminemail", value);
			}
		}

		public bool IsLogging
		{
			get
			{
				if (this.GlobalSettings.grabSetting("islogging").ToLower() == "true")
				{
					return true;
				}
				return false;
			}
		}

		public CultureInfo Locale
		{
			get
			{
				return new CultureInfo(this.GlobalSettings.grabSetting("culture"));
			}
		}

		private int MaxLogBufferSize
		{
			get
			{
				return Convert.ToInt32(this.GlobalSettings.grabSetting("maxlogbuffersize"));
			}
		}

		private string NotAcceptingUserInputMessage
		{
			get
			{
				return this.GlobalSettings.grabSetting("notacceptinguserinputmessage");
			}
		}

		public string PathToAIML
		{
			get
			{
				return Path.Combine(Environment.CurrentDirectory, this.GlobalSettings.grabSetting("aimldirectory"));
			}
		}

		public string PathToConfigFiles
		{
			get
			{
				return Path.Combine(Environment.CurrentDirectory, this.GlobalSettings.grabSetting("configdirectory"));
			}
		}

		public string PathToLogs
		{
			get
			{
				return Path.Combine(Environment.CurrentDirectory, this.GlobalSettings.grabSetting("logdirectory"));
			}
		}

		public Gender Sex
		{
			get
			{
				Gender gender;
				switch (Convert.ToInt32(this.GlobalSettings.grabSetting("gender")))
				{
					case -1:
					{
						gender = Gender.Unknown;
						break;
					}
					case 0:
					{
						gender = Gender.Female;
						break;
					}
					case 1:
					{
						gender = Gender.Male;
						break;
					}
					default:
					{
						gender = Gender.Unknown;
						break;
					}
				}
				return gender;
			}
		}

		public Regex Strippers
		{
			get
			{
				return new Regex(this.GlobalSettings.grabSetting("stripperregex"), RegexOptions.IgnorePatternWhitespace);
			}
		}

		public double TimeOut
		{
			get
			{
				return Convert.ToDouble(this.GlobalSettings.grabSetting("timeout"));
			}
		}

		public string TimeOutMessage
		{
			get
			{
				return this.GlobalSettings.grabSetting("timeoutmessage");
			}
		}

		public bool WillCallHome
		{
			get
			{
				if (this.GlobalSettings.grabSetting("willcallhome").ToLower() == "true")
				{
					return true;
				}
				return false;
			}
		}

		public Bot()
		{
			this.setup();
		}

		public Result Chat(string rawInput, string UserGUID)
		{
			Request request = new Request(rawInput, new User(UserGUID, this), this);
			return this.Chat(request);
		}

		public Result Chat(Request request)
		{
			Result result = new Result(request.user, this, request);
			if (!this.isAcceptingUserInput)
			{
				result.OutputSentences.Add(this.NotAcceptingUserInputMessage);
			}
			else
			{
				AIMLLoader aIMLLoader = new AIMLLoader(this);
				string[] strArrays = (new SplitIntoSentences(this)).Transform(request.rawInput);
				for (int i = 0; i < (int)strArrays.Length; i++)
				{
					string str = strArrays[i];
					result.InputSentences.Add(str);
					string str1 = aIMLLoader.generatePath(str, request.user.getLastBotOutput(), request.user.Topic, true);
					result.NormalizedPaths.Add(str1);
				}
				foreach (string normalizedPath in result.NormalizedPaths)
				{
					subQuery = new SubQuery(normalizedPath)
					{
						Template = this.Graphmaster.evaluate(normalizedPath, subQuery, request, MatchState.UserInput, new StringBuilder())
					};
					result.SubQueries.Add(subQuery);
				}
				foreach (SubQuery subQuery1 in result.SubQueries)
				{
					if (subQuery1.Template.Length <= 0)
					{
						continue;
					}
					try
					{
						XmlNode node = AIMLTagHandler.getNode(subQuery1.Template);
						string str2 = this.processNode(node, subQuery1, request, result, request.user);
						if (str2.Length > 0)
						{
							result.OutputSentences.Add(str2);
						}
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						if (this.WillCallHome)
						{
							this.phoneHome(exception.Message, request);
						}
						string[] strArrays1 = new string[] { "WARNING! A problem was encountered when trying to process the input: ", request.rawInput, " with the template: \"", subQuery1.Template, "\"" };
						this.writeToLog(string.Concat(strArrays1));
					}
				}
			}
			result.Duration = DateTime.Now - request.StartedOn;
			request.user.addResult(result);
			return result;
		}

		public AIMLTagHandler getBespokeTags(User user, SubQuery query, Request request, Result result, XmlNode node)
		{
			if (!this.CustomTags.ContainsKey(node.Name.ToLower()))
			{
				return null;
			}
			TagHandler item = this.CustomTags[node.Name.ToLower()];
			AIMLTagHandler aIMLTagHandler = item.Instantiate(this.LateBindingAssemblies);
			if (object.Equals(null, aIMLTagHandler))
			{
				return null;
			}
			aIMLTagHandler.user = user;
			aIMLTagHandler.query = query;
			aIMLTagHandler.request = request;
			aIMLTagHandler.result = result;
			aIMLTagHandler.templateNode = node;
			aIMLTagHandler.bot = this;
			return aIMLTagHandler;
		}

		public void loadAIMLFromFiles()
		{
			(new AIMLLoader(this)).loadAIML();
		}

		public void loadAIMLFromXML(XmlDocument newAIML, string filename)
		{
			(new AIMLLoader(this)).loadAIMLFromXML(newAIML, filename);
		}

		public void loadCustomTagHandlers(string pathToDLL)
		{
			Assembly assembly = Assembly.LoadFrom(pathToDLL);
			Type[] types = assembly.GetTypes();
			for (int i = 0; i < (int)types.Length; i++)
			{
				object[] customAttributes = types[i].GetCustomAttributes(false);
				for (int j = 0; j < (int)customAttributes.Length; j++)
				{
					if (customAttributes[j] is CustomTagAttribute)
					{
						if (!this.LateBindingAssemblies.ContainsKey(assembly.FullName))
						{
							this.LateBindingAssemblies.Add(assembly.FullName, assembly);
						}
						TagHandler tagHandler = new TagHandler()
						{
							AssemblyName = assembly.FullName,
							ClassName = types[i].FullName,
							TagName = types[i].Name.ToLower()
						};
						if (this.CustomTags.ContainsKey(tagHandler.TagName))
						{
							string[] tagName = new string[] { "ERROR! Unable to add the custom tag: <", tagHandler.TagName, ">, found in: ", pathToDLL, " as a handler for this tag already exists." };
							throw new Exception(string.Concat(tagName));
						}
						this.CustomTags.Add(tagHandler.TagName, tagHandler);
					}
				}
			}
		}

		public void loadFromBinaryFile(string path)
		{
			FileStream fileStream = File.OpenRead(path);
			this.Graphmaster = (Node)(new BinaryFormatter()).Deserialize(fileStream);
			fileStream.Close();
		}

		public void loadSettings()
		{
			string str = Path.Combine(Environment.CurrentDirectory, Path.Combine("config", "Settings.xml"));
			this.loadSettings(str);
		}

		public string loadSettings(int cnt)
		{
			string str = Path.Combine(Environment.CurrentDirectory, Path.Combine("config", "Settings.xml"));
			return str;
			
		}

		public void loadSettings(string pathToSettings)
		{
			this.GlobalSettings.loadSettings(pathToSettings);
			if (!this.GlobalSettings.containsSettingCalled("version"))
			{
				this.GlobalSettings.addSetting("version", Environment.Version.ToString());
			}
			if (!this.GlobalSettings.containsSettingCalled("name"))
			{
				this.GlobalSettings.addSetting("name", "Unknown");
			}
			if (!this.GlobalSettings.containsSettingCalled("botmaster"))
			{
				this.GlobalSettings.addSetting("botmaster", "Unknown");
			}
			if (!this.GlobalSettings.containsSettingCalled("master"))
			{
				this.GlobalSettings.addSetting("botmaster", "Unknown");
			}
			if (!this.GlobalSettings.containsSettingCalled("author"))
			{
				this.GlobalSettings.addSetting("author", "Nicholas H.Tollervey");
			}
			if (!this.GlobalSettings.containsSettingCalled("location"))
			{
				this.GlobalSettings.addSetting("location", "Unknown");
			}
			if (!this.GlobalSettings.containsSettingCalled("gender"))
			{
				this.GlobalSettings.addSetting("gender", "-1");
			}
			if (!this.GlobalSettings.containsSettingCalled("birthday"))
			{
				this.GlobalSettings.addSetting("birthday", "2006/11/08");
			}
			if (!this.GlobalSettings.containsSettingCalled("birthplace"))
			{
				this.GlobalSettings.addSetting("birthplace", "Towcester, Northamptonshire, UK");
			}
			if (!this.GlobalSettings.containsSettingCalled("website"))
			{
				this.GlobalSettings.addSetting("website", "http://sourceforge.net/projects/aimlbot");
			}
			if (!this.GlobalSettings.containsSettingCalled("adminemail"))
			{
				this.GlobalSettings.addSetting("adminemail", "");
			}
			else
			{
				this.AdminEmail = this.GlobalSettings.grabSetting("adminemail");
			}
			if (!this.GlobalSettings.containsSettingCalled("islogging"))
			{
				this.GlobalSettings.addSetting("islogging", "False");
			}
			if (!this.GlobalSettings.containsSettingCalled("willcallhome"))
			{
				this.GlobalSettings.addSetting("willcallhome", "False");
			}
			if (!this.GlobalSettings.containsSettingCalled("timeout"))
			{
				this.GlobalSettings.addSetting("timeout", "2000");
			}
			if (!this.GlobalSettings.containsSettingCalled("timeoutmessage"))
			{
				this.GlobalSettings.addSetting("timeoutmessage", "ERROR: The request has timed out.");
			}
			if (!this.GlobalSettings.containsSettingCalled("culture"))
			{
				this.GlobalSettings.addSetting("culture", "en-US");
			}
			if (!this.GlobalSettings.containsSettingCalled("splittersfile"))
			{
				this.GlobalSettings.addSetting("splittersfile", "Splitters.xml");
			}
			if (!this.GlobalSettings.containsSettingCalled("person2substitutionsfile"))
			{
				this.GlobalSettings.addSetting("person2substitutionsfile", "Person2Substitutions.xml");
			}
			if (!this.GlobalSettings.containsSettingCalled("personsubstitutionsfile"))
			{
				this.GlobalSettings.addSetting("personsubstitutionsfile", "PersonSubstitutions.xml");
			}
			if (!this.GlobalSettings.containsSettingCalled("gendersubstitutionsfile"))
			{
				this.GlobalSettings.addSetting("gendersubstitutionsfile", "GenderSubstitutions.xml");
			}
			if (!this.GlobalSettings.containsSettingCalled("defaultpredicates"))
			{
				this.GlobalSettings.addSetting("defaultpredicates", "DefaultPredicates.xml");
			}
			if (!this.GlobalSettings.containsSettingCalled("substitutionsfile"))
			{
				this.GlobalSettings.addSetting("substitutionsfile", "Substitutions.xml");
			}
			if (!this.GlobalSettings.containsSettingCalled("aimldirectory"))
			{
				this.GlobalSettings.addSetting("aimldirectory", "aiml");
			}
			if (!this.GlobalSettings.containsSettingCalled("configdirectory"))
			{
				this.GlobalSettings.addSetting("configdirectory", "config");
			}
			if (!this.GlobalSettings.containsSettingCalled("logdirectory"))
			{
				this.GlobalSettings.addSetting("logdirectory", "logs");
			}
			if (!this.GlobalSettings.containsSettingCalled("maxlogbuffersize"))
			{
				this.GlobalSettings.addSetting("maxlogbuffersize", "64");
			}
			if (!this.GlobalSettings.containsSettingCalled("notacceptinguserinputmessage"))
			{
				this.GlobalSettings.addSetting("notacceptinguserinputmessage", "This bot is currently set to not accept user input.");
			}
			if (!this.GlobalSettings.containsSettingCalled("stripperregex"))
			{
				this.GlobalSettings.addSetting("stripperregex", "[^0-9a-zA-Z]");
			}
			this.Person2Substitutions.loadSettings(Path.Combine(this.PathToConfigFiles, this.GlobalSettings.grabSetting("person2substitutionsfile")));
			this.PersonSubstitutions.loadSettings(Path.Combine(this.PathToConfigFiles, this.GlobalSettings.grabSetting("personsubstitutionsfile")));
			this.GenderSubstitutions.loadSettings(Path.Combine(this.PathToConfigFiles, this.GlobalSettings.grabSetting("gendersubstitutionsfile")));
			this.DefaultPredicates.loadSettings(Path.Combine(this.PathToConfigFiles, this.GlobalSettings.grabSetting("defaultpredicates")));
			this.Substitutions.loadSettings(Path.Combine(this.PathToConfigFiles, this.GlobalSettings.grabSetting("substitutionsfile")));
			this.loadSplitters(Path.Combine(this.PathToConfigFiles, this.GlobalSettings.grabSetting("splittersfile")));
		}

		private void loadSplitters(string pathToSplitters)
		{
			if ((new FileInfo(pathToSplitters)).Exists)
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(pathToSplitters);
				if (xmlDocument.ChildNodes.Count == 2 && xmlDocument.LastChild.HasChildNodes)
				{
					foreach (XmlNode childNode in xmlDocument.LastChild.ChildNodes)
					{
						if (!((childNode.Name == "item") & childNode.Attributes.Count == 1))
						{
							continue;
						}
						string value = childNode.Attributes["value"].Value;
						this.Splitters.Add(value);
					}
				}
			}
			if (this.Splitters.Count == 0)
			{
				this.Splitters.Add(".");
				this.Splitters.Add("!");
				this.Splitters.Add("?");
				this.Splitters.Add(";");
			}
		}

		public void phoneHome(string errorMessage, Request request)
		{
			MailMessage mailMessage = new MailMessage("donotreply@aimlbot.com", this.AdminEmail)
			{
				Subject = "WARNING! AIMLBot has encountered a problem..."
			};
			string str = "Dear Botmaster,\r\n\r\nThis is an automatically generated email to report errors with your bot.\r\n\r\nAt *TIME* the bot encountered the following error:\r\n\r\n\"*MESSAGE*\"\r\n\r\nwhilst processing the following input:\r\n\r\n\"*RAWINPUT*\"\r\n\r\nfrom the user with an id of: *USER*\r\n\r\nThe normalized paths generated by the raw input were as follows:\r\n\r\n*PATHS*\r\n\r\nPlease check your AIML!\r\n\r\nRegards,\r\n\r\nThe AIMLbot program.\r\n";
			DateTime now = DateTime.Now;
			str = str.Replace("*TIME*", now.ToString());
			str = str.Replace("*MESSAGE*", errorMessage);
			str = str.Replace("*RAWINPUT*", request.rawInput);
			str = str.Replace("*USER*", request.user.UserID);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string normalizedPath in request.result.NormalizedPaths)
			{
				stringBuilder.Append(string.Concat(normalizedPath, Environment.NewLine));
			}
			str = str.Replace("*PATHS*", stringBuilder.ToString());
			mailMessage.Body = str;
			mailMessage.IsBodyHtml = false;
			try
			{
				if (mailMessage.To.Count > 0)
				{
					(new SmtpClient()).Send(mailMessage);
				}
			}
			catch
			{
			}
		}

		private string processNode(XmlNode node, SubQuery query, Request request, Result result, User user)
		{
			if (request.StartedOn.AddMilliseconds(request.bot.TimeOut) < DateTime.Now)
			{
				Bot bot = request.bot;
				string[] userID = new string[] { "WARNING! Request timeout. User: ", request.user.UserID, " raw input: \"", request.rawInput, "\" processing template: \"", query.Template, "\"" };
				bot.writeToLog(string.Concat(userID));
				request.hasTimedOut = true;
				return string.Empty;
			}
			string lower = node.Name.ToLower();
			if (lower == "template")
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (node.HasChildNodes)
				{
					foreach (XmlNode childNode in node.ChildNodes)
					{
						stringBuilder.Append(this.processNode(childNode, query, request, result, user));
					}
				}
				return stringBuilder.ToString();
			}
			AIMLTagHandler bespokeTags = null;
			bespokeTags = this.getBespokeTags(user, query, request, result, node);
			if (object.Equals(null, bespokeTags))
			{
				string str = lower;
				string str1 = str;
				if (str != null)
				{
					switch (str1)
					{
						case "bot":
						{
							bespokeTags = new AIMLbot.AIMLTagHandlers.bot(this, user, query, request, result, node);
							goto Label0;
						}
						case "condition":
						{
							bespokeTags = new condition(this, user, query, request, result, node);
							goto Label0;
						}
						case "date":
						{
							bespokeTags = new date(this, user, query, request, result, node);
							goto Label0;
						}
						case "formal":
						{
							bespokeTags = new formal(this, user, query, request, result, node);
							goto Label0;
						}
						case "gender":
						{
							bespokeTags = new gender(this, user, query, request, result, node);
							goto Label0;
						}
						case "get":
						{
							bespokeTags = new get(this, user, query, request, result, node);
							goto Label0;
						}
						case "gossip":
						{
							bespokeTags = new gossip(this, user, query, request, result, node);
							goto Label0;
						}
						case "id":
						{
							bespokeTags = new id(this, user, query, request, result, node);
							goto Label0;
						}
						case "input":
						{
							bespokeTags = new input(this, user, query, request, result, node);
							goto Label0;
						}
						case "javascript":
						{
							bespokeTags = new javascript(this, user, query, request, result, node);
							goto Label0;
						}
						case "learn":
						{
							bespokeTags = new learn(this, user, query, request, result, node);
							goto Label0;
						}
						case "lowercase":
						{
							bespokeTags = new lowercase(this, user, query, request, result, node);
							goto Label0;
						}
						case "person":
						{
							bespokeTags = new person(this, user, query, request, result, node);
							goto Label0;
						}
						case "person2":
						{
							bespokeTags = new person2(this, user, query, request, result, node);
							goto Label0;
						}
						case "random":
						{
							bespokeTags = new random(this, user, query, request, result, node);
							goto Label0;
						}
						case "sentence":
						{
							bespokeTags = new sentence(this, user, query, request, result, node);
							goto Label0;
						}
						case "set":
						{
							bespokeTags = new set(this, user, query, request, result, node);
							goto Label0;
						}
						case "size":
						{
							bespokeTags = new size(this, user, query, request, result, node);
							goto Label0;
						}
						case "sr":
						{
							bespokeTags = new sr(this, user, query, request, result, node);
							goto Label0;
						}
						case "srai":
						{
							bespokeTags = new srai(this, user, query, request, result, node);
							goto Label0;
						}
						case "star":
						{
							bespokeTags = new star(this, user, query, request, result, node);
							goto Label0;
						}
						case "system":
						{
							bespokeTags = new system(this, user, query, request, result, node);
							goto Label0;
						}
						case "that":
						{
							bespokeTags = new that(this, user, query, request, result, node);
							goto Label0;
						}
						case "thatstar":
						{
							bespokeTags = new thatstar(this, user, query, request, result, node);
							goto Label0;
						}
						case "think":
						{
							bespokeTags = new think(this, user, query, request, result, node);
							goto Label0;
						}
						case "topicstar":
						{
							bespokeTags = new topicstar(this, user, query, request, result, node);
							goto Label0;
						}
						case "uppercase":
						{
							bespokeTags = new uppercase(this, user, query, request, result, node);
							goto Label0;
						}
						case "version":
						{
							bespokeTags = new version(this, user, query, request, result, node);
							goto Label0;
						}
					}
				}
				bespokeTags = null;
			}
		Label0:
			if (object.Equals(null, bespokeTags))
			{
				return node.InnerText;
			}
			if (bespokeTags.isRecursive)
			{
				if (node.HasChildNodes)
				{
					foreach (XmlNode xmlNodes in node.ChildNodes)
					{
						if (xmlNodes.NodeType == XmlNodeType.Text)
						{
							continue;
						}
						xmlNodes.InnerXml = this.processNode(xmlNodes, query, request, result, user);
					}
				}
				return bespokeTags.Transform();
			}
			string str2 = bespokeTags.Transform();
			XmlNode xmlNodes1 = AIMLTagHandler.getNode(string.Concat("<node>", str2, "</node>"));
			if (!xmlNodes1.HasChildNodes)
			{
				return xmlNodes1.InnerXml;
			}
			StringBuilder stringBuilder1 = new StringBuilder();
			foreach (XmlNode childNode1 in xmlNodes1.ChildNodes)
			{
				stringBuilder1.Append(this.processNode(childNode1, query, request, result, user));
			}
			return stringBuilder1.ToString();
		}

		public void saveToBinaryFile(string path)
		{
			FileInfo fileInfo = new FileInfo(path);
			if (fileInfo.Exists)
			{
				fileInfo.Delete();
			}
			FileStream fileStream = File.Create(path);
			(new BinaryFormatter()).Serialize(fileStream, this.Graphmaster);
			fileStream.Close();
		}

		private void setup()
		{
			this.GlobalSettings = new SettingsDictionary(this);
			this.GenderSubstitutions = new SettingsDictionary(this);
			this.Person2Substitutions = new SettingsDictionary(this);
			this.PersonSubstitutions = new SettingsDictionary(this);
			this.Substitutions = new SettingsDictionary(this);
			this.DefaultPredicates = new SettingsDictionary(this);
			this.CustomTags = new Dictionary<string, TagHandler>();
			this.Graphmaster = new Node();
		}

		public void writeToLog(string message)
		{
			StreamWriter streamWriter;
			this.LastLogMessage = message;
			if (this.IsLogging)
			{
				List<string> logBuffer = this.LogBuffer;
				DateTime now = DateTime.Now;
				logBuffer.Add(string.Concat(now.ToString(), ": ", message, Environment.NewLine));
				if (this.LogBuffer.Count > this.MaxLogBufferSize - 1)
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(this.PathToLogs);
					if (!directoryInfo.Exists)
					{
						directoryInfo.Create();
					}
					DateTime dateTime = DateTime.Now;
					string str = string.Concat(dateTime.ToString("yyyyMMdd"), ".log");
					FileInfo fileInfo = new FileInfo(Path.Combine(this.PathToLogs, str));
					streamWriter = (fileInfo.Exists ? fileInfo.AppendText() : fileInfo.CreateText());
					foreach (string logBuffer1 in this.LogBuffer)
					{
						streamWriter.WriteLine(logBuffer1);
					}
					streamWriter.Close();
					this.LogBuffer.Clear();
				}
			}
			if (!object.Equals(null, this.WrittenToLog))
			{
				this.WrittenToLog();
			}
		}

		public event Bot.LogMessageDelegate WrittenToLog;

		public delegate void LogMessageDelegate();
	}
}