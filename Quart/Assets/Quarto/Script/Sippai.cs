using UnityEngine;

public class Sippai : MonoBehaviour
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
                    StartPiecesPosi();
                    status = Status.Play;
                    break;
                }
            case Status.Play:
                {
                    if (!PlayerFlag)
                    {
                        PlayerChooseMove();
                        ChoosePieceMove();
                        GetPlayerChoosePiece();
                    }
                    else if (PlayerFlag)
                    {
                        PlayerSelectMove();
                        SelectPieceMove();
                        GetOnPiecePosi();
                    }
                    break;
                }
            case Status.Purse:
                {
                    break;
                }
        }

    }

    void StartPiecesPosi()
    {
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

    // PlayerFlag = false Choose
    void PlayerChooseMove()
    {
        AxisH = Input.GetAxisRaw("Horizontal");
        AxisV = Input.GetAxisRaw("Vertical");

        if (AxisH == 1 && !KeepOneShot)
        {
            IncrementPlayerChooseNum();
        }
        else if (AxisH == -1 && !KeepOneShot)
        {
            DecrementPlayerChooseNum();
        }
        else if (AxisV == 1 && !KeepOneShot)
        {
            PlayerChooseNum -= 4;
            if (PlayerChooseNum < 1)
            {
                PlayerChooseNum += 16;
            }
            SkipChoosePositionsPlus();  // 空いていない場所を飛ばす
            KeepOneShot = true;
        }
        else if (AxisV == -1 && !KeepOneShot)
        {
            PlayerChooseNum += 4;
            if (PlayerChooseNum > 16)
            {
                PlayerChooseNum -= 16;
            }
            SkipChoosePositionsPlus();  // 空いていない場所を飛ばす
            KeepOneShot = true;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            DecideFlag = true;
        }

        if (AxisH == 0 && AxisV == 0)
        {
            KeepOneShot = false;
        }
    }


    void IncrementPlayerChooseNum()
    {
        PlayerChooseNum++;
        if (PlayerChooseNum > 16)
        {
            PlayerChooseNum = 1;
        }
        SkipChoosePositionsPlus();  // 空いていない場所を飛ばす
        KeepOneShot = true;
    }
    void DecrementPlayerChooseNum()
    {
        PlayerChooseNum--;
        if (PlayerChooseNum < 1)
        {
            PlayerChooseNum = 16;
        }
        SkipChoosePositionsMinus();  // 空いていない場所を飛ばす
        KeepOneShot = true;
    }
    void SkipChoosePositionsPlus()
    {
        for (int i = 0; i < OnPiecesNum.Length; i++)
        {
            if (OnPiecesNum[i] == PlayerChooseNum)
            {
                PlayerChooseNum++;
            }
        }
    }
    void SkipChoosePositionsMinus()
    {
        for (int i = 0; i < OnPiecesNum.Length; i++)
        {
            if (OnPiecesNum[i] == PlayerChooseNum)
            {
                PlayerChooseNum--;
            }
        }
    }


    void ChoosePieceMove()
    {
        for (int i = 0; i <= ChoosePiecesPosi.Length; i++)
        {
            if (PlayerChooseNum == i)
            {
                PlayerChooseObject.transform.position = ChoosePiecesPosi[i];
            }
        }
    }


    void GetPlayerChoosePiece()
    {
        if (DecideFlag)
        {
            PlayerChooseObject = Pieces[PlayerSelectNum];
            PlayerSelectNum = 1;
            PlayerSelectObject.SetActive(false);
            PlayerFlag = true;
            DecideFlag = false;
        }
    }

    // ↓↓PlayerFlag = true
    void PlayerSelectMove()
    {
        AxisH = Input.GetAxisRaw("Horizontal");
        AxisV = Input.GetAxisRaw("Vertical");

        if (AxisH == 1 && !KeepOneShot)
        {
            IncrementPlayerSelectNum();
        }
        else if (AxisH == -1 && !KeepOneShot)
        {
            DecrementPlayerSelectNum();
        }
        else if (AxisV == 1 && !KeepOneShot)
        {
            PlayerSelectNum -= 4;
            if (PlayerSelectNum < 1)
            {
                PlayerSelectNum += 16;
            }
            SkipSelectPositionsPlus();  // 空いていない場所を飛ばす
            KeepOneShot = true;
        }
        else if (AxisV == -1 && !KeepOneShot)
        {
            PlayerSelectNum += 4;
            if (PlayerSelectNum > 16)
            {
                PlayerSelectNum -= 16;
            }
            SkipSelectPositionsMinus();  // 空いていない場所を飛ばす
            KeepOneShot = true;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            DecideFlag = true;
        }

        if (AxisH == 0 && AxisV == 0)
        {
            KeepOneShot = false;
        }
    }


    void IncrementPlayerSelectNum()
    {
        PlayerSelectNum++;
        if (PlayerSelectNum > 16)
        {
            PlayerSelectNum = 1;
        }
        SkipChoosePositionsPlus();  // 空いていない場所を飛ばす
        KeepOneShot = true;
    }
    void DecrementPlayerSelectNum()
    {
        PlayerSelectNum--;
        if (PlayerSelectNum < 1)
        {
            PlayerSelectNum = 16;
        }
        SkipChoosePositionsMinus();  // 空いていない場所を飛ばす
        KeepOneShot = true;
    }
    void SkipSelectPositionsPlus()
    {
        for (int i = 0; i < OnPiecesNum.Length; i++)
        {
            if (OnPiecesNum[i] == PlayerSelectNum)
            {
                PlayerSelectNum++;
            }
        }
    }
    void SkipSelectPositionsMinus()
    {
        for (int i = 0; i < OnPiecesNum.Length; i++)
        {
            if (OnPiecesNum[i] == PlayerSelectNum)
            {
                PlayerSelectNum--;
            }
        }
    }

    void SelectPieceMove()
    {
        for (int i = 0; i <= PiecesPosi.Length; i++)
        {
            if (PlayerSelectNum == i)
            {
                PlayerChooseObject.transform.position = PiecesPosi[i];
            }
        }
    }


    void GetOnPiecePosi()
    {
        if (DecideFlag)
        {
            DecidePosi = PlayerChooseObject.transform.position;

            OnPieces[PieceOrderNum] = PlayerChooseObject;
            OnPiecesNum[PieceOrderNum] = PlayerSelectNum;
            PieceOrderNum++;
            PlayerChooseNum = 1;
            PlayerChooseObject = PlayerSelectObject;
            PlayerSelectObject.SetActive(true);
            PlayerFlag = false;
            DecideFlag = false;
        }
    }
}
