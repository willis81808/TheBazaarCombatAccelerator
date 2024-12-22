using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using TheBazaarCombatAccelerator.Plugins;
using UnityEngine;

namespace TheBazaarCombatAccelerator
{
    [BepInPlugin(MOD_ID, MOD_NAME, MOD_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public const string MOD_ID = "com.willis.thebazaar.combataccelerator";
        public const string MOD_NAME = "Combat Accelerator";
        public const string MOD_VERSION = "0.0.1";

        public static ManualLogSource LOGGER { get; private set; }

        void Awake()
        {
            LOGGER = Logger;

            var combatUtils = new GameObject("Combat Utils");
            combatUtils.AddComponent<CombatManager>();
            combatUtils.hideFlags = HideFlags.HideAndDontSave;
            DontDestroyOnLoad(combatUtils);

            LOGGER.LogWarning("Combat Accelerator Started!");

            Harmony.CreateAndPatchAll(typeof(Plugin).Assembly);
            LOGGER.LogWarning("Patches Applied!");
        }
    }
}
