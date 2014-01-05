using System;
using System.Collections.Generic;
using System.Reflection;

namespace AIMLbot.Utils
{
	public class TagHandler
	{
		public string AssemblyName;

		public string ClassName;

		public string TagName;

		public TagHandler()
		{
		}

		public AIMLTagHandler Instantiate(Dictionary<string, Assembly> Assemblies)
		{
			if (!Assemblies.ContainsKey(this.AssemblyName))
			{
				return null;
			}
			Assembly item = Assemblies[this.AssemblyName];
			item.GetTypes();
			return (AIMLTagHandler)item.CreateInstance(this.ClassName);
		}
	}
}