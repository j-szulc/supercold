using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KillablePlayer : MonoBehaviour, IKillable
{
    public void kill()
    {
        Debug.Log("Game over");
        GameController.Instance.gameOver();
    }
    
}
