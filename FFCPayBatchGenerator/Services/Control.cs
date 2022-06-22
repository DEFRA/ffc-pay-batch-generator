namespace FFCPayBatchGenerator.Services
{
    public static class Control
    {
        public static string GetFileName(string filepath)
        {
            return filepath.Replace("/Files/", "/Files/CTL_");
        }
    }
}
