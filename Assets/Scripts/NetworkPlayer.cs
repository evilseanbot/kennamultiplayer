using UnityEngine;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour
{
    public GameObject avatar;

    public Transform playerLocal;

    void Start ()
    {
        Debug.Log("i'm instantiated");

        if (photonView.isMine)
        {
            Debug.Log("player is mine");

            playerLocal = GameObject.Find("[CameraRig]/Camera (head)/Camera (eye)").transform;

            this.transform.SetParent(playerLocal);
            this.transform.localPosition = Vector3.zero;

            // avatar.SetActive(false);
        }
    }
	
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(playerLocal.localPosition);
            stream.SendNext(playerLocal.localRotation);
        }
        else
        {
            this.transform.position = (Vector3)stream.ReceiveNext();
            this.transform.rotation = (Quaternion)stream.ReceiveNext();
            avatar.transform.localPosition = (Vector3)stream.ReceiveNext();
            avatar.transform.localRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}