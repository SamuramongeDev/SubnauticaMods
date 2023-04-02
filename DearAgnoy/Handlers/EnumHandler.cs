using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLib.Handlers
{
    public class EnumHandler
    {
        public static TechType AddTechType(string name)
        {
            return TechTypeHandler.AddTechType(name);
        }

        public static Base.Piece AddPiece(string name)
        {
            return PieceHandler.AddPiece(name);
        }

        public static TechType GetTechType(string name)
        {
            return TechTypeHandler.GetTechType(name);
        }

        public static Base.Piece GetPiece(string name) 
        {
            return PieceHandler.GetPiece(name);
        }

        public static Base.Piece TechTypeToPiece(string techTypeName)
        {
            TechType techType = GetTechType(techTypeName);
            Base.Piece piece = GetPiece(techTypeName);
            if ((int)techType == (int)piece)
            {
                return piece;
            }
            return Base.Piece.Invalid;
        }

        public static TechType PieceToTechType(string name)
        {
            Base.Piece piece = GetPiece(name);
            TechType techType = GetTechType(name);
            if ((int)piece == (int)techType)
            {
                return techType;
            }
            return TechType.None;
        }

        public static void RemoveTechType(string name)
        {
            TechTypeHandler.RemoveTechType(name);
        }

        public static void RemovePiece(string name)
        {
            PieceHandler.RemovePiece(name);
        }
    }
}
