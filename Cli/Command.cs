using System;
using System.Linq;
using VToolBase.Cli.Util;

namespace VToolBase.Cli {
	public abstract class Command {

		public readonly CommandParameters Parameters;
		public readonly string[] Arguments;
		public abstract string Name { get; }
		public abstract string[] Description { get; }

		public abstract (string syntax, string description)[] Usage { get; }
		public abstract (char shorthand, string name, string fallback, string description)[] Flags { get; }

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
					Log($"File \ae{file}\a- already exists. Overwrite? (\aby\a-es/\abn\a-o/\aba\a-ll)");
					var key = Console.ReadKey(true);
					switch(key.KeyChar) {
						case 'y':
							return true;
						case 'n':
							return false;
						case 'a':
							Parameters.SetFlag("--overwrite", "true");
							Parameters.SetFlag("-o", "true");
							return true;
						default:
							goto again;
					}
			}
		}

		protected void Log(string text, bool verbose = false) {
			if(!Parameters.GetBool("quiet", 'q', false) && (!verbose || Parameters.GetBool("verbose", 'v', false))) {
				Output.WriteLineColored(text);
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
}
