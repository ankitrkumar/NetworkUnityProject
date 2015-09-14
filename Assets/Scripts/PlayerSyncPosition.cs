using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSyncPosition : NetworkBehaviour {

    private Vector3 syncPos;
    [SerializeField] Transform myTransform;
    [SerializeField] float lerpRate = 15;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transmitPosition();
        lerpPosition();

	}
    void lerpPosition()
    {
        if(!isLocalPlayer)
        {
            myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    void CmdProvidePositionToServer(Vector3 pos)
    {
        syncPos = pos;
    }

    [ClientCallback]
    void transmitPosition()
    {
        if (isLocalPlayer)
        {
            CmdProvidePositionToServer(myTransform.position);
        }
    }
}
