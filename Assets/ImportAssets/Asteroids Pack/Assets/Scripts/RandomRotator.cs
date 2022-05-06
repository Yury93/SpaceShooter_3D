using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{
    [SerializeField]
    private float tumble;

    void Start()
    {
        transform.Rotate(Vector3.one * tumble * Time.deltaTime);
    }
}