﻿using UnityEngine; using UnityEngine.UI;  public class CheatCodesInput : MonoBehaviour {      private CheatCodeAction cheatCodeAction;     private GameObject inputFieldGO;     private InputField inputField;      private bool takeThisReturnKey;      void Start() {         cheatCodeAction = GetComponentInChildren<CheatCodeAction>();         inputFieldGO = cheatCodeAction.gameObject;         inputField = inputFieldGO.GetComponent<InputField>();          inputFieldGO.SetActive(false);         takeThisReturnKey = true;     }      void Update() {         if (Input.GetKeyDown(KeyCode.Return)) {             if (takeThisReturnKey) {                 inputFieldGO.SetActive(true);                 inputField.ActivateInputField();             }             takeThisReturnKey = !takeThisReturnKey;         }     } } 