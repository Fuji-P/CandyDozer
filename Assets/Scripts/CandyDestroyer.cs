using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyDestroyer : MonoBehaviour
{
    public CandyManager candyManager;
    public int reward;
    public GameObject effectPrefab;         // エフェクトプレハブプロパティ
    public Vector3 effectRotation;          // エフェクトローテーションプロパティ

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Candy")
        {
            // 指定数だけCandyのストック数を増やす
            candyManager.AddCandy(reward);

            // オブジェクトを削除
            Destroy(other.gameObject);

            // エフェクトプレハブの設定チェック
            if (effectPrefab != null)
            {
                // Candyのポジションにエフェクトを生成
                Instantiate(effectPrefab, other.transform.position, Quaternion.Euler(effectRotation));
            }
        }
    }
}
