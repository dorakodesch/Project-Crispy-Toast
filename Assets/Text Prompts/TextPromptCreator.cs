using UnityEngine;
using TMPro;

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
    TextBoxSettings[] textBoxSettings;

    private void Start()
    {
        
    }
}
