using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("全ての駒を取得")]
    [SerializeField] GameObject[] Pieces;

    [Header("色の違いを取得")]
    [SerializeField] GameObject[] Blacks;
    [SerializeField] GameObject[] Whites;

    [Header("穴の有無の違いを取得")]
    [SerializeField] GameObject[] Hole;
    [SerializeField] GameObject[] NotHole;

    [Header("高さの違いを取得")]
    [SerializeField] GameObject[] High;
    [SerializeField] GameObject[] Low;

    [Header("形の違いを取得")]
    [SerializeField] GameObject[] Circle;
    [SerializeField] GameObject[] Square;

    bool PlayerFlag;

    [Header("ChoosePiecesの位置の取得")]
    [SerializeField] GameObject[] ChoosePiecesPosiObjects;
    [SerializeField] Vector3[] ChoosePiecesPosi = new Vector3[17];

    [Header("Piecesの位置の取得")]
    [SerializeField] GameObject[] PiecesPosiObjects;
    [SerializeField] Vector3[] PiecesPosi = new Vector3[17];

    [Header("設置された駒を取得")]
    [SerializeField] GameObject[] OnPieces = new GameObject[16];
    [Header("設置された場所を取得")]
    [SerializeField] int[] OnPiecesNum = new int[16];

    [SerializeField] GameObject PlayerChooseObject;
    [SerializeField] GameObject PlayerSelectObject;
    [SerializeField] int PlayerSelectNum;
    [SerializeField] int PlayerChooseNum;
    [SerializeField] float AxisH;
    [SerializeField] float AxisV;
    [SerializeField] bool KeepOneShot;
    [SerializeField] bool DecideFlag;
    Vector3 DecidePosi;
    int PieceOrderNum;

    bool OneShotFlag;
    int PlayerNum;
    float StartSettingAxisH;
    bool StartSettingDecideFlag;
    enum Status
    {
        Start,
        Play,
        Purse,
    }
    Status status = Status.Start;

    void Update()
    {
        switch (status)
        {
            case Status.Start:
                {
                    if (!OneShotFlag)
                    {
                        ResetVar();
                        OneShotFlag = true;
                    }
                    StartSettings();
                    break;
                }
            case Status.Play:
                {
                    break;
                }
            case Status.Purse:
                {
                    break;
                }
        }
    }
    /// <summary>
    /// GameがStart時に初期化する関数
    /// </summary>
    void ResetVar()
    {
        PlayerNum = 2;
        StartSettingDecideFlag = false;
        for (int i = 0; i < PiecesPosiObjects.Length; i++)
        {
            PiecesPosi[i] = PiecesPosiObjects[i].transform.position;
        }

        for (int i = 0; i < ChoosePiecesPosiObjects.Length; i++)
        {
            ChoosePiecesPosi[i] = ChoosePiecesPosiObjects[i].transform.position;
        }
        PlayerSelectNum = 1;
        PlayerChooseNum = 1;
    }

    /// <summary>
    /// GameがStart時にSettingする関数
    /// </summary>
    void StartSettings()
    {
        StartSettingAxisH = Input.GetAxisRaw("Horizontal");
        if(!StartSettingDecideFlag)
        {
            if (StartSettingAxisH == 1)
            {
                PlayerNum++;
                if (PlayerNum > 4)
                {
                    PlayerNum = 2;
                }
            }
            else if (StartSettingAxisH == -1)
            {
                PlayerNum--;
                if (PlayerNum < 2)
                {
                    PlayerNum = 4;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartSettingDecideFlag = true;
        }
    }


}
