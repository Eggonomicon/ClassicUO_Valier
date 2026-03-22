using ClassicUO.Game.Managers;
using ClassicUO.Game.UI.Gumps.Valier;

namespace ClassicUO.Game.UI.Valier
{
    internal static class ValierDebugEntryPoints
    {
        public static void OpenPersistentChat()
        {
            UIManager.Add(new ValierPersistentChatGump(Client.Game.UO.World));
        }

        public static void OpenInventory()
        {
            UIManager.Add(new ValierInventoryGump(Client.Game.UO.World));
        }

        public static void OpenContainer()
        {
            UIManager.Add(new ValierContainerGump(Client.Game.UO.World));
        }

        public static void OpenSpellbook()
        {
            UIManager.Add(new ValierSpellbookGump(Client.Game.UO.World));
        }

        public static void OpenHotbar()
        {
            UIManager.Add(new ValierHotbarGump(Client.Game.UO.World));
        }
    }
}
