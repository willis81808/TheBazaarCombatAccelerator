using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheBazaarCombatAccelerator.Components
{
    internal class AccelerationController : MonoBehaviour
    {
        public static float SpeedFactor { get => timeFactorSettings[selectedTimeFactor].scalar; }

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

        private Button decreaseButton;
        private Button increaseButton;
        private TextMeshProUGUI displayText;

        void Awake()
        {
            displayText = gameObject.transform.Find("Display Panel").GetComponentInChildren<TextMeshProUGUI>();
            decreaseButton = gameObject.transform.Find("Decrease Button").GetComponentInChildren<Button>();
            increaseButton = gameObject.transform.Find("Increase Button").GetComponentInChildren<Button>();
        }

        void Start()
        {
            decreaseButton.onClick.AddListener(OnDecreaseClicked);
            increaseButton.onClick.AddListener(OnIncreaseClicked);
            UpdateText();
        }

        private void OnDecreaseClicked()
        {
            selectedTimeFactor = Math.Max(selectedTimeFactor - 1, 0);
            UpdateText();
        }

        private void OnIncreaseClicked()
        {
            selectedTimeFactor = Math.Min(selectedTimeFactor + 1, timeFactorSettings.Length - 1);
            UpdateText();
        }

        private void UpdateText()
        {
            displayText.text = timeFactorSettings[selectedTimeFactor].name;
        }
    }
}
