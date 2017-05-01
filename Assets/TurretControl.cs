using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TurretControl : NetworkBehaviour {
	
	public Quaternion rotate;
	// Update is called once per frame

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		this.rotate = transform.rotation;
	}

	void Update () {
			if (!isLocalPlayer)
            {
                return;
            }
		if(Input.GetKey(KeyCode.R)){
			transform.Rotate(0,0,2f);
			this.rotate = transform.rotation;
		}
		if(Input.GetKey(KeyCode.T)){
			transform.Rotate(0,0,-2f);
			this.rotate = transform.rotation;
		}
	}
}
