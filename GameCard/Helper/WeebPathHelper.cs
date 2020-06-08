using System.IO;

namespace ProjectWeeb.GameCard.Helper
{
    public static class WeebPathHelper
    {
        public static string DatabasePath
        {
            get
            {
                string path = Path.Combine(Path.GetTempPath(), "ProjectWeeb", "LiteDb");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return Path.Combine(path, "Weeb.db");
            }
        }

        public static string CardsImagePath
        {
            get
            {
                string path = Path.Combine("src", "Cards");

                return path;
            }
        }
    }
}