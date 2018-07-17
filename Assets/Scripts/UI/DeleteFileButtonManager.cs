using UnityEngine;

public class DeleteFileButtonManager : MonoBehaviour {

    private LoadMenuFileButtonManager fileButtonManager;

    void Start() {
        fileButtonManager =
            transform.parent.GetComponentInChildren<LoadMenuFileButtonManager>();
    }

    public void DeleteAllGameFileButtons() {
        fileButtonManager.DeleteAll();
    }

    public void DeleteOneGameFileButton() {
        fileButtonManager.DeleteOne();
    }

}
