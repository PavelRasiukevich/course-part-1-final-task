using UnityEngine;

namespace UnityBase.CountTheColors
{
    public class Figure : MonoBehaviour
    {
        [SerializeField] FigureParametersScriptable figureParams;


        private SpriteRenderer spriteRenderer;

        public FigureParametersScriptable FigureParams { get => figureParams; }

        public Color Color { get => spriteRenderer.color; set => spriteRenderer.color = value; }
        public Sprite Sprite { get => spriteRenderer.sprite; set => spriteRenderer.sprite = value; }

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }


    }
}