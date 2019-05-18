using UnityEngine;

namespace Main.Model {

	/// <summary>
	/// A class that is meant to be extended by classes that intend
	/// to be serialised and deserialised.
	/// </summary>
	public abstract class RestorableMonoBehaviour : MonoBehaviour {

		/// <summary>
		/// Saves this instance and returns the resulting data.
		/// </summary>
		/// <returns>The data.</returns>
		public abstract Data Save();

		/// <summary>
		/// Restores this instance with the specified data.
		/// </summary>
		/// <param name="data">Data.</param>
		public abstract void RestoreWith(Data data);
	}

}