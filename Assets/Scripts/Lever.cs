/*
 * This script encapsulates the functionality of a lever.
 * It requires the gameObject of interest to be dragged to interactable.
 */

using System.Collections;
using UnityEngine;

public class Lever : MonoBehaviour {

    // Expose the interactable variable to the editor
    public GameObject interactable;

    private PauseGame pauseGame;

    private bool interactableState;
    private bool hasEnteredTrigger;
    private bool toResume;
    private bool hasSwitchedRotation;
    private bool isRotating;

    private Vector3 currentAngle;
    private Vector3 targetAngle;
    private float angleOfRotation;

    private float startTime;
    private float journeyLength;
    private float speed;

    private Quaternion startRotation;
    private Quaternion endRotation;
    private Quaternion prevRotation;
    private Quaternion prevEndRotation;

    /*
     * Awake() is used instead of Start() to allow the lever
     * to be rotated by Restore() in InteractablesData
     * to its previous state when a new game is loaded
     * */
    void Awake() {
        /*
         * It is possible that a number of levers control
         * a gameObject in unison. In that case, no gameObject
         * will be attached as an interactable.
         * The InteractionManager will manage their collective
         * interaction.
         */
        if (interactable != null) {
            interactableState = interactable.activeSelf;
        }
        angleOfRotation = 90f;

        pauseGame = GameObject.FindGameObjectWithTag("Pause").GetComponent<PauseGame>();

        Vector3 angleDifference = Vector3.back * angleOfRotation;
        currentAngle = transform.eulerAngles;
        targetAngle = transform.eulerAngles + angleDifference;

        startRotation = transform.rotation;
        Vector3 eulerAngles = transform.eulerAngles;
        endRotation = Quaternion.Euler(eulerAngles + angleDifference);
    }

    void Start() {
        speed = 300f;
        journeyLength = Vector3.Distance(targetAngle, currentAngle);
    }

    /* 
     * If the player is within the collider boundaries of the lever
     * and the R key is pressed, the lever will rotate and
     * and the interactable gameObject will disappear.
    */
    void Update() {
        if (hasEnteredTrigger && Input.GetKeyUp(KeyCode.R)) {
            startTime = Time.time;
            isRotating = true;
            StartCoroutine(Rotate());
        }

        if (toResume) {
            isRotating = true;
            StartCoroutine(Rotate(startTime, prevRotation, prevEndRotation));
        }
    }

    // When the player enters the lever
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            hasEnteredTrigger = true;
        }
    }

    // When the player leaves the lever
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            hasEnteredTrigger = false;
        }
    }

    private IEnumerator Rotate() {
        float distCovered;
        float fracJourney = 0;

        while (fracJourney < 1) {
            if (!pauseGame.IsGamePaused()) {
                // Distance moved = time * speed.
                distCovered = (Time.time - startTime) * speed;

                // Fraction of journey completed = current distance divided by total distance.
                fracJourney = distCovered / journeyLength;

                // Set our position as a fraction of the distance between the markers.
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, fracJourney);
            } else {
                startTime = Time.time;
            }
            yield return null;
        }

        if (interactable != null) {
            ChangeInteractableState();
        }

        Quaternion temp = startRotation;
        startRotation = endRotation;
        endRotation = temp;

        hasSwitchedRotation = !hasSwitchedRotation;
        isRotating = false;
    }

    private IEnumerator Rotate(float newStartTime, Quaternion prevRotation, Quaternion prevEndRotation) {
        float distCovered;
        float fracJourney = 0;

        while (fracJourney < 1 && !pauseGame.IsGamePaused()) {
            // Distance moved = time * speed.
            distCovered = (Time.time - newStartTime) * speed;

            // Fraction of journey completed = current distance divided by total distance.
            fracJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.rotation = Quaternion.Slerp(prevRotation, prevEndRotation, fracJourney);

            yield return null;
        }

        if (interactable != null) {
            ChangeInteractableState();
        }

        Quaternion temp = startRotation;
        startRotation = endRotation;
        endRotation = temp;

        hasSwitchedRotation = !hasSwitchedRotation;
        isRotating = false;
        toResume = false;
    }

    public void SetPrevRotation(Quaternion prevRotation) {
        this.prevRotation = prevRotation;
    }

    public void SetPrevEndRotation(Quaternion prevEndRotation) {
        this.prevEndRotation = prevEndRotation;
    }

    public void ResumeRotation() {
        toResume = true;
        startTime = Time.time;
    }

    public void SwitchRotation() {
        transform.rotation = endRotation;
    }

    // Controls the interactable assigned to the lever
    private void ChangeInteractableState() {
        interactable.SetActive(!interactableState);
        interactableState = !interactableState;
    }

    public void Toggle() {
        Rotate();
        ChangeInteractableState();
    }

    public bool HasSwitchedRotation() {
        return hasSwitchedRotation;
    }

    public LeverData CacheData() {
        Debug.Log(transform.rotation);
        Debug.Log(endRotation);
        Debug.Log(hasSwitchedRotation);
        Debug.Log(isRotating);
        return new LeverData(transform.rotation, endRotation, hasSwitchedRotation, isRotating);
    }

}

