using System.Collections;
using TMPro;
using UnityEngine;

public class SupplyController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float time = 5;
    [SerializeField] private float valuePerTime = 2;
    [SerializeField] private float maxValue=30;

    private float supplyValue;
    public float SupplyValue
    {
        get => supplyValue;
        set
        {
            if (value > maxValue) supplyValue = maxValue;
            else if(value<=0) supplyValue = 0;
            else supplyValue = value;
            if(text != null) text.text = supplyValue.ToString();
        }
    }

    private void Start()
    {
        StartCoroutine(AddSupplyCoroutine());
    }

    private IEnumerator AddSupplyCoroutine()
    {
        while (true)
        {
            SupplyValue += valuePerTime;
            yield return new WaitForSeconds(time);
        }
    }
}
