using System;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PraeyMod
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class Class1 : BaseUnityPlugin
    {
        private const string ModName = "PraeyMod";
        private const string ModVersion = "1.0";
        private const string ModGUID = "com.zarboz.PraeyMod";
        internal static float StamRecover;
        internal static float StamDelay;
        internal static ConfigEntry<float> StaminaRecoverDelay;
        internal static ConfigEntry<float> StaminaRate;
        internal static ConfigEntry<float> DashStam;
        internal static ConfigEntry<float> LeapStam;
        private static ConfigEntry<float> JumpStam;
        private void Awake()
        {
            Harmony.CreateAndPatchAll(typeof(TestPatch).Assembly);
            StaminaRate = Config.Bind("Stamina Patches", "Stamina Rate", 100f,
                "The recovery rate of stamina on your hero ");
            StaminaRecoverDelay = Config.Bind("Stamina Patches", "Stamina Recharge Delay", 0f,
                "The delay before your hero's stamina starts recharging");
            DashStam = Config.Bind("Stamina Patches", "Dash stamina usage", 0f,
                "The volume of stamina your hero uses to dash");
            LeapStam = Config.Bind("Stamina Patches", "Leap stamina usage", 0f,
                "The volume of stamina your hero uses to leap");
            JumpStam = Config.Bind("Stamina Patches", "Jump stamina Usage", 0f,
                "The volume of stamina your hero uses to jump");
        }

        [HarmonyPatch(typeof(Player), "Awake")]
        public static class TestPatch
        {
            public static void Postfix(Player __instance)
            {
                __instance.m_StaminaRecoverDelay = StaminaRecoverDelay.Value;
                __instance.m_StaminaRecoverRate = StaminaRate.Value;
                __instance.m_DashStamina = DashStam.Value;
                __instance.m_LeapStamina = LeapStam.Value;
                __instance.jumpStamina = JumpStam.Value;
            }
        }
    }
}