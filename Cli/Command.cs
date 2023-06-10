using System;
using System.Linq;
using VToolBase.Cli.Util;

namespace VToolBase.Cli {
	public abstract class Command {

		public readonly CommandParameters Parameters;
		public readonly string[] Arguments;
		public abstract string Name { get; }
		public abstract string[] Description { get; }

		public abstract UsageCollection Usage { get; }
		public abstract FlagCollection Flags { get; }

		protected Command(CommandParameters parameters) {
			Parameters = parameters;
			Arguments = parameters.Arguments.Skip(1).ToArray();
		}

		public abstract bool Execute();

		#region Helpers

		protected void Wait() {
			Wait(ProcessHelpers.WasStartedFromExplorer());
		}

		protected void Wait(bool fallback) {
			if(Parameters.GetBool("wait", 'w', fallback)) {
				Console.ReadLine();
			}
		}

		protected bool ConfirmOverwrite(string file) {
			switch(Parameters.GetString("overwrite", 'o', "ask")) {
				case "true": return true;
				case "false": return false;

				default:
				again:
					Log($"File \ae{file}\a- already exists. Overwrite? (\aby\a-es/\abn\a-o/\aba\a-ll)", newLine: false);
					var key = Console.ReadKey(true);
					switch(key.KeyChar) {
						case 'y':
							Output.ClearLine();
							return true;
						case 'n':
							Output.ClearLine();
							return false;
						case 'a':
							Parameters.SetFlag("--overwrite", "true");
							Parameters.SetFlag("-o", "true");
							Output.ClearLine();
							return true;
						default:
							goto again;
					}
			}
		}

		protected void Log(string text, bool verbose = false, bool newLine = true) {
			if(!Parameters.GetBool("quiet", 'q', false) && (!verbose || Parameters.GetBool("verbose", 'v', false))) {
				if(newLine)
					Output.WriteLineColored(text);
				else
					Output.WriteColored(text);
			}
		}

		protected void Success(string text = "Done.") {
			if(!Parameters.GetBool("quiet", 'q', false)) {
				Output.WriteLine(text, ConsoleColor.Green);
			}
		}

		protected void Error(string text) {
			if(!Parameters.GetBool("quiet", 'q', false)) {
				Output.WriteLine(text, ConsoleColor.Red);
			}
		}

		#endregion
	}

	public readonly record struct CommandUsage(string Syntax, string? Description);

	public class UsageCollection : List<CommandUsage> {
		public void Add(string syntax, string? description) =>
			Add(new CommandUsage(syntax, description));
	}

	public readonly record struct CommandFlag(char? Shorthand, string? Name, string? Fallback, string? Description);

	public class FlagCollection : List<CommandFlag> {
		public void Add(char? shorthand, string? name, string? fallback, string? description) =>
			Add(new CommandFlag(shorthand, name, fallback, description));
	}
}
