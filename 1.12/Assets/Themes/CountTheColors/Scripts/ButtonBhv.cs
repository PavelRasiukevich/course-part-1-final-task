using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityBase.CountTheColors
{
    public class ButtonBhv : MonoBehaviour
    {
        public Action<Color> OnButtonClick;

        private Color buttonColor;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(Listener);
            buttonColor = GetComponent<Image>().color;
        }

        public void Listener()
        {
            OnButtonClick.Invoke(buttonColor);
        }

    }
}