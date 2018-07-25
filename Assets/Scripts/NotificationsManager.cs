using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NotificationsManager : MonoBehaviour {
    private Image image;
    private Text text;
    private float startTime;
    private bool isOverwriteSuccessful;

    private void Awake() {
        image = GetComponentInChildren<Image>();
        image.enabled = false;
        text = GetComponentInChildren<Text>();
    }

    private void Update() {
        if (isOverwriteSuccessful) {
            StartCoroutine(Fade(startTime));
        }
    }

    public void OverwriteSuccessful() {
        image.enabled = true;
        text.text = "Overwrite Successful!";
        startTime = Time.time;
        isOverwriteSuccessful = true;
    }

    private IEnumerator Fade(float start) {
        float difference = Time.time - start;
        while (difference < 3f) {
            yield return null;
        }
        image.enabled = false;
        text.text = System.String.Empty;
    }
}
