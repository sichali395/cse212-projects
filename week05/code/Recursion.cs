using System.Collections;
using System.Collections.Generic;

public static class Recursion
{
    /// <summary>
    /// #############
    /// # Problem 1 #
    /// #############
    /// Using recursion, find the sum of 1^2 + 2^2 + 3^2 + ... + n^2
    /// and return it.  Remember to both express the solution 
    /// in terms of recursive call on a smaller problem and 
    /// to identify a base case (terminating case).  If the value of
    /// n <= 0, just return 0.   A loop should not be used.
    /// </summary>
    public static int SumSquaresRecursive(int n)
    {
        // TODO Start Problem 1
        // Base case: if n <= 0, return 0
        if (n <= 0)
        {
            return 0;
        }
        // Recursive case: n² + sum of squares from 1 to n-1
        return n * n + SumSquaresRecursive(n - 1);
        // TODO End Problem 1
    }

    /// <summary>
    /// #############
    /// # Problem 2 #
    /// #############
    /// Using recursion, insert permutations of length
    /// 'size' from a list of 'letters' into the results list.  This function
    /// should assume that each letter is unique (i.e. the 
    /// function does not need to find unique permutations).
    ///
    /// In mathematics, we can calculate the number of permutations
    /// using the formula: len(letters)! / (len(letters) - size)!
    ///
    /// For example, if letters was [A,B,C] and size was 2 then
    /// the following would the contents of the results array after the function ran: AB, AC, BA, BC, CA, CB (might be in 
    /// a different order).
    ///
    /// You can assume that the size specified is always valid (between 1 
    /// and the length of the letters list).
    /// </summary>
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        // TODO Start Problem 2
        // Base case: if we've reached the desired size, add to results
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        // Recursive case: try adding each available letter
        for (int i = 0; i < letters.Length; i++)
        {
            char currentChar = letters[i];
            // Check if this character is already used in current permutation
            bool alreadyUsed = false;
            for (int j = 0; j < word.Length; j++)
            {
                if (word[j] == currentChar)
                {
                    alreadyUsed = true;
                    break;
                }
            }

            if (!alreadyUsed)
            {
                // Create new permutation by adding current letter
                PermutationsChoose(results, letters, size, word + currentChar);
            }
        }
        // TODO End Problem 2
    }

    /// <summary>
    /// #############
    /// # Problem 3 #
    /// #############
    /// Imagine that there was a staircase with 's' stairs.  
    /// We want to count how many ways there are to climb 
    /// the stairs.  If the person could only climb one 
    /// stair at a time, then the total would be just one.  
    /// However, if the person could choose to climb either 
    /// one, two, or three stairs at a time (in any order), 
    /// then the total possibilities become much more 
    /// complicated.  If there were just three stairs,
    /// the possible ways to climb would be four as follows:
    ///
    ///     1 step, 1 step, 1 step
    ///     1 step, 2 step
    ///     2 step, 1 step
    ///     3 step
    ///
    /// With just one step to go, the ways to get
    /// to the top of 's' stairs is to either:
    /// 
    /// - take a single step from the second to last step, 
    /// - take a double step from the third to last step, 
    /// - take a triple step from the fourth to last step
    ///
    /// We don't need to think about scenarios like taking two 
    /// single steps from the third to last step because this
    /// is already part of the first scenario (taking a single
    /// step from the second to last step).
    ///
    /// These final leaps give us a sum:
    ///
    /// CountWaysToClimb(s) = CountWaysToClimb(s-1) + 
    ///                       CountWaysToClimb(s-2) +
    ///                       CountWaysToClimb(s-3)
    ///
    /// To run this function for larger values of 's', you will need
    /// to update this function to use memoization.  The parameter
    /// 'remember' has already been added as an input parameter to 
    /// the function for you to complete this task.
    /// </summary>
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        // Base Cases
        if (s == 0)
            return 0;
        if (s == 1)
            return 1;
        if (s == 2)
            return 2;
        if (s == 3)
            return 4;

        // TODO Start Problem 3
        // Initialize memoization dictionary if null
        if (remember == null)
        {
            remember = new Dictionary<int, decimal>();
        }

        // Check if already calculated
        if (remember.ContainsKey(s))
        {
            return remember[s];
        }

        // Solve using recursion with memoization
        decimal ways = CountWaysToClimb(s - 1, remember) +
                      CountWaysToClimb(s - 2, remember) +
                      CountWaysToClimb(s - 3, remember);

        // Store in memoization dictionary
        remember[s] = ways;
        return ways;
        // TODO End Problem 3
    }

    /// <summary>
    /// #############
    /// # Problem 4 #
    /// #############
    /// A binary string is a string consisting of just 1's and 0's.  For example, 1010111 is 
    /// a binary string.  If we introduce a wildcard symbol * into the string, we can say that 
    /// this is now a pattern for multiple binary strings.  For example, 101*1 could be used 
    /// to represent 10101 and 10111.  A pattern can have more than one * wildcard.  For example, 
    /// 1**1 would result in 4 different binary strings: 1001, 1011, 1101, and 1111.
    ///	
    /// Using recursion, insert all possible binary strings for a given pattern into the results list.  You might find 
    /// some of the string functions like IndexOf and [..X] / [X..] to be useful in solving this problem.
    /// </summary>
    public static void WildcardBinary(string pattern, List<string> results)
    {
        // TODO Start Problem 4
        WildcardBinaryHelper(pattern, "", results);
        // TODO End Problem 4
    }

    // Helper method for Problem 4
    private static void WildcardBinaryHelper(string remainingPattern, string current, List<string> results)
    {
        // Base case: if we've processed all characters
        if (remainingPattern.Length == 0)
        {
            results.Add(current);
            return;
        }

        // Get the next character
        char nextChar = remainingPattern[0];
        string nextRemaining = remainingPattern.Substring(1);

        if (nextChar == '*')
        {
            // Two possibilities: replace * with 0 or 1
            WildcardBinaryHelper(nextRemaining, current + "0", results);
            WildcardBinaryHelper(nextRemaining, current + "1", results);
        }
        else
        {
            // Regular character, just add it
            WildcardBinaryHelper(nextRemaining, current + nextChar, results);
        }
    }

    /// <summary>
    /// Use recursion to insert all paths that start at (0,0) and end at the
    /// 'end' square into the results list.
    /// </summary>
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<(int, int)>? currPath = null)
    {
        // If this is the first time running the function, then we need
        // to initialize the currPath list.
        if (currPath == null)
        {
            currPath = new List<(int, int)>();
        }

        // currPath.Add((1,2)); // Use this syntax to add to the current path

        // TODO Start Problem 5
        // ADD CODE HERE

        // Add current position to path
        currPath.Add((x, y));

        // Check if we've reached the end
        if (maze.IsEnd(x, y))
        {
            // Add complete path to results
            results.Add(AsString(currPath));
            // Backtrack: remove current position before returning
            currPath.RemoveAt(currPath.Count - 1);
            return;
        }

        // Define possible moves: right, down, left, up
        List<(int, int)> moves = new List<(int, int)>
        {
            (1, 0),   // right
            (0, 1),   // down
            (-1, 0),  // left
            (0, -1)   // up
        };

        // Try each possible move
        foreach (var move in moves)
        {
            int newX = x + move.Item1;
            int newY = y + move.Item2;

            // Check if move is valid - use the correct parameter order from Maze class
            if (maze.IsValidMove(currPath, newX, newY))
            {
                // Recursively explore from new position
                SolveMaze(results, maze, newX, newY, currPath);
            }
        }

        // Backtrack: remove current position before returning to explore other paths
        currPath.RemoveAt(currPath.Count - 1);

        // TODO End Problem 5

        // results.Add(currPath.AsString()); // Use this to add your path to the results array keeping track of complete maze solutions when you find the solution.
    }

    // Helper method to convert path to string format matching test expectations
    private static string AsString(List<(int, int)> path)
    {
        System.Text.StringBuilder result = new System.Text.StringBuilder();
        result.Append("<List>{");
        for (int i = 0; i < path.Count; i++)
        {
            result.Append($"({path[i].Item1}, {path[i].Item2})");
            if (i < path.Count - 1)
                result.Append(", ");
        }
        result.Append("}");
        return result.ToString();
    }
}