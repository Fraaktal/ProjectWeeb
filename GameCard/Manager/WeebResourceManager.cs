using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ProjectWeeb.GameCard.Business;
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

        public Dictionary<int, HashSet<Effect>> GetEffectsByIdCardsFromFile()
        {
            Dictionary<int, HashSet<Effect>> result = new Dictionary<int, HashSet<Effect>>();
            var resource = Assembly.GetExecutingAssembly().GetManifestResourceNames().FirstOrDefault(r => r.Contains($"ProjectResources.Effects.EffectsList.eft"));
            if (resource != null)
            {
                var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);
                if (stream != null)
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        string line;

                        while ((line = sr.ReadLine()) != null)
                        {
                            var effectbyIdCard = ComputeLineToEffect(line);

                            if (effectbyIdCard.Key == -1)
                            {
                                continue;
                            }
                            
                            if (result.ContainsKey(effectbyIdCard.Key))
                            {
                                result[effectbyIdCard.Key].Add(effectbyIdCard.Value);
                            }
                            else
                            {
                                HashSet<Effect> efs = new HashSet<Effect> { effectbyIdCard.Value };
                                result.Add(effectbyIdCard.Key, efs);
                            }
                        }
                        
                    }
                }
            }

            return result;
        }

        //Formalisme : nomAttaque|descriptionAttaque|idAttaque|idCarte
        private KeyValuePair<int,Effect> ComputeLineToEffect(string line)
        {
            string[] values = line.Split("|");
            if (values.Length == 5)
            {
                string name = values[0];
                string description = values[1];
                string idAttackS = values[2];
                string idCardS = values[3];
                string typeS = values[4];

                int.TryParse(idAttackS, out int idAttack);
                int.TryParse(idCardS, out int idCard);
                int.TryParse(typeS, out int type);

                Effect effect = new Effect(name,description,idAttack, (Effect.TypeOfEffect)type);
                return new KeyValuePair<int, Effect>(idCard, effect);
            }

            return new KeyValuePair<int, Effect>(-1,null);
        }
    }
}
