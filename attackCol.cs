using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class attackCol : MonoBehaviour
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
        if (GameObject.Find("Player").GetComponent<Player>().playerAttack)
        {
            myCol.enabled = true;
        }

        if (GameObject.Find("Player").GetComponent<Player>().noPlayerAttack)
        {
            myCol.enabled = false;
        }
    }
}