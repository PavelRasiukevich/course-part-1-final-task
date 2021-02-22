using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityBase.Main
{
    public class Manager : MonoBehaviour
    {
        [SerializeField] List<ButtonBhv> buttons;

        private void Start()
        {
            foreach (var b in buttons)
            {
                b.OnClickButton += SwitchScene;
            }
        }

        private void SwitchScene(string  name)
        {
            SceneManager.LoadScene(name);
        }

        [ContextMenu("Autofill")]
        private void AutoFill()
        {
            buttons = FindObjectsOfType<ButtonBhv>().ToList();
        }
    }
}