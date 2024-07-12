using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    public Enemy enemy;
    public Enemy1000 enemy1000;

    public void Start()
    {
        //Destroy(enemy);
        Destroy(enemy.gameObject);
    }

    public void Update()
    {
        FunctionA();
        //FunctionB();
        //FunctionC();
    }

    public void FunctionA()
    {
        enemy.health = 5;
        Debug.Log(enemy.health);

        //enemy1000.ttt = 0;
        //Debug.Log(enemy1000.ttt);
    }

    private void FunctionB()
    {
        enemy.name = "...";
        //enemy.gameObject.name = "...";
        //Debug.Log(enemy.name);
    }

    private void FunctionC()
    {
        if (enemy != null)
        {
            Debug.Log("Enemy is NOT null");
        }
        else
        {
            Debug.LogWarning("Enemy is null");
        }
    }

    //private void OnDestroy()
    //{
    //    Destroy(enemy);
    //}
}
