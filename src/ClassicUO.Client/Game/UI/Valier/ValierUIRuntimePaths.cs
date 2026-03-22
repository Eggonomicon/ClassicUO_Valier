// SPDX-License-Identifier: BSD-2-Clause

using System.IO;

namespace ClassicUO.Game.UI.Valier
{
    internal static class ValierUIRuntimePaths
    {
        public static string BasePath => Path.Combine(CUOEnviroment.ExecutablePath, "Data", "Client", "ValierUI");

        public static string GetAbsolutePath(ValierAssetId id)
        {
            string relative = ValierAssetCatalog.GetRelativePath(id);

            if (string.IsNullOrEmpty(relative))
            {
                return null;
            }

            return Path.Combine(BasePath, relative);
        }
    }
}
