using UnityEngine;
using System.Collections;

public class DestructibleBehaviour : MonoBehaviour {

	void Start () {
	
	}

    public virtual void Destroy()
    {
        if (enabled)
            Destroy(gameObject);
    }
}
