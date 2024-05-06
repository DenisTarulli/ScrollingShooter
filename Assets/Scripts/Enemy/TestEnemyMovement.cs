using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyMovement : EnemyMovement
{
    private void Awake()
    {
        FindWavePattern();        
    }

    private void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        courutineAllowed = true;
    }

    private void Update()
    {
        if (courutineAllowed)
            StartCoroutine(GoByTheRoute(routeToGo));
    }
}
