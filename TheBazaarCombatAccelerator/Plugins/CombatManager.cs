using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TheBazaar;
using System.Linq;
using BazaarGameShared.Infra.Messages.CombatSimEvents;
using TheBazaarCombatAccelerator.Components;

namespace TheBazaarCombatAccelerator.Plugins
{
    internal class CombatManager : MonoBehaviour
    {
        private bool inCombat = false;
        private float cachedTimeScale = 1f;

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
                Time.timeScale = cachedTimeScale * AccelerationController.SpeedFactor;
            }
        }

        //void OnGUI()
        //{
        //    var gameManager = Singleton<GameServiceManager>.Instance;

        //    GUILayout.Label($"Speed factor: {timeFactorSettings[selectedTimeFactor].name}");
        //    selectedTimeFactor = GUILayout.SelectionGrid(selectedTimeFactor, timeFactorSettings.Select(tfs => tfs.name).ToArray(), timeFactorSettings.Length);
        //}

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
