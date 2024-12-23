using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using TheBazaarCombatAccelerator.Components;
using UnityEngine;

namespace TheBazaarCombatAccelerator
{
    internal static class Assets
    {
        private static AssetBundle bundle;

        public static GameObject AccelerationControls { get; private set; }

        public static void LoadAssets()
        {
            string pluginPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string bundlePath = Path.Combine(pluginPath, "assets");
            bundle = AssetBundle.LoadFromFile(bundlePath);

            AccelerationControls = bundle.LoadAsset<GameObject>("Assets/Acceleration Controls.prefab");
            AccelerationControls.AddComponent<AccelerationController>();
        }
    }
}
