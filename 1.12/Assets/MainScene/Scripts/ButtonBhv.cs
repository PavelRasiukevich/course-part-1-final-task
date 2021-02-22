using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityBase.Main
{
    public class ButtonBhv : MonoBehaviour
    {
        public Action<string> OnClickButton;

        [SerializeField] new string name;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(Listener);
            name = gameObject.name;
        }

        void Listener()
        {
            OnClickButton.Invoke(name);
        }
    }
}