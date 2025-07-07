using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RimEffectN7
{
    [DefOf]
    public static class RimEffectN7DefOf
    {
        static RimEffectN7DefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(RimEffectN7DefOf));
        }

        public static VEF.Abilities.AbilityDef REN7_DarkChannel;

        public static HediffDef REN7_DarkChannelPainHediff;
    }
}
