using UnityEngine;

/// <summary>
/// This script resets the level progress.
/// </summary>
public class ResetProgressOnClick : MonoBehaviour {

	public void ResetProgress() {
		GameDataManager.ResetProgress();
	}
}
