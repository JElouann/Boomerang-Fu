using System;

public class Random
{
    private ulong seed;
    private ulong key;

    public Random(ulong seed, ulong key = 42)
    {
        this.seed = seed;
        this.key = key;
    }

    // Algorithme, variant 64 bit, basé sur l'algorithme SquareRNG : https://arxiv.org/pdf/2004.06278.pdf
    public ulong NextUInt64()
    {
        ulong t, x, y, z;

        // Init -> Premiere modification
        y = x = seed * key;
        z = y * key;

        // Passe 1
        x = (x * x) + y;
        x = (x >> 32) | (x << 32);

        // Passe 2
        x = (x * x) + z;
        x = (x >> 32) | (x << 32);

        // Passe 3
        x = (x * x) + y;
        x = (x >> 32) | (x << 32);

        // Passe 4
        t = x = (x * x) + z;
        x = (x >> 32) | (x << 32);

        // Passe Final
        seed = t ^ (((x * x) + y) >> 32);
        return seed;
    }

    public long NextInt64()
    {
        return Math.Abs((long)NextUInt64());
    }
}
