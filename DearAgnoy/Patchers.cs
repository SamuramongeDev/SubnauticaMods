namespace BaseLib
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using BaseLib.Handlers;
    using HarmonyLib;
    using UnityEngine;

    [HarmonyPatch(typeof(Base))]
    public class BaseClassPatch
    {
        [HarmonyPatch(nameof(Base.Initialize))]
        [HarmonyPostfix]
        public static void InitializePatch()
        {
            PatchUsables.DefinePiecesAsync();
        }
    }
    
    public class PatchUsables
    {
        public static readonly Int3 extraCells = new Int3(2, 0, 2);
        public static readonly Int3 extraCells2 = new Int3(3, 0, 2);

        public static AssetBundleList assetBundleList = Main.bundles;

        public static IEnumerator DefinePiecesAsync()
        {
            Base.pieces.AddItem(new Base.PieceDef(assetBundleList.GetAsset<GameObject>("PrecursorITube"), extraCells, Quaternion.identity));
            yield return null;
            Base.corridors.AddItem(new Base.CorridorDef(EnumHandler.GetPiece("PrecursorITube"), Base.Piece.CorridorIShapeSupport, Base.Piece.CorridorIShapeAdjustableSupport, EnumHandler.GetTechType("PrecursorITube")));
        }        
    }
}
