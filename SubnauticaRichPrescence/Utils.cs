using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordRPCBepInEx
{

    public class Utils
    {
        public static string GetBiomeStringForImage(string wantedbiome)
        {
            foreach (string biome in biomesReferences)
            {
                bool flag = wantedbiome.Contains(biome);
                if (flag)
                {
                    return biome;
                }
            }
            return "subnauticalogo";
        }

        public static string GetCurrentSubForImage(string sub) 
        {
            foreach (string subs in vehicleReferences)
            {
                bool flag = subs.Contains(sub);
                if (flag) 
                { 
                    return subs;
                }  
            }
            return "placeholder";
        }

        public static string GetBiomeDisplayName(string biomereference)
        {
            foreach (KeyValuePair<string, string> valuePair in biomeDisplays)
            {
                if (valuePair.Key.Contains(biomereference))
                {
                    return valuePair.Value;
                }
            }
            return "Void";
        }


        public static string[] vehicleReferences = new string[3]
        {
            "cyclops",
            "prawn",
            "seamoth"
        };

        public static string[] biomesReferences = new string[20]
        {
            "safeshallows",
            "kelpforest",
            "grassyplateaus",
            "mushroomforest",
            "sparsereef",
            "grandreef",
            "dunes",
            "crash",
            "koosh",
            "lava",
            "lostriver",
            "ilz",
            "underwaterislands",
            "treecove",
            "floatingilsand",
            "mountains",
            "seatrader",
            "jellyshroomcaves",
            "bloodkelp",
            "lifepod"
        };

        public static Dictionary<string, string> biomeDisplays = new Dictionary<string, string>()
        {
            { "safeshallows", "Safe Shallows" },
            { "kelpforest" , "Kelp Forest"},
            { "grassyplateaus", "Grassy Plateaus" },
            { "mushroomforest", "Mushroom Forest" },
            { "sparsereef", "Sparse Reef" },
            { "grandreef", "Grand Reef" },
            { "dunes", "Dunes" },
            { "crashzone", "Crash Zone" },
            { "crashedship", "Crash Zone"},
            { "koosh", "Koosh Zone" },
            { "lava", "Lava Zone" },
            { "lostriver", "Lost River" },
            { "ilz", "Inactive Lava Zone" },
            { "underwaterislands", "Unerwater Islands" },
            { "treecove", "Giant Tree" },
            { "floatingisland", "Floating Island" },
            { "mountains", "Mountains" },
            { "seatrader", "Sea Traders Path" },
            { "jellyshroomcaves", "Jellyshroom Caves" },
            { "bloodkelp", "Blood Kelp" },
            { "lifepod", "Lifepod" }
        };
    }
}
