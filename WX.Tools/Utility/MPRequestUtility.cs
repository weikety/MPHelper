﻿using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WX.Tools.Utility
{
	using Hanger.Common;

	public class MPRequestUtility
	{
		public static async Task<string> Post(string url, string postData, CookieContainer cookie, Encoding encoding = null)
		{
			var byteArray = Encoding.UTF8.GetBytes(postData);

			try
			{
				var request = (HttpWebRequest)WebRequest.Create(url);

				request.Accept = "application/json, text/javascript, */*; q=0.01";
				request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
				request.ContentLength = byteArray.Length;
				request.CookieContainer = cookie;
				request.Headers.Add("Accept-Encoding", "gzip,deflate,sdch");
				request.Headers.Add("Accept-Language", "zh-cn");
				request.Headers.Add("Origin", "https://mp.weixin.qq.com");
				request.Headers.Add("X-Requested-With", "XMLHttpRequest");
				request.Host = "mp.weixin.qq.com";
				request.KeepAlive = true;
				request.Method = "POST";
				request.Referer = "https://mp.weixin.qq.com/";
				request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/30.0.1599.101 Safari/537.36";
				

				using (var requestStream = request.GetRequestStream())
				{
					requestStream.Write(byteArray, 0, byteArray.Length);
				}

				using (var response = (HttpWebResponse)request.GetResponse())
				{
					using (var streamReader = new StreamReader(response.GetResponseStream(), encoding ?? Encoding.UTF8))
					{
						return await streamReader.ReadToEndAsync();
					}
				}
			}
			catch (Exception ex)
			{
				LocalLoggingService.Exception(ex);
			}

			return string.Empty;
		}

		public static async Task<string> Get(string url, CookieContainer cookie, Encoding encoding = null)
		{
			try
			{
				var request = (HttpWebRequest)WebRequest.Create(url);

				request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
				request.CookieContainer = cookie;
				request.ContentType = "application/x-www-form-urlencoded";
				request.Headers.Add("Accept-Encoding", "gzip,deflate,sdch");
				request.Host = "mp.weixin.qq.com";
				request.KeepAlive = true;
				request.Method = "GET";
				request.Referer = "https://mp.weixin.qq.com/";
				request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/30.0.1599.101 Safari/537.36";

				using (var response = (HttpWebResponse)request.GetResponse())
				{
					using (var streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
					{
						return await streamReader.ReadToEndAsync();
					}
				}

			}
			catch (Exception ex)
			{
				LocalLoggingService.Exception(ex);
			}

			return string.Empty;
		}
	}
}