using System.Text;
namespace VToolBase.Core;

public static class DataHelpers {

	public static BinaryReader NewReader(this Stream s, bool leaveOpen = false) => new BinaryReader(s, Encoding.UTF8, leaveOpen);
	public static BinaryWriter NewWriter(this Stream s, bool leaveOpen = false) => new BinaryWriter(s, Encoding.UTF8, leaveOpen);

	public static TextReader NewTextReader(this Stream s, bool leaveOpen = false) => new StreamReader(s, Encoding.UTF8, leaveOpen);
	public static TextWriter NewTextWriter(this Stream s, bool leaveOpen = false) => new StreamWriter(s, Encoding.UTF8, 1024, leaveOpen);

	public static void SeekTo(this BinaryReader reader, long position) =>
		reader.BaseStream.Seek(position, SeekOrigin.Begin);
	public static void SeekTo(this BinaryWriter writer, long position) =>
		writer.BaseStream.Seek(position, SeekOrigin.Begin);
}