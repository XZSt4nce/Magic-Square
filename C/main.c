#include <stdio.h>
#include <stdlib.h>

int main() {
    int n;
    printf("Enter the dimension of the square: ");
    scanf("%d", &n);

    int matrix[n][n];

    if (n == 0) {
        printf("Empty square\n");
        return 0;
    }
    else if (n == 1) {
        printf("1");
        return 0;
    }
    else if (n == 2) {
        printf("Impossible\n");
        return 0;
    }
    else if (n % 2 ==  1) {
        int counter = 1;
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {
                matrix[i][j] = 0;
            }
        }
        int r = 0;
        int c = n / 2;
        while (counter <= n * n) {
            if (r == 0) {
                if (matrix[r][c] != 0) {
                    r = -n + 2;
                    c--;
                    if (c == -1) c = n - 1;
                    matrix[n + r][c] = counter;
                }
                else matrix[r][c] = counter;
            }
            else {
                if (matrix[n + r][c] != 0) {
                    if (r == -1) r = -n +1;
                    else if (r == -2) r = -n;
                    else r += 2;
                    c--;
                    if (c == -1)  c = n - 1;
                }
                matrix[n + r][c] = counter;
            }
            r--;
            c++;
            r %= -n;
            c %= n;
            counter++;
        }
    }
    else if (n % 4 == 0) {
        int c = n / 4;
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {
                matrix[i][j] = 0;
            }
        }
        for (int i = 0; i < c; i++)
        {
            for (int j = 0; j < c; j++)
            {
                matrix[i][j] = i * n + j + 1;
                matrix[i][n - j - 1] = (i + 1) * n - j;
                matrix[n - i - 1][j] = (n - i - 1) * n + j + 1;
                matrix[n - i - 1][n - j - 1] = (n - i) * n - j;
            }
        }
        for (int i = c; i < n - c; i++)
        {
            for (int j = c; j < n - c; j++)
            {
                matrix[i][j] = i * n + j + 1;
            }
        }
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (matrix[n - i - 1][n - j - 1] == 0)
                {
                    matrix[n - i - 1][n - j - 1] = i * n + j + 1;
                }
            }
        }
    }
    else {
        int q = n / 2;
        int quadrant[q][q];
        int counter = 1;
        for (int i = 0; i < q; i++) {
            for (int j = 0; j < q; j++) {
                quadrant[i][j] = 0;
            }
        }
        int r = 0;
        int c = q / 2;
        while (counter <= q * q) {
            if (r == 0) {
                if (quadrant[r][c] != 0) {
                    r = -q + 2;
                    c--;
                    if (c == -1) c = q - 1;
                    quadrant[q + r][c] = counter;
                }
                else quadrant[r][c] = counter;
            }
            else {
                if (quadrant[q + r][c] != 0) {
                    if (r == -1) r = -q +1;
                    else if (r == -2) r = -q;
                    else r += 2;
                    c--;
                    if (c == -1)  c = q - 1;
                }
                quadrant[q + r][c] = counter;
            }
            r--;
            c++;
            r %= -q;
            c %= q;
            counter++;
        }
        for (int i = 0; i < q; i++) {
            for (int j = 0; j < q; j++) matrix[i][j] = quadrant[i][j];
            for (int j = q; j < n; j++) matrix[i][j] = quadrant[i][j-q] + q * q * 2;
        }
        for (int i = q; i < n; i++)
        {
            for (int j = 0; j < q; j++) matrix[i][j] = quadrant[i-q][j] + q * q * 3;
            for (int j = q; j < n; j++) matrix[i][j] = quadrant[i - q][j - q] + q * q;
        }
        int f = q / 2;
        int a, d;
        a = matrix[0][0];
        d = matrix[q][0];
        matrix[0][0] = d;
        matrix[q][0] = a;
        a = matrix[q - 1][0];
        d = matrix[n - 1][0];
        matrix[q - 1][0] = d;
        matrix[n - 1][0] = a;
        for (int i = 1; i < q - 1; i++)
        {
            a = matrix[i][1];
            d = matrix[i + q][1];
            matrix[i][1] = d;
            matrix[i + q][1] = a;
        }
        for (int i = 0; i < q; i++)
        {
            for (int j = 0; j < f - 1; j++)
            {
                a = matrix[i][q + j];
                d = matrix[q + i][q + j];
                matrix[i][q + j] = d;
                matrix[q + i][q + j] = a;

                a = matrix[i][q - 1 - j];
                d = matrix[q + i][q - 1 - j];
                matrix[i][q - 1 - j]  = d;
                matrix[q + i][q - 1 - j] = a;
            }
        }
    }

    int spaces = 0;
    for (int i = n * n; i != 0; i /= 10) spaces++;
    for (int i = 0; i < n; i++) {
        int  *row = matrix[i];
        for (int j = 0; j < n; j++) {
            int value = row[j];
            int ls = -1;
            for (int k = value; k != 0; k /= 10) ls++;
            printf("%d", value);
            for (int k = 0; k  < spaces - ls; k++) printf(" ");
        }
        printf("\n");
    }
    system("pause");
    return 0;
}
