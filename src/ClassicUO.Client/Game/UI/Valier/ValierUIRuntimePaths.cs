using System.IO;

namespace ClassicUO.Game.UI.Valier
{
    internal static class ValierUIRuntimePaths
    {
        public static string GetRoot()
        {
            return Path.Combine(CUOEnviroment.ExecutablePath, "Data", "Client", "ValierUI");
        }

        public static string GetAbsoluteAssetPath(string relativePath)
        {
            return Path.Combine(GetRoot(), relativePath);
        }

        public static bool TryResolve(ValierAssetId assetId, out string absolutePath)
        {
            absolutePath = null;

            if (!ValierAssetCatalog.TryGetRelativePath(assetId, out string relativePath))
            {
                return false;
            }

            absolutePath = GetAbsoluteAssetPath(relativePath);
            return true;
        }
    }
}
