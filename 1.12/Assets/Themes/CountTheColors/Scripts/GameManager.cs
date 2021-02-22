using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace UnityBase.CountTheColors
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI textLabel;
        [SerializeField] ButtonBhv[] buttons;
        [SerializeField] Transform figuresHolder;
        [SerializeField] GameObject figurePrefab;
        [SerializeField] int ROWS, COLUMNS;
        [SerializeField] float CELL_SIZE;
        [SerializeField] AudioClip wrong, correct;

        private Dictionary<int, Color> dictionaryOfColors;
        private Dictionary<Color, int> valueCount;
        private List<Color> validKeys;
        private AudioSource audioSource;
        private Animator animator;

        private int mistakes;
        private bool isWaiting;

        private int[,] matrix;

        private void Start()
        {
            buttons = FindObjectsOfType<ButtonBhv>();

            foreach (var button in buttons)
            {
                button.OnButtonClick += OnClickHandler;
            }


            mistakes = 0;

            dictionaryOfColors = new Dictionary<int, Color>();
            valueCount = new Dictionary<Color, int>();
            audioSource = GetComponent<AudioSource>();

            GenerateMatrix();
            GenerateFigures();
            CountOccurrencesOfColor();

        }


        private void Update()
        {

            if (CheckForDuplicates())
            {
                Clear();

                GenerateMatrix();
                GenerateFigures();
                CountOccurrencesOfColor();
            }

            if (valueCount != null)
            {
                if (valueCount.Count == 1 && !isWaiting)
                {

                    StopAllCoroutines();

                    mistakes = 0;

                    Clear();

                    GenerateMatrix();
                    GenerateFigures();
                    CountOccurrencesOfColor();

                }
            }
        }


        private void Clear()
        {
            textLabel.text = $"Mistakes: {mistakes}";

            Figure[] _f = FindObjectsOfType<Figure>();

            foreach (var f in _f)
            {
                Destroy(f.gameObject);
            }


            for (int i = 0; i < dictionaryOfColors.Count; i++)
            {
                if (dictionaryOfColors.ContainsKey(i))
                    dictionaryOfColors.Remove(i);
            }

            valueCount = null;
            validKeys = null;

        }

        private void GenerateMatrix()
        {
            matrix = new int[ROWS, COLUMNS];

            for (int row = 0; row < ROWS; row++)
            {
                for (int column = 0; column < COLUMNS; column++)
                {
                    matrix[row, column] = Random.Range(0, 2);
                }
            }
        }

        private void GenerateFigures()
        {

            dictionaryOfColors = new Dictionary<int, Color>();

            for (int row = 0; row < ROWS; row++)
            {
                for (int column = 0; column < COLUMNS; column++)
                {
                    bool canCreate = matrix[row, column] == 1;

                    if (canCreate)
                    {
                        GameObject figure = Instantiate(figurePrefab);
                        Figure f = figure.GetComponent<Figure>();

                        Color color = f.FigureParams.Colors[Random.Range(0, f.FigureParams.Colors.Length)];
                        Sprite sprite = f.FigureParams.Shapes[Random.Range(0, f.FigureParams.Shapes.Length)];

                        f.Color = color;
                        f.Sprite = sprite;

                        f.transform.position = new Vector2(column * CELL_SIZE, row * CELL_SIZE);

                        f.transform.SetParent(figuresHolder);

                        dictionaryOfColors.Add(f.transform.GetSiblingIndex(), color);
                    }

                }
            }

        }

        private void CountOccurrencesOfColor()
        {
            valueCount = new Dictionary<Color, int>();

            foreach (Color item in dictionaryOfColors.Values)
            {
                if (valueCount.ContainsKey(item))
                    valueCount[item]++;
                else
                    valueCount[item] = 1;
            }

        }

        private bool CheckForDuplicates()
        {

            var duplicates = valueCount.GroupBy(x => x.Value).Where(x => x.Count() > 1);

            if (duplicates.Count() > 0)
                return true;
            else
                return false;

        }

        public Color GetMostRepeatedColor()
        {

            var max = valueCount.Aggregate((a, b) => a.Value > b.Value ? a : b);

            return max.Key;
        }


        private void OnClickHandler(Color buttonColor)
        {
            if (isWaiting)
                return;

            Color mostRepeated = GetMostRepeatedColor();

            if (buttonColor == mostRepeated)
            {
                audioSource.PlayOneShot(correct);
                RemoveFigures(buttonColor);
                DeleteCurrentValidKey(mostRepeated, valueCount);
            }
            else
            {
                audioSource.PlayOneShot(wrong);
                textLabel.text = $"Mistakes: {++mistakes}";
            }

        }

        private void RemoveFigures(Color color)
        {
            Figure[] _f = FindObjectsOfType<Figure>();

            foreach (var f in _f)
            {
                if (f.Color == color)
                {
                    isWaiting = true;
                    animator = f.GetComponent<Animator>();
                    animator.SetTrigger("Destroying");
                    StartCoroutine(Destroying(f));
                }
            }
        }

        IEnumerator Destroying(Figure f)
        {
            yield return new WaitForSeconds(1.0f);
            Destroy(f.gameObject);
            isWaiting = false;
        }

        private static void DeleteCurrentValidKey(Color currentMaxColor, Dictionary<Color, int> dict)
        {
            for (int i = 0; i < dict.Count; i++)
            {
                if (dict.ContainsKey(currentMaxColor))
                    dict.Remove(currentMaxColor);
            }
        }

    }
}