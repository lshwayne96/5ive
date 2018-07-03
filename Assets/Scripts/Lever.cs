using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {
    public GameObject interactable;
    private bool hasRotated;
    private bool interactableState;
    private bool hasEnteredTrigger;
    private Vector3 currentAngle;
    private Vector3 targetAngle;
    private float ANGLE; // How much the lever should rotate

    // Use this for initialization
    void Start() {
        interactableState = interactable.activeSelf;
        ANGLE = 90f;

        // Cache both the currentAngle and targetAngle for convenience
        currentAngle = transform.eulerAngles;
        targetAngle = transform.eulerAngles + Vector3.back * ANGLE;
    }

    // Update is called once per frame
    void Update() {
        if (hasEnteredTrigger && Input.GetKeyUp(KeyCode.R)) {
            RotateLever();
            ChangeInteractableState();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            hasEnteredTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            hasEnteredTrigger = false;
        }
    }

    private void RotateLever() {
        transform.eulerAngles = Vector3.Lerp(currentAngle, targetAngle, 1);

        // Swap the currentAngle and targetAngle
        Vector3 temp;
        temp = currentAngle;
        currentAngle = targetAngle;
        targetAngle = temp;
    }

    // Controls the interactable assigned to the lever
    private void ChangeInteractableState() {
        interactable.SetActive(!interactableState);
        interactableState = !interactableState;
    }
}
