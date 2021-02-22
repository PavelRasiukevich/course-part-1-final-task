using UnityEngine;
using UnityEngine.UI;

namespace UnityBase.Shooter
{
    public class GameManager : MonoBehaviour
    {

        private int amountOfChilds;

        private static bool stopRotation;

        [SerializeField] GameObject[] levels = null;

        private GameObject level;

        [SerializeField] Text gameOverLabel = null;

        [SerializeField] float posX, posZ;

        public static int LevelID { get; private set; }

        public static bool IsGameOver { get; private set; }
        public static bool StopRotation { get => stopRotation; set => stopRotation = value; }

        private void Awake()
        {
            stopRotation = false;

            LevelID = 0;
            CreateNewLevel();
        }

        private void Start()
        {
            gameOverLabel.gameObject.SetActive(IsGameOver);
        }

        private void Update()
        {

            if (!IsGameOver)
            {

                amountOfChilds = level.transform.childCount;

                if (amountOfChilds == 1)
                {
                    Destroy(level);

                    if (LevelID < levels.Length)
                        CreateNewLevel();
                    else
                    {
                        IsGameOver = !IsGameOver;
                        gameOverLabel.gameObject.SetActive(IsGameOver);
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    }
                }
            }
        }

        private void CreateNewLevel()
        {
            posX = Random.Range(-posX, posX);

            level = Instantiate(levels[LevelID], new Vector3(posX, 0, posZ), Quaternion.identity);

            LevelID++;

        }
    }
}