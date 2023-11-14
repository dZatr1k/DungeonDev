using System;

public static class ArrayExtensions
{
    public static void Shuffle(this Array array)
    {
        Random random = new Random();
        for (int i = 0; i < array.Length; i++)
        {
            var temp = array.GetValue(i);
            int randomIndex = random.Next(i, array.Length);

            array.SetValue(array.GetValue(randomIndex), i);
            array.SetValue(temp, randomIndex);
        }
    }
}
