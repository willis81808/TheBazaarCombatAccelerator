using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TheBazaar.UI.Components;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace TheBazaarCombatAccelerator.Patches
{
    [HarmonyPatch(typeof(ExperienceBarController))]
    [HarmonyPatch(nameof(ExperienceBarController.Init))]
    internal class ExperienceBarController_Patch
    {
        [HarmonyPostfix]
        static void Postfix(ExperienceBarController __instance)
        {
            Assert.IsNotNull(Assets.AccelerationControls);
            GameObject instance = GameObject.Instantiate(Assets.AccelerationControls, __instance.gameObject.transform);
            Plugin.LOGGER.LogWarning("Spawned Acceleration Controls!");

            __instance.gameObject.GetComponent<GraphicRaycaster>().enabled = true;
        }
    }
}