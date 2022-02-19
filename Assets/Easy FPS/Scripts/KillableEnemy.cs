using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KillableEnemy : MonoBehaviour, IKillable
{

    public GameObject blood;
    public int count = 5;
    public void kill()
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(blood, transform.position+Vector3.up, transform.rotation);
        }

        Debug.Log("Enemy is dead!");
        GameController.Instance.Invoke("win",0.1f);
        Destroy(gameObject);
    }
    
}
