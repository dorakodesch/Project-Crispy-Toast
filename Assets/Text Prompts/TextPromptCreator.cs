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
    GameObject textPrefab;

    [SerializeField]
    TextBoxSettings[] textBoxSettings;

    GameObject[] textObjects;

    // setup, might put text changes in update for easy testing
    private void Start()
    {
        textObjects = new GameObject[textBoxSettings.Length];

        for (int i = 0; i < textBoxSettings.Length; i++)
        {
            TextBoxSettings settings = textBoxSettings[i];

            // creating text object
            var go = Instantiate(textPrefab);
            go.transform.SetParent(canvas.transform, false);
            var text = go.GetComponent<TextMeshProUGUI>();
            // setting text box size, kind of a pain
            text.rectTransform.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Horizontal, settings.textBoxSize.x);
            text.rectTransform.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Vertical, settings.textBoxSize.y);
            text.rectTransform.anchoredPosition = settings.textBoxPosition;
            text.SetText(settings.displayText);
            text.fontSize = settings.fontSize;
            textObjects[i] = go;
            go.SetActive(false);

            // collider stuff
            go = new GameObject("Collider " + i);
            go.transform.position = settings.colliderPosition;
            go.transform.localScale = settings.colliderSize;
            go.AddComponent<BoxCollider>().isTrigger = true;
            go.AddComponent<TextBoxCollider>();
            TextBoxCollider textBoxCollider = go.GetComponent<TextBoxCollider>();
            textBoxCollider.promptCreator = this;
            textBoxCollider.index = i;
        }
    }

    public void CollisionDetected(int i)
    {
        GameObject go = textObjects[i];
        go.SetActive(true);
    }
}
