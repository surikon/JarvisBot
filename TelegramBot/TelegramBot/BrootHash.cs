using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Jarvis
{ 
	class HashSHA1
	{
		public static string Encrypt(string source)
		{
			try
			{
				using (SHA1Managed sha1 = new SHA1Managed ()) {
					var hash = sha1.ComputeHash (Encoding.UTF8.GetBytes (source));
					var sb = new StringBuilder (hash.Length * 2);

					foreach (byte b in hash) {
						sb.Append (b.ToString ("X2"));
					}

					return sb.ToString ();
				}
			}
			catch {
				return "";
			}
		}
	}

	class HashSHA256
	{
		public static string Encrypt(string source)
		{
			try
			{
				var crypt = new SHA256Managed();
				var hash = new System.Text.StringBuilder();
				byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(source));
				foreach (byte theByte in crypto)
				{
					hash.Append(theByte.ToString("x2"));
				}
				return hash.ToString();
			}
			catch {
				return "";
			}
		}
	}

	class HashMD5
	{
		public static string Encrypt(string source)
		{
			using (MD5 md5Hash = MD5.Create())
			{
				string hash = GetMd5Hash(md5Hash, source);

				if (VerifyMd5Hash(md5Hash, source, hash))
					return hash;
				else
					return "";
			}
		}

		private static string GetMd5Hash(MD5 md5Hash, string input)
		{
			byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

			StringBuilder sBuilder = new StringBuilder();

			for (int i = 0; i < data.Length; i++)
				sBuilder.Append(data[i].ToString("x2"));
	
			return sBuilder.ToString();
		}
			
		private static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
		{
			string hashOfInput = GetMd5Hash(md5Hash, input);
			StringComparer comparer = StringComparer.OrdinalIgnoreCase;

			if (0 == comparer.Compare(hashOfInput, hash))
				return true;
			else
				return false;
		}
	}

	public class BrootHash
	{
		private string orig_hash;
		private string rec_hash;
		private string orig_text;
		private string rec_text;
		private string hash_type;

		public BrootHash(string hash_type) 
		{	
			this.hash_type = hash_type;
		}

		public void SetOrigHash(string hash)
		{
			this.orig_hash = hash;
		}

		public void SetOrigText(string text)
		{
			this.orig_text = text;
		}

		public string getHash()
		{
			switch (this.hash_type) 
			{
			case "md5":
				return HashMD5.Encrypt (this.orig_text);
			case "sha1":
				return HashSHA1.Encrypt (this.orig_text);
			case "sha256":
				return HashSHA256.Encrypt (this.orig_text);
			}
			return "";
		}
	}
}