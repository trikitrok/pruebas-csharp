namespace Examples;

public class Counter
{
    public static int CountClumps(int[]? nums)
    {
        if (nums == null || nums.Length == 0)
        {
            return 0;
        }

        var count = 0;
        var prev = nums[0];
        var inClump = false;

        for (var i = 1; i < nums.Length; i++)
        {
            if (nums[i] == prev && !inClump)
            {
                inClump = true;
                count += 1;
            }

            if (nums[i] != prev)
            {
                prev = nums[i];
                inClump = false;
            }
        }
        
        return count;
    }
}
