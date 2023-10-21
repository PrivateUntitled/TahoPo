using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private float totalRotation = 180.0f;
    private float startingRotation;
    private float rotationPerTurn;

    [SerializeField] private GameObject arrow;

    // Start is called before the first frame update
    void Start()
    {
        rotationPerTurn = totalRotation / GameManager.instance.CustomerToServe;
        startingRotation = arrow.transform.eulerAngles.z;

        Debug.Log(startingRotation);
    }

    public void RotateArrow(int currentCustomer)
    {
        arrow.transform.eulerAngles = new Vector3(0, 0, startingRotation - (rotationPerTurn * currentCustomer));
    }
    
}
