using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FFCPayBatchGenerator.Services
{
    public static class Checksum
    {
        public static string Generate(string filename)
        {
            using var md5 = MD5.Create();
            using var stream = File.OpenRead(filename);
            var hash = md5.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        public static string GetFileName(string filepath)
        {
            return Path.ChangeExtension(filepath, "txt");
        }
    }
}
