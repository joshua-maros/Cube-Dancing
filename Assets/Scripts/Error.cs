using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Error : MonoBehaviour
{
    Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.transform.localPosition;
        pos.y -= Time.deltaTime * 0.01f;
        this.transform.localPosition = pos;
        Color color = mat.GetColor("_EmissionColor");
        color.r -= Time.deltaTime * 5.0f;
        mat.SetColor("_EmissionColor", color);
        if (color.r < 0) {
            Destroy(this.gameObject);
        }
    }
}
