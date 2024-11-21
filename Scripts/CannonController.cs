using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject objPrefab;
    public float delayTime = 3.0f;
    public float fireSpeed = 4.0f;
    public float length = 8.0f;

    GameObject player;
    Transform gateTransform;
    float passedTimes = 0;

    bool CheckLength(Vector2 targetPos)
    {
        bool ret = false;
        float d = Vector2.Distance(transform.position, targetPos);
        if(length >= d)
        {
            ret = true;
        }
        return ret;
    }
    // Start is called before the first frame update
    void Start()
    {
        gateTransform = transform.Find("gate");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //砲弾を打ってからのインターバルを計測
        passedTimes += Time.deltaTime;

        //プレイヤーが近くにいるかどうかチェック
        if (CheckLength(player.transform.position))
        {
            //計測した時間が基準時間を越えれば発射
            if (passedTimes > delayTime)
            {
                //計測時間をリセットしておく
                passedTimes = 0;

                //砲弾の生成場所を取得※gateオブジェクトの位置
                Vector2 pos = new Vector2(gateTransform.position.x,gateTransform.position.y);

                //砲弾プレハブを生成して、生成したオブジェクト情報を変数objに入れる
                GameObject obj = Instantiate(objPrefab, pos, Quaternion.identity);

                //Rigidbody2Dの力を使えるように準備
                Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();

                //砲台の角度をオイラー値で取得
                float angleZ = transform.localEulerAngles.z;

                //オイラー角度から縦の比率を取得
                float x = Mathf.Cos(angleZ * Mathf.Deg2Rad);
                //オイラー角度から横の比率を取得
                float y = Mathf.Sin(angleZ * Mathf.Deg2Rad);

                //縦横の方向データに基準となるスピードの値を加えておく
                Vector2 v = new Vector2(x, y) * fireSpeed;

                //寸前につくった変数v(方向データ*スピード）の値を元にして、Rigidbody2DのAddForceメソッドにより砲弾を飛ばす
                rbody.AddForce(v, ForceMode2D.Impulse);
            }
        }
    }

    //砲台のテリトリーを視覚化
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, length);
    }
}