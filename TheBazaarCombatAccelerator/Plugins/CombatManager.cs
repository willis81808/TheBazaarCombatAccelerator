using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TheBazaar;
using System.Linq;
using BazaarGameShared.Infra.Messages.CombatSimEvents;

namespace TheBazaarCombatAccelerator.Plugins
{
    internal class CombatManager : MonoBehaviour
    {
        public static float SpeedFactor { get => timeFactorSettings[selectedTimeFactor].scalar; }

        private bool inCombat = false;
        private float cachedTimeScale = 1f;

        private static int selectedTimeFactor = 0;
        private static TimeFactorSetting[] timeFactorSettings = {
            new TimeFactorSetting("1x", 1),
            new TimeFactorSetting("2x", 2),
            new TimeFactorSetting("3x", 3),
            new TimeFactorSetting("5x", 5),
            new TimeFactorSetting("10x", 10),
        };

        private struct TimeFactorSetting
        {
            public string name;
            public float scalar;

            public TimeFactorSetting(string name, float scalar)
            {
                this.name = name;
                this.scalar = scalar;
            }
        }

        void OnEnable()
        {
            Events.CombatStarted.AddListener(OnCombatStarted, this);
            Events.CombatantDied.AddListener(OnCombatEnded, this);
        }

        void OnDisable()
        {
            Events.CombatStarted.RemoveListener(OnCombatStarted);
            Events.CombatantDied.RemoveListener(OnCombatStarted);
        }

        void Update()
        {
            if (inCombat)
            {
                Time.timeScale = cachedTimeScale * SpeedFactor;
            }
        }

        void OnGUI()
        {
            var gameManager = Singleton<GameServiceManager>.Instance;

            GUILayout.Label($"Speed factor: {timeFactorSettings[selectedTimeFactor].name}");
            selectedTimeFactor = GUILayout.SelectionGrid(selectedTimeFactor, timeFactorSettings.Select(tfs => tfs.name).ToArray(), timeFactorSettings.Length);
        }

        private void OnCombatStarted(object sender)
        {
            cachedTimeScale = Time.timeScale;
            inCombat = true;
        }

        private void OnCombatEnded(object sender)
        {
            Time.timeScale = cachedTimeScale;
            inCombat = false;
        }
    }
}
