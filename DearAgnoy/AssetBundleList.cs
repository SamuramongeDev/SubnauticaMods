namespace BaseLib
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class AssetBundleList
    {
        public Dictionary<AssetBundle, string> bundles;
        public AssetBundle MainBundle { get; private set; }

        public void AddAssetBundle(AssetBundle assetBundle)
        {
            if (bundles.Count == 1)
            {
                MainBundle = assetBundle;
            }
            bundles.Add(assetBundle, assetBundle.name);
        }

        public void RemoveAssetBundle(string name)
        {
            AssetBundle assetBundle = GetAssetBundleWithName(name);
            bundles.Remove(assetBundle);
        }

        public AssetBundle GetAssetBundleWithName(string name)
        {
            foreach (var keyValuePair in bundles)
            {
                string bundleName = keyValuePair.Value;
                AssetBundle bundle = keyValuePair.Key;

                if (bundleName == name)
                {
                    return bundle;
                }
            }
            return null;
        }

        
        public T GetAsset<T>(string name) where T : UnityEngine.Object
        {
            foreach (var keyValuePair in bundles)
            {
                AssetBundle assetBundle = keyValuePair.Key;
                string[] assetNames = assetBundle.GetAllAssetNames();

                foreach (string assetName in assetNames)
                {
                    if (assetName == name)
                    {
                        return assetBundle.LoadAsset<T>(assetName);
                    }
                }
            }
            return null;
        }

        public UnityEngine.Object GetAsset(string name)
        {
            foreach (var keyValuePair in bundles)
            {
                AssetBundle assetBundle = keyValuePair.Key;
                string[] assetNames = assetBundle.GetAllAssetNames();

                foreach (string assetName in assetNames)
                {
                    if (assetName == name)
                    {
                        return assetBundle.LoadAsset(assetName);
                    }
                }
            }
            return null;
        }

        public AssetBundleList()
        {
            bundles = new Dictionary<AssetBundle, string>();
        }
    }
}
