﻿using System;

class Triangle
{
    private double a, b, c;  // Сторони трикутника
    private string color;     // Колір трикутника

    // Конструктор за замовчуванням
    public Triangle(double a = 1.0, double b = 1.0, double c = 1.0, string color = "White")
    {
        this.a = a;
        this.b = b;
        this.c = c;
        this.color = color;
    }

    // Індексатор
    public double this[int index]
    {
        get
        {
            switch (index)
            {
                case 0: return a;  // Відповідає за сторону a
                case 1: return b;  // Відповідає за сторону b
                case 2: return c;  // Відповідає за сторону c
                case 3: throw new ArgumentException("Помилка: індекс 3 доступний тільки для кольору!");
                default: throw new ArgumentOutOfRangeException("Помилка: неправильний індекс!");
            }
        }
        set
        {
            switch (index)
            {
                case 0: a = value; break;
                case 1: b = value; break;
                case 2: c = value; break;
                case 3: throw new ArgumentException("Помилка: індекс 3 доступний тільки для кольору!");
                default: throw new ArgumentOutOfRangeException("Помилка: неправильний індекс!");
            }
        }
    }

    // Перевантаження операції ++ (збільшення всіх сторін на 1)
    public static Triangle operator ++(Triangle t)
    {
        return new Triangle(t.a + 1, t.b + 1, t.c + 1, t.color);
    }

    // Перевантаження операції -- (зменшення всіх сторін на 1)
    public static Triangle operator --(Triangle t)
    {
        return new Triangle(t.a - 1, t.b - 1, t.c - 1, t.color);
    }

    // Перевантаження операції * (множення сторін на скаляр)
    public static Triangle operator *(Triangle t, double scalar)
    {
        return new Triangle(t.a * scalar, t.b * scalar, t.c * scalar, t.color);
    }

    // Перевантаження операції bool (перевірка, чи існує трикутник)
    public static implicit operator bool(Triangle t)
    {
        return (t.a + t.b > t.c && t.a + t.c > t.b && t.b + t.c > t.a);  // Перевірка умови існування трикутника
    }

    // Перетворення Triangle в string (повертає рядок)
    public static explicit operator string(Triangle t)
    {
        return $"Triangle: sides a = {t.a}, b = {t.b}, c = {t.c}, color = {t.color}";
    }

    // Виведення трикутника
    public void Print()
    {
        Console.WriteLine($"Triangle [a={a}, b={b}, c={c}, color={color}]");
    }
}

class VectorUInt
{
    // Поля класу
    protected uint[] IntArray;  // Масив для зберігання елементів вектора
    protected uint size;        // Розмір вектора
    protected int codeError;    // Код помилки
    protected static uint num_vec = 0; // Кількість векторів

    // Конструктор без параметрів
    public VectorUInt()
    {
        this.size = 1;
        IntArray = new uint[1];
        IntArray[0] = 0;
        num_vec++;
        this.codeError = 0;
    }

    // Конструктор з одним параметром (розмір вектора)
    public VectorUInt(uint size)
    {
        this.size = size;
        IntArray = new uint[size];
        for (int i = 0; i < size; i++) IntArray[i] = 0;
        num_vec++;
        this.codeError = 0;
    }

    // Конструктор з двома параметрами (розмір вектора та значення ініціалізації)
    public VectorUInt(uint size, uint initialValue)
    {
        this.size = size;
        IntArray = new uint[size];
        for (int i = 0; i < size; i++) IntArray[i] = initialValue;
        num_vec++;
        this.codeError = 0;
    }

    // Деструктор
    ~VectorUInt()
    {
        Console.WriteLine("Деструктор викликаний для вектора з розміром " + size);
    }

    // Властивість для розміру вектора
    public uint Size
    {
        get { return size; }
    }

    // Властивість для коду помилки
    public int CodeError
    {
        get { return codeError; }
        set { codeError = value; }
    }

    // Статичний метод для підрахунку кількості векторів
    public static uint GetNumVec()
    {
        return num_vec;
    }

