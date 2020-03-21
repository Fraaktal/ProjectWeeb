using System;
using System.IO;
using System.Linq;
using System.Reflection;
using ProjectWeeb.GameCard.Helper;

namespace ProjectWeeb.GameCard.Manager
{
    public class WeebResourceManager
    {
        private static WeebResourceManager _instance;

        private WeebResourceManager()
        {

        }

        public static WeebResourceManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new WeebResourceManager();
            }

            return _instance;
        }


        public string GetCardImageByCardId(int id)
        {
            string path = Path.Combine(WeebPathHelper.CardsImagePath, $"{id}.png");

            if (!File.Exists(path))
            {
                BuildImagesFromAssemblyById(id);
            }

            return path;
        }

        private void BuildImagesFromAssemblyById(int id)
        {
            var resource = Assembly.GetExecutingAssembly().GetManifestResourceNames()
                .FirstOrDefault(r => r.Contains($"ProjectResources.Cards.{id}"));
            if (resource != null)
            {
                var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);

                if (stream != null)
                {
                    string filePath = Path.Combine(WeebPathHelper.CardsImagePath, $"{id}.png");

                    try
                    {
                        using (var fileStream = File.Create(filePath))
                        {
                            stream.Seek(0, SeekOrigin.Begin);
                            stream.CopyTo(fileStream);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    
                }
            }
        }
    }
}
