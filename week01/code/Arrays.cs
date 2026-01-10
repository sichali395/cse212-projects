using System;
using System.Collections.Generic;

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

        // STEP-BY-STEP PLAN for MultiplesOf:
        // 1. Since the problem states that length is a positive integer greater than 0,
        //    we don't need to validate the input (but I'll add a check for robustness)
        // 2. Create a new array of doubles with the specified length
        // 3. Use a for loop to populate the array:
        //    - For index i from 0 to length-1:
        //    - The value at position i should be: number * (i + 1)
        //    - We use (i + 1) because we want multiples, not zero-based multiples
        //    - Example: For number=7, length=5:
        //      i=0: 7 * 1 = 7
        //      i=1: 7 * 2 = 14
        //      i=2: 7 * 3 = 21
        //      i=3: 7 * 4 = 28
        //      i=4: 7 * 5 = 35
        // 4. Return the populated array

        // Implementation:
        if (length <= 0)
        {
            // Even though the problem says length is positive, 
            // we'll handle edge cases for robustness
            return new double[0];
        }

        // Create the result array
        double[] result = new double[length];

        // Fill the array with multiples
        for (int i = 0; i < length; i++)
        {
            // Calculate the (i+1)th multiple of the number
            result[i] = number * (i + 1);
        }

        return result;
        // TODO Problem 1 End
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

        // STEP-BY-STEP PLAN for RotateListRight:
        // 1. Check for edge cases: if data is null or empty, or if amount is 0, do nothing
        // 2. Since amount can be up to data.Count, we should handle cases where amount equals data.Count
        //    (rotating by the full length brings us back to the original list)
        // 3. The key insight: When rotating right by 'amount', the last 'amount' elements
        //    become the first 'amount' elements in the new order
        // 4. Approach using GetRange (as suggested in the hint):
        //    a. Calculate the split point: data.Count - amount
        //       This gives us the index where the rotation happens
        //    b. Get the right part (elements that will move to the front): 
        //       data.GetRange(splitPoint, amount)
        //    c. Get the left part (elements that will move to the back):
        //       data.GetRange(0, splitPoint)
        //    d. Clear the original list
        //    e. Add the right part first, then the left part

        // Implementation:

        // Step 1: Handle edge cases
        if (data == null || data.Count == 0 || amount <= 0)
        {
            return; // Nothing to rotate
        }

        // Step 2: If amount equals or exceeds the list length, 
        // we can use modulo to get the effective rotation amount
        int effectiveAmount = amount % data.Count;
        if (effectiveAmount == 0)
        {
            return; // Full rotation brings us back to original
        }

        // Step 3: Calculate the split point
        int splitPoint = data.Count - effectiveAmount;

        // Step 4: Get the two parts using GetRange
        // Right part: last 'effectiveAmount' elements
        List<int> rightPart = data.GetRange(splitPoint, effectiveAmount);

        // Left part: first 'splitPoint' elements
        List<int> leftPart = data.GetRange(0, splitPoint);

        // Step 5: Clear the original list
        data.Clear();

        // Step 6: Add the right part first (these were the last elements, now they go to the front)
        data.AddRange(rightPart);

        // Step 7: Add the left part after (these were the first elements, now they go to the back)
        data.AddRange(leftPart);

        // ALTERNATIVE APPROACH (without creating new lists, more memory efficient):
        // We could use a temporary array and manual element copying,
        // but the GetRange approach is cleaner and easier to understand.
        // TODO Problem 2 End
    }
}