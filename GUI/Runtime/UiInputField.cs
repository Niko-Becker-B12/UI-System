using UnityEngine;
using TMPro;

namespace GPUI
{
    public class UiInputField : UiElement
    {

        public TMP_InputField inputField => this.GetComponent<TMP_InputField>();

        public UiText placeholderText;
        public UiText inputText;
        public UiText helperText;

        public UiElement icon1;
        public UiElement icon2;

    }
}