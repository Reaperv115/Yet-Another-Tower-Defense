using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Quaternion targetRotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("enemy"))
        {
            targetRotation = Quaternion.LookRotation(collider.transform.position - transform.position);
            transform.Rotate(targetRotation.eulerAngles);
        }
    }
}
