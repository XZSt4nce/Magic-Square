class Program { 
    static int[][] odd_numbered (int n)
    {
        int counter = 1;
        int[][] result = new int[n][];
        for (int i = 0; i < n; i++)
        {
            result[i] = new int[n];
            for (int j = 0; j < n; j++)
            {
                result [i][j] = 0;
            }
        }
        int r = 0,
            c = n / 2;
        while (counter <= n * n)
        {

            if (r == 0)
            {
                if (result[r][c] != 0)
                {
                    r = -n + 2;
                    c--;
                    if (c == -1) c = n - 1;
                    result[n + r][c] = counter;
                }
                else
                {
                    result[r][c] = counter;
                }
            }
            else
            {
                if (result[n+r][c] != 0)
                {
                    if (r == -1) r = -n + 1;
                    else if (r == -2) r = -n;
                    else r += 2;
                    c--;
                    if (c == -1) c = n - 1;
                }
                result[n + r][c] = counter;
            }
            r--;
            c++;
            r %= -n;
            c %= n;
            counter++;
        }
        return result;
    }

    static int[][] singly_even (int n)
    {
        int q = n / 2;
        int[][] quadrant = odd_numbered(q), result = new int[n][];
        for (int i = 0; i < n; i++) result[i] = new int[n];
        for (int i = 0; i < q; i++)
        {
            for (int j = 0; j < q; j++) result[i][j] = quadrant[i][j];
            for (int j = q; j < n; j++) result[i][j] = quadrant[i][j-q] + q * q * 2;
        }
        for (int i = q; i < n; i++)
        {
            for (int j = 0; j < q; j++) result[i][j] = quadrant[i-q][j] + q * q * 3;
            for (int j = q; j < n; j++) result[i][j] = quadrant[i-q][j-q] + q * q;
        }
        int f = q / 2;
        int a, d;
        a = result[0][0];
        d = result[q][0];
        result[0][0] = d;
        result[q][0] = a;
        a = result[q - 1][0];
        d = result[n - 1][0];
        result[q - 1][0] = d;
        result[n - 1][0] = a;
        for (int i = 1; i < q - 1; i++)
        {
            a = result[i][1];
            d = result[i + q][1];
            result[i][1] = d;
            result[i + q][1] = a;
        }
        for (int i = 0; i < q; i++)
        {
            for (int j = 0; j < f - 1; j++)
            {
                a = result[i][q + j];
                d = result[q + i][q + j];
                result[i][q + j] = d;
                result[q + i][q + j] = a;

                a = result[i][q - 1 - j];
                d = result[q + i][q - 1 - j];
                result[i][q - 1 - j]  = d;
                result[q + i][q - 1 - j] = a;
            }
        }
        return result;
    }

    static int[][] doubly_even (int n) {
        int[][] result = new int[n][];
        int c = n / 4;
        for (int i = 0; i < n; i++)
        {
            result[i] = new int[n];
            for (int j = 0; j < n; j++)
            {
                result[i][j] = 0;
            }
        }
        for (int i = 0; i < c; i++)
        {
            for (int j = 0; j < c; j++)
            {
                result[i][j] = i * n + j + 1;
                result[i][n - j - 1] = (i + 1) * n - j;
                result[n - i - 1][j] = (n - i - 1) * n + j + 1;
                result[n - i - 1][n - j - 1] = (n - i) * n - j;
            }
        }
        for (int i = c; i < n - c; i++)
        {
            for (int j = c; j < n - c; j++)
            {
                result[i][j] = i * n + j + 1;
            }
        }
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (result[n - i - 1][n - j - 1] == 0)
                {
                    result[n - i - 1][n - j - 1] = i * n + j + 1;
                }
            }
        }
        return result;
    }

    static int Main()
    {
        int n;
        Console.Title = "Magic Square";
        Console.Write("Enter the dimension of the square: ");
        n = int.Parse(Console.ReadLine());
        int[][] matrix = new int[n][];

        if (n == 2)
        {
            Console.WriteLine("Impossible");
            return 0;
        }
        else if (n % 2 == 1) matrix = odd_numbered(n);
        else if (n % 4 == 0) matrix = doubly_even(n);
        else matrix = singly_even(n);

        int spaces = Convert.ToString(n * n).Length;
        foreach (int[] row in matrix)
        {
            foreach (int value in row)
            {
                Console.Write(value);
                for (int i = 0; i < spaces - Convert.ToString(value).Length + 1; i++) Console.Write(" ");
            }
            Console.WriteLine();
        }
        return 0;
    }
}