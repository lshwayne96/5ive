using UnityEngine;
using UnityEngine.UI;

public class OnClickResetProgress : MonoBehaviour {

    public void ResetProgress() {
        GameDataManager.ResetProgress();
    }
}
