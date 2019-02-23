using System;

public interface IModel {

	RestorableMonoBehaviour[] GetRestorableMonoBehaviours();

	void RestoreRestorableMonoBehaviours();

}
