using System;
using TheBazaar;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheBazaarCombatAccelerator.Components
{
    public class AccelerationController : MonoBehaviour
    {
        public static float SpeedFactor { get => speedFactorSettings[selectedSpeedFactor].scalar; }

        private static int selectedSpeedFactor = 0;
        private static SpeedFactorSetting[] speedFactorSettings = {
            new SpeedFactorSetting("1x", 1),
            new SpeedFactorSetting("2x", 2),
            new SpeedFactorSetting("3x", 3),
            new SpeedFactorSetting("5x", 5),
        };

        private struct SpeedFactorSetting
        {
            public string name;
            public float scalar;

            public SpeedFactorSetting(string name, float scalar)
            {
                this.name = name;
                this.scalar = scalar;
            }
        }

        private Button decreaseButton;
        private Button increaseButton;
        private TextMeshProUGUI displayText;

        private bool inCombat = false;
        private float cachedTimeScale = 1f;

        private void Awake()
        {
            displayText = gameObject.transform.Find("Display Panel").GetComponentInChildren<TextMeshProUGUI>();
            decreaseButton = gameObject.transform.Find("Decrease Button").GetComponentInChildren<Button>();
            increaseButton = gameObject.transform.Find("Increase Button").GetComponentInChildren<Button>();
        }

        private void Start()
        {
            decreaseButton.onClick.AddListener(OnDecreaseClicked);
            increaseButton.onClick.AddListener(OnIncreaseClicked);
            UpdateText();
        }

        private void OnEnable()
        {
            Events.CombatStarted.AddListener(OnCombatStarted, this);
            Events.CombatantDied.AddListener(OnCombatEnded, this);
        }

        private void OnDisable()
        {
            Events.CombatStarted.RemoveListener(OnCombatStarted);
            Events.CombatantDied.RemoveListener(OnCombatStarted);
        }

        private void Update()
        {
            if (!inCombat) return;
            Time.timeScale = cachedTimeScale * SpeedFactor;
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

        private void OnDecreaseClicked()
        {
            selectedSpeedFactor = Math.Max(selectedSpeedFactor - 1, 0);
            UpdateText();
        }

        private void OnIncreaseClicked()
        {
            selectedSpeedFactor = Math.Min(selectedSpeedFactor + 1, speedFactorSettings.Length - 1);
            UpdateText();
        }

        private void UpdateText()
        {
            displayText.text = speedFactorSettings[selectedSpeedFactor].name;
        }
    }
}
