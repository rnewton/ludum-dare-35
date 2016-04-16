# PoolParty
## A simple entity pool for Unity

Inspired pretty heavily from [quill-18's SimplePool](https://gist.github.com/quill18/5a7cfffae68892621267). The code has been cleaned up and two additional features have been added. 

1. When calling `PoolParty.Destroy ()`, an optional time parameter can be added to destory it after a set period. This is closely in line with the default `GameObject.Destory ()` method.
2. If your prefab includes a MonoBehaviour that implements the `PooledGameObject` interface, values can be automatically reset when the prefab is spawned. The `ResetEntity ()` method is called when the prefab is first created and whenever it is pulled from the pool.

### Usage:

```csharp
using CoinFlipGames.PoolParty;
using System.Collections;
using UnityEngine;

public class MyMonoBehaviour : MonoBehaviour
{
	public GameObject somePrefab;

	void Start ()
	{
		PoolParty.Preload(somePrefab, 200);
	}

	public void SomeImportantMethod ()
	{
		// You probably want to keep a reference to the spawned entity until it needs to be destroyed.
		GameObject entity = PoolParty.Instantiate (somePrefab, Random.insideUnitSphere, Random.rotation);

		// You can do whatever you want with the spawned entity, but I'm just going to remove it after 5 seconds.
		PoolParty.Destroy (entity, 5f);
	}
}
```

You can also automatically reset entity values when spawning if you declare a monobehaviour on your prefab like so:

```csharp
public class PrefabBehaviour : MonoBehaviour, PooledEntity
```

And implement the method `public void ResetEntity ()`

See the example folder for more details on how this is accomplished.