    // Метод для введення елементів вектора
    public void Input()
    {
        Console.WriteLine("Введіть елементи вектора:");
        for (int i = 0; i < size; i++)
        {
            IntArray[i] = Convert.ToUInt32(Console.ReadLine());
        }
    }

    // Метод для виведення елементів вектора
    public void Print()
    {
        for (int i = 0; i < size; i++)
        {
            Console.Write(IntArray[i] + " ");
        }
        Console.WriteLine();
    }

    // Індексатор для доступу до елементів вектора
    public uint this[int index]
    {
        get
        {
            if (index >= 0 && index < size)
            {
                return IntArray[index];
            }
            else
            {
                codeError = -1;
                return 0;
            }
        }
        set
        {
            if (index >= 0 && index < size)
            {
                IntArray[index] = value;
                codeError = 0;
            }
            else
            {
                codeError = -1;
            }
        }
    }

    // Перевантаження операції ++ (унарне збільшення на 1)
    public static VectorUInt operator ++(VectorUInt v)
    {
        VectorUInt newVec = new VectorUInt(v.size);
        for (int i = 0; i < v.size; i++)
        {
            newVec.IntArray[i] = v.IntArray[i] + 1;
        }
        return newVec;
    }

    // Перевантаження операції -- (унарне зменшення на 1)
    public static VectorUInt operator --(VectorUInt v)
    {
        VectorUInt newVec = new VectorUInt(v.size);
        for (int i = 0; i < v.size; i++)
        {
            newVec.IntArray[i] = v.IntArray[i] - 1;
        }
        return newVec;
    }

    // Перевантаження операції логічного заперечення !
    public static bool operator !(VectorUInt v)
    {
        if (v.size == 0) return true;

        foreach (var item in v.IntArray)
        {
            if (item != 0) return false;
        }
        return true;
    }

    // Перевантаження операції додавання +
    public static VectorUInt operator +(VectorUInt v1, VectorUInt v2)
    {
        uint newSize = Math.Max(v1.size, v2.size);
        VectorUInt result = new VectorUInt(newSize);

        for (int i = 0; i < newSize; i++)
        {
            if (i < v1.size && i < v2.size)
            {
                result.IntArray[i] = v1.IntArray[i] + v2.IntArray[i];
            }
            else if (i < v1.size)
            {
                result.IntArray[i] = v1.IntArray[i];
            }
            else
            {
                result.IntArray[i] = v2.IntArray[i];
            }
        }

        return result;
    }

    // Перевантаження операції додавання + для вектора і скаляра
    public static VectorUInt operator +(VectorUInt v, int scalar)
    {
        VectorUInt result = new VectorUInt(v.size);
        for (int i = 0; i < v.size; i++)
        {
            result.IntArray[i] = v.IntArray[i] + (uint)scalar;
        }
        return result;
    }

    // Перевантаження операції порівняння ==
    public static bool operator ==(VectorUInt v1, VectorUInt v2)
    {
        if (v1.size != v2.size) return false;
        for (int i = 0; i < v1.size; i++)
        {
            if (v1.IntArray[i] != v2.IntArray[i])
                return false;
        }
        return true;
    }

    // Перевантаження операції порівняння !=
    public static bool operator !=(VectorUInt v1, VectorUInt v2)
    {
        return !(v1 == v2);
    }

    // Перевантаження операції порівняння <
    public static bool operator <(VectorUInt v1, VectorUInt v2)
    {
        for (int i = 0; i < Math.Min(v1.size, v2.size); i++)
        {
            if (v1.IntArray[i] < v2.IntArray[i]) return true;
            if (v1.IntArray[i] > v2.IntArray[i]) return false;
        }
        return v1.size < v2.size;
    }

    // Перевантаження операції порівняння >
    public static bool operator >(VectorUInt v1, VectorUInt v2)
    {
        return v2 < v1;
    }
}

class MatrixUint
{
    private uint[,] IntArray;  // Двовимірний масив для зберігання елементів матриці
    private int n, m;          // Розміри матриці

    // Конструктор за замовчуванням
    public MatrixUint(int n, int m)
    {
        this.n = n;
        this.m = m;
        IntArray = new uint[n, m];
    }

