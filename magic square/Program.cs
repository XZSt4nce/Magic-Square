class Program { 
    static uint[][] odd_numbered (uint n)
    {
        uint counter = 1;
        uint[][] result = new uint[n][];
        for (uint i = 0; i < n; i++)
        {
            result[i] = new uint[n];
            for (uint j = 0; j < n; j++)
            {
                result [i][j] = 0;
            }
        }
        Int64 r = 0,
              c = Convert.ToInt64(n) / 2;
        while (counter <= n * n)
        {

            if (r == 0)
            {
                if (result[r][c] != 0)
                {
                    r = -Convert.ToInt64(n) + 2;
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

    static uint[][] singly_even (uint n)
    {
        uint q = n / 2;
        uint[][] quadrant = odd_numbered(q), result = new uint[n][];
        for (uint i = 0; i < n; i++) result[i] = new uint[n];
        for (uint i = 0; i < q; i++)
        {
            for (uint j = 0; j < q; j++) result[i][j] = quadrant[i][j];
            for (uint j = q; j < n; j++) result[i][j] = quadrant[i][j-q] + q * q * 2;
        }
        for (uint i = q; i < n; i++)
        {
            for (uint j = 0; j < q; j++) result[i][j] = quadrant[i-q][j] + q * q * 3;
            for (uint j = q; j < n; j++) result[i][j] = quadrant[i - q][j - q] + q * q;
        }
        uint f = q / 2;
        uint a, d;
        a = result[0][0];
        d = result[q][0];
        result[0][0] = d;
        result[q][0] = a;
        a = result[q - 1][0];
        d = result[n - 1][0];
        result[q - 1][0] = d;
        result[n - 1][0] = a;
        for (uint i = 1; i < q - 1; i++)
        {
            a = result[i][1];
            d = result[i + q][1];
            result[i][1] = d;
            result[i + q][1] = a;
        }
        for (uint i = 0; i < q; i++)
        {
            for (uint j = 0; j < f - 1; j++)
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

    static uint[][] doubly_even (uint n) {
        uint[][] result = new uint[n][];
        uint c = n / 4;
        for (uint i = 0; i < n; i++)
        {
            result[i] = new uint[n];
            for (uint j = 0; j < n; j++)
            {
                result[i][j] = 0;
            }
        }
        for (uint i = 0; i < c; i++)
        {
            for (uint j = 0; j < c; j++)
            {
                result[i][j] = i * n + j + 1;
                result[i][n - j - 1] = (i + 1) * n - j;
                result[n - i - 1][j] = (n - i - 1) * n + j + 1;
                result[n - i - 1][n - j - 1] = (n - i) * n - j;
            }
        }
        for (uint i = c; i < n - c; i++)
        {
            for (uint j = c; j < n - c; j++)
            {
                result[i][j] = i * n + j + 1;
            }
        }
        for (uint i = 0; i < n; i++)
        {
            for (uint j = 0; j < n; j++)
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
        uint n;
        Console.Title = "Magic Square";
        Console.Write("Enter the dimension of the square: ");
        while (true)
        {
            try
            {
                string? s = Console.ReadLine();
                if (s == null)
                {
                    Console.WriteLine("I/O error occured!");
                    continue;
                }
                n = uint.Parse(s);
                break;
            }
            catch (OverflowException)
            {
                Console.WriteLine("Too large value! Try again");
            }
            catch (FormatException)
            {
                Console.WriteLine("Format error! Try again");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Argument out of range! Try again");
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("Not enough memory!");
                Console.ReadKey();
                return -1;
            }
            catch (IOException)
            {
                Console.WriteLine("I/O error occured!");
                Console.ReadKey();
                return -1;
            }
        }
        uint[][] matrix = new uint[n][];

        if (n == 0)
        {
            Console.WriteLine("Empty square");
            return 0;
        }
        else if (n == 1)
        {
            Console.WriteLine(1);
            return 1;
        }
        else if (n == 2)
        {
            Console.WriteLine("Impossible");
            return 0;
        }
        else if (n % 2 == 1) matrix = odd_numbered(n);
        else if (n % 4 == 0) matrix = doubly_even(n);
        else matrix = singly_even(n);

        int spaces = Convert.ToString(n * n).Length;
        foreach (uint[] row in matrix)
        {
            foreach (uint value in row)
            {
                Console.Write(value);
                for (uint i = 0; i < spaces - Convert.ToString(value).Length + 1; i++) Console.Write(" ");
            }
            Console.WriteLine();
        }
        return 0;
    }
}