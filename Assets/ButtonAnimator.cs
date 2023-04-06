using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimator : MonoBehaviour
{
    [SerializeField] private Animator buttonAnim, buttonAnimChild, buttonAnimChildTwo;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHover()
    {
        buttonAnim.SetBool("onHover", true);
        buttonAnimChild.SetBool("onHover", true);
        buttonAnimChildTwo.SetBool("onHover", true);
    }

    public void OnMouseLeave()
    {
        buttonAnim.SetBool("onHover", false);
        buttonAnimChild.SetBool("onHover", false);
        buttonAnimChildTwo.SetBool("onHover", false);
    }   
    
}
