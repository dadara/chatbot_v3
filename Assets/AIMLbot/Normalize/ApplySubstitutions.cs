using AIMLbot;
using AIMLbot.Utils;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace AIMLbot.Normalize
{
	public class ApplySubstitutions : TextTransformer
	{
		public ApplySubstitutions(Bot bot, string inputString) : base(bot, inputString)
		{
		}

		public ApplySubstitutions(Bot bot) : base(bot)
		{
		}

		private static string getMarker(int len)
		{
			char[] charArray = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
			StringBuilder stringBuilder = new StringBuilder();
			Random random = new Random();
			for (int i = 0; i < len; i++)
			{
				stringBuilder.Append(charArray[random.Next((int)charArray.Length)]);
			}
			return stringBuilder.ToString();
		}

		private static string makeRegexSafe(string input)
		{
			string str = input.Replace("\\", "");
			str = str.Replace(")", "\\)");
			return str.Replace("(", "\\(").Replace(".", "\\.");
		}

		protected override string ProcessChange()
		{
			return ApplySubstitutions.Substitute(this.bot, this.bot.Substitutions, this.inputString);
		}

		public static string Substitute(Bot bot, SettingsDictionary dictionary, string target)
		{
			string marker = ApplySubstitutions.getMarker(5);
			string str = target;
			string[] settingNames = dictionary.SettingNames;
			for (int i = 0; i < (int)settingNames.Length; i++)
			{
				string str1 = settingNames[i];
				string str2 = ApplySubstitutions.makeRegexSafe(str1);
				string str3 = string.Concat("\\b", str2.TrimEnd(new char[0]).TrimStart(new char[0]), "\\b");
				string str4 = string.Concat(marker, dictionary.grabSetting(str1).Trim(), marker);
				str = Regex.Replace(str, str3, str4, RegexOptions.IgnoreCase);
			}
			return str.Replace(marker, "");
		}
	}
}