using UnityEngine;

namespace Main._5ive.Model {

	/// <summary>
	/// A class that is meant to be extended by classes that intend
	/// to be serialised and deserialised.
	/// </summary>
	public abstract class PersistentObject : MonoBehaviour {

		/// <summary>
		/// Saves this instance and returns the resulting data.
		/// </summary>
		/// <returns>The data.</returns>
		public abstract State Save();

		/// <summary>
		/// Restores this instance with the specified data.
		/// </summary>
		/// <param name="state">Data.</param>
		public abstract void RestoreWith(State state);
	}

}