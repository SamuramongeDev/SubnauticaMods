namespace BaseLib.Handlers
{
    public class PieceHandler
    {
        public static readonly EnumAccesor<Base.Piece> accesor;

        public static Base.Piece AddPiece(string name)
        {
            return accesor.AddValue(name); ;
        }

        public static Base.Piece GetPiece(string name)
        {
            return accesor.GetValue(name);
        }

        public static void RemovePiece(string name)
        {
            accesor.RemoveValue(name);
        }

        static PieceHandler()
        {
            accesor = new EnumAccesor<Base.Piece>();
        }
    }
}
