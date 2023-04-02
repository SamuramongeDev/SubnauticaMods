using SMLHelper.V2.Handlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLib.Handlers
{
    public class TechTypeHandler
    {
        public static readonly EnumAccesor<TechType> accesor;

        public static TechType AddTechType(string name)
        {
            return accesor.AddValue(name);
        }

        public static TechType GetTechType(string name)
        {
            return accesor.GetValue(name);
        }

        public static TechType GetTechType(TechType value)
        {
            return accesor.GetValue(value);
        }

        public static void RemoveTechType(string name)
        {
            accesor.RemoveValue(name);
        }

        static TechTypeHandler()
        {
            accesor = new EnumAccesor<TechType>();
            accesor.startingIndex += 15000;
        }
    }
}
