using System.Collections;

using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {


    //void OnTriggerExit(Collider other)
    //{
    //    Destroy(other.gameObject);
    //}
    void OnTriggerExit2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}
