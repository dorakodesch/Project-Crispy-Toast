using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

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
			var t = go.transform;
            t.SetParent(canvas.transform, false);
            var text = t.GetChild(0).GetComponent<TextMeshProUGUI>();
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

			go.AddComponent<TextBoxCollider>().creator = this;
			go.GetComponent<TextBoxCollider>().index = i;
        }
    }

    public void OnPlayerEnter(int i)
    {
        textObjects[i].SetActive(true);
    }

	public void OnPlayerExit(int i)
	{
		textObjects[i].SetActive(false);
	}
}
