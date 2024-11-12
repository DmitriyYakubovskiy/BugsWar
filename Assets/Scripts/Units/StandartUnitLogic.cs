using UnityEngine;

public class StandartUnitLogic : MonoBehaviour
{
    private MoveController moveController;
    private Unit unit;

    private void Start()
    {
        moveController=GetComponent<MoveController>();
        unit=GetComponent<Unit>();
    }

    private void Update()
    {
        if (!moveController.IsMoved)
        {
            moveController.TargetPosition=new Vector3(Random.Range(-9,9),1, Random.Range(-9, 9));
        }
    }
}
