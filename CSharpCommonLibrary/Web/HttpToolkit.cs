using CommonLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace CommonLibrary.Web
{
    public class HttpToolkit
    {
        public static bool IsRevalidateCache = false;

        public static byte[] CreatePostData(Dictionary<string, HttpParameter> parameter, Encoding encoding)
        {
            StringBuilder builder = new StringBuilder();

            int itemCount = parameter.Count;
            bool isLast = false;

            foreach (var item in parameter)
            {
                itemCount--;
                isLast = itemCount == 0;
                
                if (item.Value.Values.Count > 1)
                {
                    int innerItemCount = item.Value.Values.Count;
                    bool isInnerLast = false;

                    foreach (string value in item.Value.Values)
                    {
                        innerItemCount--;
                        isInnerLast = innerItemCount == 0;

                        builder.Append(
                        String.Format("{0}={1}",
                        item.Key,
                        HttpUtility.UrlEncode(value, encoding)));

                        if (!isInnerLast)
                        {
                            builder.Append("&");
                        }
                    }
                }
                else
                {
                    builder.Append(
                    String.Format("{0}={1}",
                    item.Key,
                    HttpUtility.UrlEncode(item.Value.Values[0], encoding)));
                }
                

                if (!isLast)
                {
                    builder.Append("&");
                }
            }

            string postData = builder.ToString();
            byte[] buffer = encoding.GetBytes(postData);
            return buffer;
        }

        public static string GetResponseByPost(string uriString, Dictionary<string, HttpParameter> parameter, string requestEnc, string responseEnc, int requestTimeout, out HttpStatusCode outHttpStatusCode)
        {
            if (uriString.Substring(0, 5).ToLower().Equals("https"))
            {
                ServicePointManager.ServerCertificateValidationCallback += delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true; 
                };
            }

            Uri address = new Uri(uriString);

            //
            // Create the web request   
            //
            HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;

            request.UserAgent = "Mozilla/4.0";

            if (IsRevalidateCache)
            {
                HttpRequestCachePolicy cachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Revalidate);
                request.CachePolicy = cachePolicy;
            }

            //
            //TimeOut 설정
            //
            request.Timeout = requestTimeout;

            ///
            /// Expect100Continue 무시
            ///
            request.ServicePoint.Expect100Continue = false;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            if (parameter != null && parameter.Count > 0)
            {
                byte[] data = CreatePostData(parameter, Encoding.GetEncoding(requestEnc));

                ////
                //// Set the content length in the request headers   
                ////
                request.ContentLength = data.Length;

                ////
                //// Write data   
                ////
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            else
            {
                request.ContentLength = 0;
            }

            string responseData = "";

            HttpStatusCode httpStatusCode = HttpStatusCode.OK;

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    httpStatusCode = response.StatusCode;
                    Toolkit.DebugWriteLine("Response.IsFromCache=" + response.IsFromCache);
                    
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(responseEnc));
                    responseData = reader.ReadToEnd();

                    Toolkit.DebugWriteLine("ResponseData=" + responseData);
                }
            }
            catch (WebException ex)
            {
                if (ex.Response is HttpWebResponse)
                {
                    httpStatusCode = ((HttpWebResponse)ex.Response).StatusCode;
                }
                else
                {
                    httpStatusCode = HttpStatusCode.BadRequest;
                }

                if (ex.InnerException != null)
                {
                    responseData = ex.InnerException.Message;
                }
                else
                {
                    responseData = ex.Message;
                }
            }
            catch (Exception ex)
            {
                httpStatusCode = HttpStatusCode.InternalServerError;

                if (ex.InnerException != null)
                {
                    responseData = ex.InnerException.Message;
                }
                else
                {
                    responseData = ex.Message;
                }
            }

            outHttpStatusCode = httpStatusCode;
            return responseData;
        }
    }
}
