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
	}

    [SerializeField]
    Canvas canvas;

    [SerializeField]
    GameObject textPrefab;

	[SerializeField]
	GameObject colliderParent;

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
			var panelTransform = go.GetComponent<RectTransform>();
            var text = t.GetChild(0).GetComponent<TextMeshProUGUI>();

            // setting text box size, kind of a pain
            panelTransform.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Horizontal, settings.textBoxSize.x);
            panelTransform.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Vertical, settings.textBoxSize.y);
            panelTransform.anchoredPosition = settings.textBoxPosition;
			

            text.SetText(settings.displayText);
            text.fontSize = settings.fontSize;
            textObjects[i] = go;
            go.SetActive(false);

            // collider stuff
            Transform collider = colliderParent.transform.GetChild(i);
			collider.gameObject.AddComponent<TextBoxCollider>().creator = this;
			collider.gameObject.GetComponent<TextBoxCollider>().index = i;
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

	private void OnDrawGizmos()
	{
		for (int i = 0; i < colliderParent.transform.childCount; i++)
		{
			Transform collider = colliderParent.transform.GetChild(i);
			Gizmos.DrawWireCube(collider.position, collider.localScale);
		}
	}
}
