using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Web;
using System.Linq;
using System.Globalization;

namespace Jarvis
{
	class JSON
	{
		public static string getValByKey(string json, string key)
		{
			int json_size = json.Length, key_size = key.Length, j, val_size = 0, start_position;
			string result = "";

			for (int i = 0; i < json_size - key_size; i++) {
				if (json.Substring (i, key_size) == key) {
					j = i + key_size + 2;
					while (json [j - 1] != '"') {
						j++;
					}

					start_position = j;

					while (json [j] != '"') {
						val_size++; j++;
					}

					result = json.Substring (start_position, val_size);
					break;
				}
			}

			return result;
		}
	}
	class Mail
	{
		public static void SendEmail(string receiverMailAddress, string SubjectMail, string TextMail, string AttachmentPath = "")
		{
			MailAddress from = new MailAddress ("jarvis.smtp@gmail.com", "JARVIS SMTP");
			MailAddress to = new MailAddress (receiverMailAddress);
			MailMessage message = new MailMessage (from, to); 
			message.Subject = SubjectMail;
			message.Body = TextMail;

			if (AttachmentPath.Length > 0)
				message.Attachments.Add (new Attachment(AttachmentPath));

			SmtpClient smtp = new SmtpClient ("smtp.gmail.com", 587);
			smtp.Credentials = new NetworkCredential ("jarvis.smtp@gmail.com", "9874563217412369741853741741953");
			smtp.EnableSsl = true;

			try
			{
				smtp.Send (message);
			}	
			catch(Exception ex) 
			{
				Console.WriteLine (ex);
			}
		}
	}

	class Interpreter
	{
		private static string YandeksTranslateApi = "trnsl.1.1.20180412T210918Z.108398f712de403d.4ec44652eeddac18d8e8a9a60d8674bb11196c5b";

		public static string Translate(string text, string FromLang, string ToLang)
		{
			string request = "https://translate.yandex.net/api/v1.5/tr.json/translate?key=" + YandeksTranslateApi + 
				"&text=" + text + "&lang=" + FromLang + "-" + ToLang;

			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create (request);
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse ();
				string response;

				using (StreamReader streamReader = new StreamReader (httpWebResponse.GetResponseStream ())) 
					response = streamReader.ReadToEnd ();
				
				return response;
			}

			catch (Exception ex) {
				Console.WriteLine (ex);
			}

			return "";
		}
	}

	class Weather
	{
		public static string getWeatherFromWebRequest(string city)
		{
			string url = "http://api.openweathermap.org/data/2.5/weather?q=" + city + "&appid=fea10ca66ebba73a9beaa4a1f2af96cf";
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create (url);
			HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse ();

			string allinfo;
			using (StreamReader streamReader = new StreamReader (httpWebResponse.GetResponseStream ())) {
				allinfo = streamReader.ReadToEnd ();
			}
			return allinfo; 	//!!!!!!!!!!!!!!!!!!!!!!!!!!
		}
	}

	class Location
	{	
		private static bool wasGettedCountryCode;

		public static string getCountryCode()
		{
			string inf = getIpAddress ();
			if (wasGettedCountryCode) {
				return inf;
			} else {
				return "";
			}
		}

		public static string getIpAddress()
		{
			string allInfo = new WebClient ().DownloadString ("http://api.hostip.info/get_json.php");

			if (JSON.getValByKey (allInfo, "country_code") == "XX") 
			{
				return JSON.getValByKey (allInfo, "ip");
			}

			wasGettedCountryCode = true;
			return JSON.getValByKey (allInfo, "country_code");
		}
	}

	class Money
	{

	}

	class Time
	{
		public static string getTime()
		{
			string result = "";
			DateTime localDate = DateTime.Now;
			String[] cultureNames = { "en-US", "en-GB", "fr-FR", "de-DE", "ru-RU" };

			foreach (var cultureName in cultureNames) {
				var culture = new CultureInfo(cultureName);
				result += cultureName + ": " + localDate.ToString (culture) + "\n";
			}

			return result;
		}
	} 

	class MainClass
	{
		public static void Main (string[] args)
		{
			string text = Console.ReadLine ();
			int step = Convert.ToInt32(Console.ReadLine ());
			Console.WriteLine (Codings.XOR(text, step));
		}
	}
}