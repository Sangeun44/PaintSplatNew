using UnityEngine;
using System.Collections;

public class BallBehavior : Photon.MonoBehaviour
{
    private float yBelowTarget = -5;
    private float zBeyondTarget = 40;

    // Update is called once per frame
    private void Update()
    {
        if (GetComponent<PhotonView>().isMine)
        {
            if ((transform.position.y < yBelowTarget && GetComponent<Rigidbody>().velocity.y < 0) ||
                transform.position.z >= zBeyondTarget)
            {
                AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position);
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    // LOOK-2.c you use PhotonView.RPC() to call this and give the ball a velocity and color!
    [PunRPC]
    public void RPCInitialize(Vector3 init_vel, Vector3 color)
    {
        GetComponent<Rigidbody>().AddForce(init_vel, ForceMode.VelocityChange);
        GetComponent<Rigidbody>().useGravity = true;

        // color is sent as Vector3 since UnityEngine.Color is not serializable by Photon
        GetComponent<Renderer>().material.color = new Color(color.x,color.y, color.z);

        if (PhotonNetwork.isMasterClient)
        {
            GetComponent<PhotonView>().RequestOwnership();
        }
    }
}