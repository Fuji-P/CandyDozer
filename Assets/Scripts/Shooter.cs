using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    const int MaxShotPower = 5;
    const int RecoverySeconds = 3;          // ショットパワーの回復時間定数

    int shotPower = MaxShotPower;
    AudioSource shotSound;

    public GameObject[] candyPrefabs;       // Candyプレハブプロパティ配列
    public Transform candyParentTransform;
    public CandyManager candyManager;
    public float shotForce;
    public float shotTorque;
    public float baseWidth;

    void Start()
    {
        shotSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 入力の検知
        if (Input.GetButtonDown("Fire1")) Shot();
    }

    // キャンディのプレハブからランダムに一つ選ぶ
    GameObject SampleCandy()
    {
        int index = Random.Range(0, candyPrefabs.Length);
        return candyPrefabs[index];
    }

    // 発射位置の計算
    Vector3 GetInstantiatePosition()
    {
        // 画面のサイズとInputの割合からキャンディ生成のポジションを計算
        float x = baseWidth * (Input.mousePosition.x / Screen.width) - (baseWidth / 2);
        return transform.position + new Vector3(x, 0, 0);
    }

    public void Shot()
    {
        // キャンディを生成できる条件外ならばShotしない
        if (candyManager.GetCandyAmount() <= 0) return;

        // ショットパワーのチェック
        if (shotPower <= 0) return;

        // プレハブからCandyオブジェクトを生成
        GameObject candy = (GameObject)Instantiate(SampleCandy(), GetInstantiatePosition(), Quaternion.identity);

        // 生成したCandyオブジェクトの親をcandyParentTransformに設定する
        candy.transform.parent = candyParentTransform;

        // CandyオブジェクトのRigidbodyを取得し力と回転を加える
        Rigidbody candyRigidBody = candy.GetComponent<Rigidbody>();
        candyRigidBody.AddForce(transform.forward * shotForce);
        candyRigidBody.AddTorque(new Vector3(0, shotTorque, 0));

        // Candyのストックを消費
        candyManager.ConsumeCandy();

        // ShotPowerを消費
        ConsumePower();

        // サウンドを再生
        shotSound.Play();
    }

    // ショットパワーの表示
    void OnGUI()
    {
        GUI.color = Color.black;

        // ShotPowerの残数を+の数で表示
        string label = "";
        for (int i = 0; i < shotPower; i++) label = label + "+";
        GUI.Label(new Rect(50, 65, 100, 30), label);
    }

    // ショットパワーの消費処理
    void ConsumePower()
    {
        // ShotPowerを消費すると同時に回復のカウントをスタート
        shotPower--;
        StartCoroutine(RecoverPower());
    }

    // ショットパワーの回復コルーチン
    IEnumerator RecoverPower()
    {
        // 一定秒数待った後にshotPowerを回復
        yield return new WaitForSeconds(RecoverySeconds);
        shotPower++;
    }
}
