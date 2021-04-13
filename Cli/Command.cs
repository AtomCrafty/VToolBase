﻿using System;
using System.Collections.Generic;
using System.Linq;
using VToolBase.Cli.Commands;
using VToolBase.Cli.Util;
using VToolBase.Core;

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

		protected void Wait(bool fallback) {
			if(Parameters.GetBool("wait", 'w', fallback)) {
				Console.ReadLine();
			}
		}

		protected void Log(string text) {
			if(!Parameters.GetBool("quiet", 'q', false) && Parameters.GetBool("verbose", 'v', false)) {
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
