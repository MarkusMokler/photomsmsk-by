using System.IO;

namespace Extension
{
    public static class StreamExtensions
    {
        public static byte[] ReadToArray(this Stream stream)
        {
            if (stream.CanSeek)
                return stream.ReadBlock();
            else
                return stream.ReadSequence();
        }

        private static byte[] ReadBlock(this Stream stream)
        {
            if (stream.CanSeek)
                stream.Seek(0L, SeekOrigin.Begin);
            var buffer = new byte[stream.Length];
            int offset = 0;
            int length = buffer.Length;
            while (length > 0)
            {
                int num = stream.Read(buffer, offset, length);
                if (num == 0)
                    return null;
                length -= num;
                offset += num;
            }
            return buffer;
        }

        private static byte[] ReadSequence(this Stream stream)
        {
            if (stream.CanSeek)
                stream.Seek(0L, SeekOrigin.Begin);
            var buffer = new byte[32768];
            using (var memoryStream = new MemoryStream())
            {
                while (true)
                {
                    int count = stream.Read(buffer, 0, buffer.Length);
                    if (count > 0)
                        memoryStream.Write(buffer, 0, count);
                    else
                        break;
                }
                return memoryStream.ToArray();
            }
        }
    }

}