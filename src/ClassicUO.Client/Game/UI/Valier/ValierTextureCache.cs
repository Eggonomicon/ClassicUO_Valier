using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace ClassicUO.Game.UI.Valier
{
    internal static class ValierTextureCache
    {
        private static readonly Dictionary<string, Texture2D> _cache = new();

        public static bool TryGet(ValierAssetId assetId, out Texture2D texture)
        {
            texture = null;

            if (!ValierUIRuntimePaths.TryResolve(assetId, out string path) || string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                return false;
            }

            if (_cache.TryGetValue(path, out texture) && texture != null)
            {
                return true;
            }

            using FileStream stream = File.OpenRead(path);
            texture = Texture2D.FromStream(Client.Game.GraphicsDevice, stream);
            _cache[path] = texture;

            return true;
        }

        public static void Clear()
        {
            foreach (Texture2D texture in _cache.Values)
            {
                texture?.Dispose();
            }

            _cache.Clear();
        }
    }
}
