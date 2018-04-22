using System;
using System.Net;
using System.Text;
using System.Collections.Generic;

namespace Jarvis
{
	public class MyRSAClass
	{
		public class PrimeTest
		{
			public static long quickMod(long x, long y, long p)
			{
				long res = 1;
				x %= p;
				while (y > 0)
				{
					if (y % 2 == 1)
						res = (res * x) % p;
					y /= 2;
					x = (x * x) % p;
				}
				return res;
			}

			private static bool MillerTest(int d, int n)
			{
				Random rnd = new Random();
				int a = rnd.Next(2, n - 2);
				int x = (int)quickMod(a, d, n);

				if (x == 1 || x == n - 1)
					return true;

				while (d < n - 1)
				{
					x = (x * x) % n;
					d *= 2;

					if (x == 1)
						return false;
					if (x == n - 1)
						return true;
				}
				return false;
			}

			public static bool isPrime(int n, int k)
			{
				if (n <= 1 || n == 4) return false;
				if (n <= 3) return true;

				int d = n - 1;

				while (d % 2 == 0)
					d /= 2;

				for (int i = 0; i < k; i++)
					if (MillerTest(d, n) == false)
						return false;

				return true;
			}
		}

		class RSA
		{
			private static int p = 0, q = 0, tmp1, tmp2;
			private static long N, f, d, e;
			private static List <int> prime = new List <int> ();

			private static long gcd(long a, long b)
			{
				while (b != 0)
					b = a % (a = b);
				return a;
			}

			public static long[] getKeys()
			{
				Random rnd = new Random ();

				tmp1 = rnd.Next (0, Math.Min(prime.Count, 50));
				tmp2 = rnd.Next (0, Math.Min(prime.Count, 50));

				if (tmp1 == tmp2)
					tmp1 += rnd.Next (5, 10);

				p = prime [tmp1];
				q = prime [tmp2];

				N = (long)(p * q);
				f = (long)((p - 1) * (q - 1));

				for (int i = 1; i < Math.Min(f, prime.Count); i++) {
					if ((long)prime [i] > (f % 2 == 0 ? f / 2 : f / 2 + 1)) {
						e = (long)Convert.ToInt64 (prime [i]);
						break;
					}
				}

				d = reverse(e, f); //ЗАПИЛИТЬ ФУНКЦИЮ МУЛЬТИПЛИКАТИВНОГО ОБРАТНОГО

				long[] key = new long[3];

				key [0] = N;
				key [1] = e;
				key [2] = d;

				return key;
			}

			public static long LongRandom(int min, long max, Random rand) {
				byte[] buf = new byte[8];
				rand.NextBytes(buf);
				long longRand = BitConverter.ToInt64(buf, 0);

				return (Math.Abs(longRand % (max - min)) + min);
			}

			private static long reverse(long e, long f)
			{
				long d = 10;

				while (true)
				{
					if ((d * e) % f == 1)
						break;
					else
						d++;
				}

				return d;
			}

			public static void getPrimeNums()
			{
				for (int i = 100; i < int.MaxValue - 999999; i++) {
					if (PrimeTest.isPrime (i, 3)) {
						prime.Add (i);
					}
				}
			}
		}
	}
}