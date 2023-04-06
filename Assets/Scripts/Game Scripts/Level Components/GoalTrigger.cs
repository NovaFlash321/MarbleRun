using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] Camera goalCamera;
    [SerializeField] GameManager gManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hasFinishedLevel) HoldMarble();
    }


    private bool hasFinishedLevel = false;

    private void HoldMarble()
    {
        GameObject pMarble = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Rigidbody>().gameObject;
        pMarble.GetComponent<Rigidbody>().useGravity = false;
        pMarble.GetComponent<Rigidbody>().velocity = Vector3.zero;
        pMarble.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        pMarble.transform.position = Vector3.Slerp(pMarble.transform.position, this.transform.position, 0.01f);
    }
    private void OnTriggerEnter(Collider other) {

        gManager.SetLevelState(LevelState.LEVELEND);

        GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>().OnDisable();
        
        hasFinishedLevel = true;
        
    }
}
