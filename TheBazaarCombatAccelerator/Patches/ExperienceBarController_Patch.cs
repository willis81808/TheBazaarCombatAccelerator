using HarmonyLib;
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
            GameObject instance = GameObject.Instantiate(Assets.AccelerationControls, __instance.gameObject.transform);
            __instance.gameObject.GetComponent<GraphicRaycaster>().enabled = true;
        }
    }
}