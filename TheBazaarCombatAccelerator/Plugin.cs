using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;

namespace TheBazaarCombatAccelerator
{
    [BepInPlugin(MOD_ID, MOD_NAME, MOD_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public const string MOD_ID = "com.willis.thebazaar.combataccelerator";
        public const string MOD_NAME = "Combat Accelerator";
        public const string MOD_VERSION = "1.0.0";

        public static ManualLogSource LOGGER { get; private set; }

        private void Awake()
        {
            LOGGER = Logger;

            Assets.LoadAssets();
            LOGGER.LogWarning("Assets Loaded!");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
            LOGGER.LogWarning("Patches Applied!");
        }
    }
}
