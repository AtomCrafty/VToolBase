using System.Globalization;
using System.IO;
using System.Text;
using CsvHelper;
using ISM.Util;

namespace VToolBase.Core;

public static class DataHelpers {

	public static BinaryReader NewReader(this Stream s, bool leaveOpen = false) => new BinaryReader(s, Encoding.UTF8, leaveOpen);
	public static BinaryWriter NewWriter(this Stream s, bool leaveOpen = false) => new BinaryWriter(s, Encoding.UTF8, leaveOpen);

	public static TextReader NewTextReader(this Stream s, bool leaveOpen = false) => new StreamReader(s, Encoding.UTF8, leaveOpen);
	public static TextWriter NewTextWriter(this Stream s, bool leaveOpen = false) => new StreamWriter(s, Encoding.UTF8, 1024, leaveOpen);

	public static StreamColorWriter NewColorWriter(this TextWriter w) => new StreamColorWriter(w);
	public static StreamColorWriter NewColorWriter(this Stream s) => s.NewTextWriter().NewColorWriter();

	public static CsvReader NewCsvReader(this TextReader r) => new CsvReader(r, CultureInfo.InvariantCulture);
	public static CsvReader NewCsvReader(this Stream s) => s.NewTextReader().NewCsvReader();

	public static CsvWriter NewCsvWriter(this TextWriter w) => new CsvWriter(w, CultureInfo.InvariantCulture);
	public static CsvWriter NewCsvWriter(this Stream s) => s.NewTextWriter().NewCsvWriter();

	public static void SeekTo(this BinaryReader reader, long position) =>
		reader.BaseStream.Seek(position, SeekOrigin.Begin);
	public static void SeekTo(this BinaryWriter writer, long position) =>
		writer.BaseStream.Seek(position, SeekOrigin.Begin);
}