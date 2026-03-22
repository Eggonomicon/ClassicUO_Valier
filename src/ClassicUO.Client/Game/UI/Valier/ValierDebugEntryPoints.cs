using ClassicUO.Game.Managers;
using ClassicUO.Game.UI.Gumps.Valier;

namespace ClassicUO.Game.UI.Valier
{
    internal static class ValierDebugEntryPoints
    {
        public static void TogglePersistentChat()
        {
            var existing = UIManager.GetGump<ValierPersistentChatGump>();

            if (existing != null)
            {
                existing.Dispose();
            }
            else
            {
                UIManager.Add(new ValierPersistentChatGump(Client.Game.UO.World));
            }
        }

        public static void ToggleInventory()
        {
            var existing = UIManager.GetGump<ValierInventoryGump>();

            if (existing != null)
            {
                existing.Dispose();
            }
            else
            {
                UIManager.Add(new ValierInventoryGump(Client.Game.UO.World));
            }
        }

        public static void ToggleContainer()
        {
            var existing = UIManager.GetGump<ValierContainerGump>();

            if (existing != null)
            {
                existing.Dispose();
            }
            else
            {
                UIManager.Add(new ValierContainerGump(Client.Game.UO.World));
            }
        }

        public static void ToggleSpellbook()
        {
            var existing = UIManager.GetGump<ValierSpellbookGump>();

            if (existing != null)
            {
                existing.Dispose();
            }
            else
            {
                UIManager.Add(new ValierSpellbookGump(Client.Game.UO.World));
            }
        }

        public static void ToggleHotbar()
        {
            var existing = UIManager.GetGump<ValierHotbarGump>();

            if (existing != null)
            {
                existing.Dispose();
            }
            else
            {
                UIManager.Add(new ValierHotbarGump(Client.Game.UO.World));
            }
        }
    }
}
