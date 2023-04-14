using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackwardFail : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<RuleVoice>().Play_vehicleMoveBackwardFail();
            Score.DeductScore(30);
            Destroy(gameObject);
        }
    }
}
