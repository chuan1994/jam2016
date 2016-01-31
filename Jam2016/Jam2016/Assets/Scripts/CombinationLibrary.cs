using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombinationLibrary {

    private static CombinationLibrary instance;

    private static Dictionary<int[], int> combination = new Dictionary<int[], int>();

    private CombinationLibrary()
    {
        combination.Add(new int[] { 0, 0 }, 3);
        combination.Add(new int[] { 1, 2 }, 4);
        combination.Add(new int[] { 0, 2 }, 5);
        combination.Add(new int[] { 1, 1 }, 6);
        combination.Add(new int[] { 2, 2 }, 7);
        combination.Add(new int[] { 4, 4 }, 8);
        combination.Add(new int[] { 0, 1 }, 9);
        combination.Add(new int[] { 3, 6 }, 10);
        combination.Add(new int[] { 4, 6 }, 11);
        combination.Add(new int[] { 7, 9 }, 12);
        combination.Add(new int[] { 5, 9 }, 13);
        combination.Add(new int[] { 3, 3 }, 14);
        combination.Add(new int[] { 3, 7 }, 15);
    }

    public static CombinationLibrary Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CombinationLibrary();
            }
            return instance;
        }
    }

    public static int GetCombinationResult(List<GameObject> pair)
    {
        if (instance == null)
        {
            instance = new CombinationLibrary();
        }
        int num1 = pair[0].GetComponent<BaseIngredientController>().id;
        int num2 = pair[1].GetComponent<BaseIngredientController>().id;
        int temp;
        if (num2 < num1)
        {
            temp = num2;
            num2 = num1;
            num1 = temp;
        }
        foreach (KeyValuePair<int[], int> entry in combination)
        {
            if (num1 == entry.Key[0] && num2 == entry.Key[1])
            {
                return entry.Value;
            }
        }
        return 404;
    }
}
