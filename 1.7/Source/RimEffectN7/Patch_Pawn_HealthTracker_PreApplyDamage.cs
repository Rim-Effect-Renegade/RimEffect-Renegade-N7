using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RimEffectN7
{
    [HarmonyPatch(typeof(Pawn_HealthTracker), "PreApplyDamage")]
    public static class Patch_Pawn_HealthTracker_PreApplyDamage
    {
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator ilg)
        {
            MethodInfo spawnedInfo = AccessTools.PropertyGetter(typeof(Thing), nameof(Pawn.Spawned));
            bool foundFirst = false;

            Label label = ilg.DefineLabel();

            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.Calls(spawnedInfo))
                    if (!foundFirst)
                        foundFirst = true;
                    else
                    {
                        yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Patch_Pawn_HealthTracker_PreApplyDamage), nameof(Absorb)))
                        {
                            labels = instruction.ExtractLabels()
                        };
                        yield return new CodeInstruction(OpCodes.Brfalse_S, label);
                        yield return new CodeInstruction(OpCodes.Ldarg_2);
                        yield return new CodeInstruction(OpCodes.Ldc_I4_1);
                        yield return new CodeInstruction(OpCodes.Stind_I1);
                        yield return new CodeInstruction(OpCodes.Ret);
                        yield return new CodeInstruction(OpCodes.Ldarg_0) { labels = new List<Label> { label } };
                        yield return new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(Pawn_HealthTracker), "pawn"));
                    }

                yield return instruction;
            }
        }

        public static bool Absorb(Pawn pawn) => pawn.equipment?.Primary?.def.GetModExtension<DeflectExtension>()?.Deflected ?? false;
    }
}
