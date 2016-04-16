using System.Collections;
using UnityEngine;

public interface PooledEntity
{
	/// <summary>
	/// Everything needed to reset the entity on spawn
	/// should be placed in this method and *not* Start().
	/// </summary>
	void ResetEntity ();
}
