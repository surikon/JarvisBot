using System;
using System.Collections.Generic;
using System.Text;

namespace Jarvis
{
	class ROT
	{
		public static string DE (string message, int key, int mode, bool stat)
		{
			string result = "";

			if (mode == 0) {
				result = mod0 (message, key, stat);
			} else if (mode == 1) {
				result = mod1 (message, key, stat);
			} else if (mode == 2) {
	//			result = mod2 (message, key, stat);
			}

			return result;
		}
		private static string mod0(string message, int key, bool stat)
		{
			//all ASCII
			string result = ""; int l = message.Length;

			if (stat)
				key *= -1;

			for (int i = 0; i < l; i++) {
				result += Convert.ToChar (message [i] + key);
			}
			return result;
		}
		private static string mod1(string message, int key, bool stat)
		{
			//32-126 Decimal ASCII
			string result = ""; int l = message.Length, shift;

			if (stat)
				key *= -1;

			for (int i = 0; i < l; i++) 
			{
				shift = (Convert.ToInt32(message [i]) + key - 32) % 126;
				result += Convert.ToChar (shift + 32);
			}

			return result;
		} 
		private static string mod2(string message, int key, bool stat)
		{
			string result = ""; int l = message.Length;

			if (stat)
				key *= -1;

			return result;
		}
	}
	class Viesenar
	{
		public static string DE (string message, string key, int mode, bool stat)
		{
			string result = ""; int l = message.Length, keyLength = key.Length;

			for (int i = 0; i < l; i++) {
				result += ROT.DE (Convert.ToString(message [i]),
					(Convert.ToInt32(key [i % keyLength]) - Convert.ToInt32('a') + 1) * (stat ? -1 : 1), mode, stat);
			}

			return result;
		}
	}
	class XOR
	{
		public static string DE(string message, int key)
		{
			string result = "";
			int l = message.Length;

			for (int i = 0; i < l; i++) {
				result += Convert.ToChar (Convert.ToInt32(message[i]) ^ key);
			}

			return result;
		}
	}
	class XOR_Bytes
	{
		private static string hex_convert(long key)
		{
			return Convert.ToString (key, 16);
		}
		public static string XOR_Encrypt(byte[] arr, long key)
		{

			byte[] ans = new byte[arr.Length];
			string hex = hex_convert (key);
			for (long i = 0; i < ans.Length; i++)
				ans[i] = (byte)(arr[i] ^ hex[(int)i % hex.Length]);
			return Encoding.Unicode.GetString(ans);
		}

		public static string XOR_Decrypt(string encrypt, long key)
		{
			byte[] arr = Encoding.Unicode.GetBytes(encrypt);
			return XOR_Encrypt(arr, key);
		}
	}
	public class Codings
	{
		public static string Ceaser(string message, int key, int mode, bool stat)
		{
			return Jarvis.ROT.DE (message, key, mode, stat);
		}
		public static string Viesenar (string message, string key, int mode, bool stat)
		{
			return Jarvis.Viesenar.DE (message, key, mode, stat);
		}
		public static string XOR (string message, int key)
		{
			return Jarvis.XOR.DE (message, key);
		}
	}
}