using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AmmuNation : MonoBehaviour
{
    public GameObject[] camera;
    public GameObject Player;
    public bool once = true;
    private void OnTriggerEnter(Collider col)
    {
        if (once)
        {
            Debug.Log("Collider detected !");
            if (col.gameObject.tag == "Player")
            {
                Debug.Log("The player is close to AmmuNation !");
                camera[0].gameObject.SetActive(true);
            }
            once = false;
        }
    }

}
