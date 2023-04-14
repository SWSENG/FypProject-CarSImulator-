using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fail : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Score.DeductScore(30);
            Destroy(gameObject);
            //Debug.Log("Hit");
        }
    }
}
