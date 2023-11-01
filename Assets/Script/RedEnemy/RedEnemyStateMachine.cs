using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemyStateMachine : MonoBehaviour
{
    public MonoBehaviour oblivious;
    public MonoBehaviour running;
    public MonoBehaviour startingState;
    private MonoBehaviour currentState;
    // Start is called before the first frame update
    void Awake()
    {
        ChangeState(startingState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(MonoBehaviour newState)
    {
        if (currentState != null)
        {
            currentState.enabled = false;
        }
        currentState = newState;

        currentState.enabled = true;
    }
}
