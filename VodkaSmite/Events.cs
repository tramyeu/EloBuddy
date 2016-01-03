﻿using System;
using System.Drawing;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Color = SharpDX.Color;
using Font = System.Drawing.Font;
using FontColor = System.Drawing.Color;

namespace VodkaSmite
{
    public static class Events
    {
        private static Text Text;

        static Events()
        {
            Text = new Text("", new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold)) { Color = System.Drawing.Color.White };
            Drawing.OnDraw += OnDraw;
        }


        public static void Initialize()
        {
        }

        private static void OnDraw(EventArgs args)
        {
            if (Player.Instance.IsDead)
            {
                return;
            }
            if (Config.DrawSmiteRange)
            {
                Circle.Draw(Color.LightCyan, SpellManager.Smite.Range, Player.Instance.Position);
            }
            if (Config.DrawSmiteStatus)
            {
                var enabled = Config.SmiteEnabled || Config.SmiteEnabledToggle;
                Text.Position = Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(40, -40);
                Text.Color = enabled ? FontColor.LightSeaGreen : FontColor.DarkRed;
                Text.TextValue = enabled ? "Smite: ENABLED" : "Smite: disabled";
                Text.Draw();
            }
            if (Config.DrawSmiteable)
            {
                var monsters =
                    EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position, SpellManager.Smite.Range + 500.0f)
                        .Where(e => !e.IsDead && e.Health > 0 && !e.IsInvulnerable && e.IsVisible && e.Health < Damages.SmiteDmgMonster(e) && Util.MonstersNames.Contains(e.BaseSkinName));
                foreach (var monster in monsters)
                {
                    Circle.Draw(Color.Red, monster.BoundingRadius, monster.Position);
                }
            }
        }

    }
}
