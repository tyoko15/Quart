using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject[] centerObject;   // 注目するオブジェクト
    [SerializeField] float cameraWarkRadius;    // 中心からの距離
    [SerializeField] float cameraWarkspeed;     // 回転スピード
    [SerializeField] float heightOffset;        // 中心からの高さ

    float angleX;           // 水平角度
    float angleY = 40f;     // 垂直角度

    bool changeLookFlag;
    [SerializeField] float changeLookTime; 
    float changeLookTimer;

    bool pickupStageFlag;   // false = 駒選択を注目 : true ステージを注目

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    void LateUpdate()
    {
        changeLookFlag = InputManager.Instance.GetChangeLookFlag();
        ChangeLookObject();
        if (!changeLookFlag) CameraVerticalMove();
    }

    void ChangeLookObject()
    {
        if (changeLookFlag)
        {
            if (changeLookTimer >= changeLookTime)
            {
                changeLookTimer = 0;
                angleX = 0;
                pickupStageFlag = (pickupStageFlag) ? false : true;
                InputManager.Instance.SetChangeLookFlag(false);
            }
            else
            {
                /* 最初の処理 */
                if (changeLookTimer == 0)
                {
                    // beforeの地点で回転をリセット
                    angleX = 0;
                    // 回転をQuaternionに変換
                    Quaternion rotation = Quaternion.Euler(angleY, angleX, 0);

                    // 注目するオブジェクトを設定
                    GameObject attentionObject;
                    attentionObject = (pickupStageFlag) ? centerObject[0] : centerObject[1];

                    // offsetベクトルを回転させて位置を算出
                    Vector3 negDistance = new Vector3(0.0f, 0.0f, -cameraWarkRadius);
                    Vector3 position = rotation * negDistance + attentionObject.transform.position + new Vector3(0, heightOffset, 0);

                    // カメラの位置と向き
                    transform.position = position;
                    transform.LookAt(attentionObject.transform.position + new Vector3(0, heightOffset, 0));
                }                

                /* 横移動の処理 */

                // before地点とafter地点の取得
                GameObject before = (pickupStageFlag) ? centerObject[1] : centerObject[0];
                GameObject after = (pickupStageFlag) ? centerObject[0] : centerObject[1];

                // before地点からafter地点へ移動
                float x = Mathf.Lerp(before.transform.position.x, after.transform.position.x, changeLookTimer / changeLookTime);
                transform.position = new Vector3(x, transform.position.y, transform.position.z);

                /* タイマーの更新 */
                changeLookTimer += Time.deltaTime;
            }
        }
    }

    void CameraVerticalMove()
    {
        float horizontal = InputManager.Instance.GetMove().x;
        angleX -= horizontal * cameraWarkspeed * Time.deltaTime;

        // 回転をQuaternionに変換
        Quaternion rotation = Quaternion.Euler(angleY, angleX, 0);

        // 注目するオブジェクトを設定
        GameObject attentionObject;
        attentionObject = (!pickupStageFlag) ? centerObject[0] : centerObject[1];

        // offsetベクトルを回転させて位置を算出
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -cameraWarkRadius);
        Vector3 position = rotation * negDistance + attentionObject.transform.position + new Vector3(0, heightOffset, 0);

        // カメラの位置と向き
        transform.position = position;
        transform.LookAt(attentionObject.transform.position + new Vector3(0, heightOffset, 0));
    }
}
