using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() => Spawn();

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.up * 0.1f);
    }

    void Spawn()
    {
        float x = Random.Range(0, 800);
        float y = Random.Range(-300, 300);
        this.transform.position = new Vector3(x, y);
    }
}
