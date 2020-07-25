using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodDisplay : MonoBehaviour
{
    [SerializeField]
    Text FoodText = null;

    [SerializeField]
    GameObject[] Hams;

    private void Start()
    {
        if (FoodText == null)
        {
            FoodText = GetComponentInChildren<Text>();
        }
    }

    public void Redisplay(int food)
    {
        if (FoodText)
        {
            FoodText.text = "Food: " + food;
        }
    }
}
