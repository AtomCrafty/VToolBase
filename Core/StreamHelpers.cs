using System.IO;

namespace VToolBase.Core;

public static class StreamHelpers {
	public static void MoveData(this Stream stream, long srcOffset, long dstOffset, long size) {
		if(srcOffset == dstOffset) return;
		if(size == 0) return;

		const int blockSize = 4096;
		var buffer = new byte[blockSize];

		void MoveBlock(long src, long dst, int count) {
			stream.Seek(src, SeekOrigin.Begin);
			stream.Read(buffer, 0, count);
			stream.Seek(dst, SeekOrigin.Begin);
			stream.Write(buffer, 0, count);
		}

		int blocks = (int)(size / blockSize);
		int remaining = (int)(size % blockSize);

		if(srcOffset < dstOffset) {
			// moving forwards, so start at the end
			MoveBlock(srcOffset + blocks * blockSize, dstOffset + blocks * blockSize, remaining);
			for(int i = blocks - 1; i >= 0; i--) {
				MoveBlock(srcOffset + i * blockSize, dstOffset + i * blockSize, blockSize);
			}
		}
		else {
			// moving backwards, so start at the beginning
			for(int i = 0; i < blocks; i++) {
				MoveBlock(srcOffset + i * blockSize, dstOffset + i * blockSize, blockSize);
			}

			MoveBlock(srcOffset + blocks * blockSize, dstOffset + blocks * blockSize, remaining);
		}
	}
}