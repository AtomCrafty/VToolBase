using System.IO;

namespace VToolBase.Core {
	public static class DataHelpers {

		public static BinaryReader NewReader(this Stream s) => new BinaryReader(s);
		public static BinaryWriter NewWriter(this Stream s) => new BinaryWriter(s);
		
		public static TextReader NewTextReader(this Stream s) => new StreamReader(s);
		public static TextWriter NewTextWriter(this Stream s) => new StreamWriter(s);

		public static void SeekTo(this BinaryReader reader, long position) =>
			reader.BaseStream.Seek(position, SeekOrigin.Begin);
		public static void SeekTo(this BinaryWriter writer, long position) =>
			writer.BaseStream.Seek(position, SeekOrigin.Begin);
	}
}
