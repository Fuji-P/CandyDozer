using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    Vector3 startPosition;
    public float amplitube;         // 移動量プロパティ
    public float speed;             // 移動速度プロパティ

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // 変位を計算
        float z = amplitube * Mathf.Sin(Time.time * speed);

        // zを変位させたポジションに再設定
        transform.localPosition = startPosition + new Vector3(0, 0, z);
    }
}
