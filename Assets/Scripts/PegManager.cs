using UnityEngine;

public class PegManager : MonoBehaviour{
    public Material red;

    public Peg createPeg(BattleshipGrid grid, BattleshipGrid opponentShipGrid){
        GameObject pm = createPegModel();
        Peg peg = new Peg(pm, red, grid, opponentShipGrid);

        return peg;
    }

    private GameObject createPegModel(){
        GameObject pegModel = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        pegModel.transform.localScale = new Vector3(1,1.5f,1);
        return pegModel;
    }
}