using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    [SerializeField, Tooltip("Rotates force axes on the X and Z direction")] private Transform forceDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region Manually Rotate around X and Z axes
        
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.RotateAround(this.transform.position, this.transform.forward, 1f);
            forceDirection.transform.RotateAround(this.transform.position, this.transform.forward, 1f);
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.RotateAround(this.transform.position, this.transform.forward, -1f);
            forceDirection.transform.RotateAround(this.transform.position, this.transform.forward, -1f);
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.RotateAround(this.transform.position, this.transform.right, -1f);
            forceDirection.transform.RotateAround(this.transform.position, this.transform.right, -1f);
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.RotateAround(this.transform.position, this.transform.right, 1f);
            forceDirection.transform.RotateAround(this.transform.position, this.transform.right, 1f);
        }

        
        #endregion
    }
}
