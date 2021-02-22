using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UnityBase
{
    public class MenuButton : MonoBehaviour
    {

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(Listener);
        }

        private void Listener()
        {
            SceneManager.LoadScene(0);
        }
    }
}