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
        // Declare a variable for an array of length-elements
        double[] multiples = new double[length];

        // Start a for loop that iterates length-times, with i as 1
        // Multiply number by i through each interaction, and add to the array at the [i - 1] position (because arrays are 0 indexed)
        for (int i = 1; i <= length; i++) {
            multiples[i - 1] = number * i;
        }

        // Return the array.
        return multiples;
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
        // Get Range to be rotated and insert it at the start of data (index 0)
        // "data.Count - amount" gives the starting index to get the elements from and
        // "amount" equals the count of elements to be rotated
        data.InsertRange(0, data.GetRange(data.Count - amount, amount));

        // Remove elements rotated from right side of data
        data.RemoveRange(data.Count - amount, amount);
    }
}
