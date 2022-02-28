using TMPro;
using UnityEngine;

public class TextPromptCreator : MonoBehaviour
{
    [System.Serializable]
    struct TextBoxSettings
    {
        public Vector2 textBoxSize, textBoxPosition;

        public string displayText;

        public int fontSize;

        public Vector3 colliderPosition;

        public Vector3 colliderSize;
    }

    [SerializeField]
    Canvas canvas;

    [SerializeField]
    TextBoxSettings[] textBoxSettings;

    Collider[] colliders;

    // setup, might put text changes in update for easy testing
    private void Start()
    {
        colliders = new Collider[textBoxSettings.Length];

        for (int i = 0; i < textBoxSettings.Length; i++)
        {
            TextBoxSettings settings = textBoxSettings[i];

            // creating text object
            var go = new GameObject("Text " + i);
            go.transform.SetParent(canvas.transform, false);
            var text = go.AddComponent<TextMeshProUGUI>();
            // setting text box size, kind of a pain
            text.rectTransform.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Horizontal, settings.textBoxSize.x);
            text.rectTransform.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Vertical, settings.textBoxSize.y);
            text.rectTransform.anchoredPosition = settings.textBoxPosition;
            text.SetText(settings.displayText);
            text.fontSize = settings.fontSize;

            // collider stuff
            go = new GameObject("Collider " + i);
            go.transform.position = settings.colliderPosition;
            go.transform.localScale = settings.colliderSize;
            go.AddComponent<BoxCollider>();
        }
    }
}
