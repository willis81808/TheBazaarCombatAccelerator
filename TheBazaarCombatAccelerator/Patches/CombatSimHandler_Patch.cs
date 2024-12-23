using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using TheBazaar;
using TheBazaarCombatAccelerator.Components;
using TheBazaarCombatAccelerator.Plugins;

namespace TheBazaarCombatAccelerator.Patches
{
    [HarmonyPatch]
    class CombatSimHandler_Patch
    {
        static MethodBase TargetMethod()
        {
            var stateMachineType = typeof(CombatSimHandler).GetNestedTypes(BindingFlags.NonPublic)
                .FirstOrDefault(t => t.Name.Contains("<HandleMessage>"));

            if (stateMachineType == null)
            {
                Plugin.LOGGER.LogError("Could not find state machine type!");
                return null;
            }

            return stateMachineType.GetMethod("MoveNext", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public static int GetSimulationDelay()
        {
            return (int) (50 / AccelerationController.SpeedFactor);
        }

        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = instructions.ToList();

            for (int i = 0; i < codes.Count - 2; i++)
            {
                if (codes[i].opcode == OpCodes.Ldc_I4_S &&
                    (sbyte)codes[i].operand == 50 &&
                    codes[i + 1].opcode == OpCodes.Stloc_S &&
                    codes[i + 2].opcode == OpCodes.Ldarg_0)
                {
                    codes[i] = new CodeInstruction(OpCodes.Call,
                        typeof(CombatSimHandler_Patch).GetMethod(nameof(GetSimulationDelay)));
                }
            }

            foreach (var instruction in codes)
            {
                yield return instruction;
            }
        }
    }
}