using System;
using System.Collections.Generic;
using SMLHelper.V2.Json;
using SMLHelper.V2.Options;
using SMLHelper.V2.Options.Attributes;

namespace MoUpgradesBepInEx
{

    [Menu("Mo' Upgrades")]
    public class Settings : ConfigFile
    {
        [Toggle("Toggle In Game Messages", Tooltip = "If checked, shows a message everytime you change a module."), OnChange(nameof(OnToggle))]
        public bool Toggle;

        public bool InGameMessage;

        private void OnToggle(ToggleChangedEventArgs e)
        {
            Main.logSource.LogInfo($"Value: {e.Value}");
            InGameMessage = e.Value;
        }
    }
}
