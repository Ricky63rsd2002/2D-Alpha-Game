using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cemeracontroler : MonoBehaviour
{
    [SerializeField] private Transform trans;

    private void Update()
    {
        transform.position = new Vector3(trans.position.x, trans.position.y, transform.position.z);
    }
}
