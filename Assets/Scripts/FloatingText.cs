using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public bool active;
    public GameObject gObject;
    public Text txt;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    public void Show()
    {
        active = true;
        lastShown = Time.time;
        gObject.SetActive(active);
    }

    public void Hide()
    {
        active = false;
        gObject.SetActive(active);
    }

    public void UpdateFloatingText()
    {
        if (!active) {
            return;
        }

        if (Time.time - lastShown > duration) {
            Hide();
        }

        gObject.transform.position += motion * Time.deltaTime;
    }
}
