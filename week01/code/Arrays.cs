public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // Plan:
        // Step 1: Create a new array of type double and size 'length'.
        // Step 2: Loop from 0 to length -1.
        // Step 3: In each loop iteration, multiply 'number' by (i + 1) and store the result
        // Step 4: return the filled array

        double[] result = new double[length];

        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1);
        }

        return result; // replace this return statement with your own
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.


        // Plan:
        // Step 1: Determine the number of elements to move from the end of the list to the front (amount).
        // Step 2: Use GetRange to extract the last 'amount' items from the list.
        // Step 3: Use GetRange again to get the remaining items at the start of the list.
        // Step 4: Clear the original list.
        // Step 5: Add the last 'amount' items to the front, followed by the remaining items.
        // This avoids modifying the list in-place during iteration.

        // Step 2: Get the tail (last 'amount' items)
        List<int> tail = data.GetRange(data.Count - amount, amount);

        // Step 3: Get the head (first 'data.Count - amount' items)
        List<int> head = data.GetRange(0, data.Count - amount);

        // Step 4: Clear the original list
        data.Clear();

        // Step 5: Add the rotated elements
        data.AddRange(tail);
        data.AddRange(head);
    }
}
