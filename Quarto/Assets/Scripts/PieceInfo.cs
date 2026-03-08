using UnityEngine;
public enum P_Color
{
    None,
    Black,
    White
}

public enum P_Height
{
    None,
    High,
    Low
}

public enum P_Shape
{
    None,
    Circle,
    Square
}

public enum P_Hole
{
    None,
    Yes,
    No
}
[System.Serializable]
public class Pieces
{
    [Header("éÌóﬁ")]
    public P_Color color = P_Color.None;
    public P_Height height = P_Height.None;
    public P_Shape shape = P_Shape.None;
    public P_Hole hole = P_Hole.None;
}


public class PieceInfo : MonoBehaviour
{
    int Number;
    [Header("èÓïÒ"), SerializeField] Pieces pieceInfo;
    string pieceName;
    GameObject model;
    Vector2 position;

    public void InitializationInfo()
    {
        model = gameObject;
        pieceName = model.name;
        model.name = "Model";
        string[] kinds = pieceName.Split('_');
        
        // ãÓÇÃèÓïÒÇéÊìæ
        pieceInfo = new Pieces();
        pieceInfo.color  = (kinds[0] == "Black")  ? P_Color.Black  : (kinds[0] == "White")  ? P_Color.White  : P_Color.None;
        pieceInfo.height = (kinds[1] == "High")   ? P_Height.High  : (kinds[1] == "Low")    ? P_Height.Low   : P_Height.None;
        pieceInfo.shape  = (kinds[2] == "Circle") ? P_Shape.Circle : (kinds[2] == "Square") ? P_Shape.Square : P_Shape.None;
        pieceInfo.hole   = (kinds[3] == "Yes")    ? P_Hole.Yes     : (kinds[3] == "No")     ? P_Hole.No      : P_Hole.None;
    }

    public void SetPosition(GameObject destinationObject, Vector2 destination)
    {
        model.transform.position = destinationObject.transform.position;
        position = destination;
    }

    public void SetMaterial(Material changeMaterial)
    {
        model.GetComponent<MeshRenderer>().material = changeMaterial;
    }
}
