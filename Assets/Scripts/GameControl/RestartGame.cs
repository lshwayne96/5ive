using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour {

    public void Restart() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }
}
