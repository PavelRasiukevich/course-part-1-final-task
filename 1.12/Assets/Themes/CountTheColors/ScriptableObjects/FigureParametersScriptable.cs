using UnityEngine;

[CreateAssetMenu(fileName = "Figures Parameters", menuName = "Figures Parameters", order = 0)]
public class FigureParametersScriptable : ScriptableObject
{
    [SerializeField] Color[] colors;
    [SerializeField] Sprite[] shapes;

    public Color[] Colors { get => colors; }
    public Sprite[] Shapes { get => shapes; }
}
