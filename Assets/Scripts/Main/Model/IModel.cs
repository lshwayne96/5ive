namespace Main.Model {

	public interface IModel {

		RestorableMonoBehaviour[] GetRestorableMonoBehaviours();

		void RestoreRestorableMonoBehaviours();

	}

}