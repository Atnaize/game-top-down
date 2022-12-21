using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;
    private List<FloatingText> floatingTexts = new List<FloatingText>();

    public void Show(string text, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingText floatingText = GetFloatingText();

        floatingText.textMeshProUGUI.text = text;
        floatingText.textMeshProUGUI.fontSize = fontSize;
        floatingText.textMeshProUGUI.color = color;
        floatingText.gameObject.transform.position = Camera.main.WorldToScreenPoint(position);
        floatingText.motion = motion;
        floatingText.duration = duration;

        floatingText.Show();
    }

    public void Update()
    {
        foreach (FloatingText floatingText in floatingTexts)
        {
            floatingText.Update();
        }
    }

    private FloatingText GetFloatingText()
    {
        FloatingText floatingText = floatingTexts.Find(t => !t.active);

        if (floatingText == null) {
            floatingText = new FloatingText();

            floatingText.gameObject = Instantiate(textPrefab);
            floatingText.gameObject.transform.SetParent(textContainer.transform);
            floatingText.textMeshProUGUI = floatingText.gameObject.GetComponent<TextMeshProUGUI>();

            floatingTexts.Add(floatingText);
        }

        return floatingText;
    }
}
