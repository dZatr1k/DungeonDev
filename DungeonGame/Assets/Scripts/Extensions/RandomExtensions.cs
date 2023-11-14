using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomExtensions
{
    public static float GetNumberInEpsilonAmbit(float number, float epsilon = 0.5f)
    {
        var delta = Random.Range(-epsilon, epsilon);
        return number + delta;
    }
}
