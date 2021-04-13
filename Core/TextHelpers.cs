using System.Linq;

namespace VToolBase.Core {
	public static class TextHelpers {

		public static string UcFirst(this string str) =>
			string.IsNullOrEmpty(str) ? str : char.ToUpper(str[0]) + str[1..].ToLower();

		public static string UcWords(this string str) =>
			str.Split(' ').Select(UcFirst).JoinString(", ");
	}
}
