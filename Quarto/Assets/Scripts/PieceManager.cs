using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    public static PieceManager Instance;

    /* 全駒の情報の管理 */
    [SerializeField] GameObject pieceGroupPrefab;
    GameObject pieceGroup;
    PieceInfo[] pieceInfos;

    /* ステージ上の位置の管理 */
    Vector2[] stagePositions = new Vector2 [16];
    [SerializeField] GameObject positionGroup;
    GameObject[] positionObjects;

    List<Vector2> placedPositions;
    List<Vector2> stillPositions;

    private void Awake()
    {
        //if (Instance == null) Instance = this;
    }

    void Start()
    {
        SetPiece();
        SetPositionObject();
    }

    // 駒の生成と情報を準備
    void SetPiece()
    {
        pieceGroup = Instantiate(pieceGroupPrefab);
        pieceInfos = new PieceInfo[pieceGroup.transform.childCount];
        for (int i = 0; i < pieceInfos.Length; i++)
        {
            //pieceInfos[i] = new PieceInfo();
            pieceInfos[i] = pieceGroup.transform.GetChild(i).GetComponent<PieceInfo>();
            pieceInfos[i].InitializationInfo();
        }
    }

    // PositionObjectの取得
    void SetPositionObject()
    {
        positionObjects = new GameObject[positionGroup.transform.childCount];
        for (int i = 0; i < positionObjects.Length; i++) 
        {
            positionObjects[i] = positionGroup.transform.GetChild(i).gameObject;
            stagePositions[i] = new Vector2(i % 4, i / 4);

            //stillPositions.Add(stagePositions[i]);
        }
    }

    

    void Update()
    {
        
    }
}
