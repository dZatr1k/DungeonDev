using UnityEngine;

public static class RandomExtensions
{
    public static float GetNumberInEpsilonAmbit(float number, float epsilon = 1f)
    {
        var delta = Random.Range(0, epsilon);
        return number + delta;
    }
}
