﻿using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using Settings = VodkaTristana.Config.ModesMenu.JungleClear;
using SettingsMana = VodkaTristana.Config.ManaManagerMenu;
using SettingsPrediction = VodkaTristana.Config.PredictionMenu;

namespace VodkaTristana.Modes
{
    public sealed class JungleClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
        }

        public override void Execute()
        {
            if (Settings.UseW & W.IsReady() && PlayerMana >= SettingsMana.MinWMana)
            {
                var monsters = EntityManager.MinionsAndMonsters.GetJungleMonsters(_PlayerPos, W.Range + W.Width);
                var farmLoc = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(monsters, W.Width, (int) W.Range);
                if (farmLoc.HitNumber >= Settings.MinWTargets)
                {
                    W.Cast(farmLoc.CastPosition);
                    Debug.WriteChat("Casting W in JungleClear on {0} targets.", farmLoc.HitNumber.ToString());
                }
            }
        }
    }
}

