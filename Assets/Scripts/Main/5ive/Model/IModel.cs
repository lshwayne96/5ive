namespace Main._5ive.Model {

	public interface IModel {

		PersistentObject[] GetRestorableMonoBehaviours();

		void RestoreRestorableMonoBehaviours();

	}

}