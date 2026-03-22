// SPDX-License-Identifier: BSD-2-Clause

using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace ClassicUO.Game.UI.Valier
{
    internal static class ValierTextureCache
    {
        private static readonly Dictionary<ValierAssetId, Texture2D> _textures = new Dictionary<ValierAssetId, Texture2D>();

        public static bool TryGet(ValierAssetId id, out Texture2D texture)
        {
            if (_textures.TryGetValue(id, out texture) && texture != null && !texture.IsDisposed)
            {
                return true;
            }

            string fullPath = ValierUIRuntimePaths.GetAbsolutePath(id);

            if (string.IsNullOrEmpty(fullPath) || !File.Exists(fullPath) || Client.Game?.GraphicsDevice == null)
            {
                texture = null;
                return false;
            }

            using (FileStream stream = File.OpenRead(fullPath))
            {
                texture = Texture2D.FromStream(Client.Game.GraphicsDevice, stream);
            }

            _textures[id] = texture;
            return texture != null;
        }

        public static void Clear()
        {
            foreach (Texture2D texture in _textures.Values)
            {
                texture?.Dispose();
            }

            _textures.Clear();
        }
    }
}
