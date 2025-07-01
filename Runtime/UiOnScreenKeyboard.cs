using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace GPUI
{
    public class UiOnScreenKeyboard : UiWindowModal
    {

        [TabGroup("Tabs", "UI Elements")] public GameObject keyboardRowPrefab;

        [TabGroup("Tabs", "UI Elements")] public GameObject keyboardButtonPrefab;

        [TabGroup("Tabs", "UI Elements")] public TextMeshProUGUI displayNameText;

        public List<KeyboardRow> keyboardRows = new List<KeyboardRow>();

        [SerializeField] string enteredRawText;

        public UnityEvent<string> OnTextSubmit;


        public string EnteredRawText
        {
            get { return enteredRawText; }
            set { enteredRawText = value; }
        }


        private void Start()
        {

            GenerateButtons();

        }

        public override void ApplySkinData()
        {

            base.ApplySkinData();

        }

        void GenerateButtons()
        {

            for (int i = 0; i < keyboardRows.Count; i++)
            {

                int rowIndex = i;

                GameObject newRow = Instantiate(keyboardRowPrefab, windowBody);

                UiElement rowElement = newRow.GetComponent<UiElement>();


                for (int j = 0; j < keyboardRows[i].keys.Count; j++)
                {

                    int keyIndex = j;

                    GameObject newKey = Instantiate(keyboardButtonPrefab, newRow.transform);
                    UiButton keyButton = newKey.GetComponent<UiButton>();

                    (keyButton.detailGraphic as TextMeshProUGUI).text = keyboardRows[rowIndex].keys[keyIndex].ToUpper();

                    switch (keyboardRows[rowIndex].keys[keyIndex])
                    {

                        default:
                            (keyButton.detailGraphic as TextMeshProUGUI).text =
                                keyboardRows[rowIndex].keys[keyIndex].ToUpper();
                            break;
                        case "backspace":
                            (keyButton.detailGraphic as TextMeshProUGUI).text = "←";
                            break;
                        case "enter":
                            (keyButton.detailGraphic as TextMeshProUGUI).text = "→";
                            break;
                        case "space":
                            (keyButton.detailGraphic as TextMeshProUGUI).text = "_";
                            break;

                    }


                    Function OnKeyPressed = new Function
                    {
                        functionDelay = 0,
                        functionName = new UnityEvent { }
                    };
                    OnKeyPressed.functionName.AddListener(() =>
                    {
                        AddCharacterToString(keyboardRows[rowIndex].keys[keyIndex]);
                    });

                    keyButton.onClickFunctions.Add(OnKeyPressed);

                }

            }

        }

        void AddCharacterToString(string newChar)
        {

            switch (newChar)
            {

                default:
                    enteredRawText += newChar;
                    break;
                case "backspace":
                    enteredRawText = enteredRawText.Substring(0, enteredRawText.Length - 1);
                    break;
                case "enter":
                    OnTextSubmit?.Invoke(enteredRawText);
                    break;
                case "space":
                    enteredRawText += " ";
                    break;

            }

            displayNameText.text = enteredRawText;

        }

    }

    [Serializable]
    public class KeyboardRow
    {

        public List<string> keys = new List<string>();

    }
}