using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class lRAttackCol : MonoBehaviour
{
    public GameObject Player;

    private BoxCollider2D myCol;

    void Start()
    {
        myCol = GetComponent<BoxCollider2D>();

        myCol.enabled = false;
    }

    void Update()
    {
        if (GameObject.Find("Player").GetComponent<Player>().playerLRAttack)
        {
            myCol.enabled = true;
        }

        if (GameObject.Find("Player").GetComponent<Player>().noPlayerLRAttack)
        {
            myCol.enabled = false;
        }
    }
}