    // Конструктор з ініціалізацією всіх елементів певним значенням
    public MatrixUint(int n, int m, uint initialValue)
    {
        this.n = n;
        this.m = m;
        IntArray = new uint[n, m];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
                IntArray[i, j] = initialValue;
    }

    // Індексатор для доступу до елементів матриці
    public uint this[int i, int j]
    {
        get { return IntArray[i, j]; }
        set { IntArray[i, j] = value; }
    }

    // Перевантаження операції + (додавання двох матриць)
    public static MatrixUint operator +(MatrixUint m1, MatrixUint m2)
    {
        if (m1.n != m2.n || m1.m != m2.m)
            throw new InvalidOperationException("Розміри матриць не співпадають!");

        MatrixUint result = new MatrixUint(m1.n, m1.m);
        for (int i = 0; i < m1.n; i++)
            for (int j = 0; j < m1.m; j++)
                result.IntArray[i, j] = m1.IntArray[i, j] + m2.IntArray[i, j];

        return result;
    }

    // Перевантаження операції * (множення матриці на скаляр)
    public static MatrixUint operator *(MatrixUint m, uint scalar)
    {
        MatrixUint result = new MatrixUint(m.n, m.m);
        for (int i = 0; i < m.n; i++)
            for (int j = 0; j < m.m; j++)
                result.IntArray[i, j] = m.IntArray[i, j] * scalar;

        return result;
    }

    // Перевантаження операції % (остання від ділення для двох матриць)
    public static MatrixUint operator %(MatrixUint m1, MatrixUint m2)
    {
        if (m1.n != m2.n || m1.m != m2.m)
            throw new InvalidOperationException("Розміри матриць не співпадають!");

        MatrixUint result = new MatrixUint(m1.n, m1.m);
        for (int i = 0; i < m1.n; i++)
            for (int j = 0; j < m1.m; j++)
                result.IntArray[i, j] = m1.IntArray[i, j] % m2.IntArray[i, j];

        return result;
    }

    // Метод для виведення елементів матриці
    public void OutputMatrix()
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
                Console.Write(IntArray[i, j] + "\t");
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main()
    {
        // Тестування класу Triangle
        Triangle t1 = new Triangle(3.0, 4.0, 5.0, "Red");

        // Використовуємо індексатор
        Console.WriteLine("Side a: " + t1[0]);
        Console.WriteLine("Side b: " + t1[1]);
        Console.WriteLine("Side c: " + t1[2]);
        
        // Перевірка існування трикутника
        if (t1)
        {
            Console.WriteLine("Трикутник існує!");
        }
        else
        {
            Console.WriteLine("Трикутник не існує!");
        }

        // Перевантаження операцій ++ і --
        t1++;
        t1.Print();
        t1--;
        t1.Print();

        // Перевантаження операції *
        Triangle t2 = t1 * 2;
        t2.Print();

        // Перетворення Triangle в string
        string triangleString = (string)t2;
        Console.WriteLine(triangleString);

        // Тестування класу VectorUInt
        VectorUInt v1 = new VectorUInt(5);
        v1[0] = 1;
        v1[1] = 2;
        v1[2] = 3;
        v1[3] = 4;
        v1[4] = 5;

        v1.Print();

        VectorUInt v2 = new VectorUInt(5);
        v2[0] = 5;
        v2[1] = 4;
        v2[2] = 3;
        v2[3] = 2;
        v2[4] = 1;

        v2.Print();

        VectorUInt sum = v1 + v2;
        sum.Print();

        // Тестування класу MatrixUint
        MatrixUint matrix1 = new MatrixUint(2, 2);
        matrix1[0, 0] = 1;
        matrix1[0, 1] = 2;
        matrix1[1, 0] = 3;
        matrix1[1, 1] = 4;
        
        MatrixUint matrix2 = new MatrixUint(2, 2, 5);
        
        Console.WriteLine("Matrix 1:");
        matrix1.OutputMatrix();
        
        Console.WriteLine("Matrix 2:");
        matrix2.OutputMatrix();

        MatrixUint result = matrix1 + matrix2;
        Console.WriteLine("Matrix 1 + Matrix 2:");
        result.OutputMatrix();
    }
}
