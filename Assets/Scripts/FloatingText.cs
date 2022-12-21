using UnityEngine;
using TMPro;

public class FloatingText
{
    public bool active;
    public GameObject gameObject;
    public TextMeshProUGUI textMeshProUGUI;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    public void Show()
    {
        active = true;
        lastShown = Time.time;
        gameObject.SetActive(active);
    }

    public void Hide()
    {
        active = false;
        gameObject.SetActive(active);
    }

    public void Update()
    {
        if (!active) {
            return;
        }

        if (Time.time - lastShown > duration) {
            Hide();
        }

        gameObject.transform.position += motion * Time.deltaTime;
    }
}
