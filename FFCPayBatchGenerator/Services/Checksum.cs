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
            using var sha256 = SHA256.Create();
            using var stream = File.OpenRead(filename);
            var hash = sha256.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        public static string GetFileName(string filepath)
        {
            return Path.ChangeExtension(filepath, "txt");
        }
    }
}
