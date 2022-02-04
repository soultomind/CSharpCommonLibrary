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
        public static string ContentType = "application/x-www-form-urlencoded; charset=";
        public string UserAgent { get; set; }
        public bool IsSetCachePolicy { get; set; }
        public HttpRequestCacheLevel CacheLevel { get; set; }

        public bool IsExpect100Continue { get; set; }

        public HttpToolkit()
        {
            UserAgent = "Mozilla/4.0";
            IsSetCachePolicy = false;
            CacheLevel = HttpRequestCacheLevel.Revalidate;

            IsExpect100Continue = false;
        }

        public byte[] CreatePostData(Dictionary<string, string[]> parameter, Encoding encoding)
        {
            StringBuilder builder = new StringBuilder();

            int itemCount = parameter.Count;
            bool isItemLast = false;

            foreach (var item in parameter)
            {
                itemCount--;
                isItemLast = itemCount == 0;
                
                if (item.Value.Length > 1)
                {
                    string[] values = item.Value;
                    int inneValueCount = values.Length;
                    bool isInnerValueLast = false;

                    foreach (string value in values)
                    {
                        inneValueCount--;
                        isInnerValueLast = inneValueCount == 0;

                        builder.Append(
                            String.Format("{0}={1}",
                            item.Key,
                        HttpUtility.UrlEncode(value, encoding)));

                        if (!isInnerValueLast)
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
                    HttpUtility.UrlEncode(item.Value[0], encoding)));
                }
                

                if (!isItemLast)
                {
                    builder.Append("&");
                }
            }

            string postData = builder.ToString();
            byte[] buffer = encoding.GetBytes(postData);
            return buffer;
        }

        public string GetResponseByPost(string uriString, Dictionary<string, string[]> parameter, string requestEnc, string responseEnc, int requestTimeout, out HttpStatusCode outHttpStatusCode, out Exception outException)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.OK;

            if (uriString.Substring(0, 5).ToLower().Equals("https"))
            {
                ServicePointManager.ServerCertificateValidationCallback += delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
            }

            string responseData = "";
            try
            {
                
                Uri requestUri = new Uri(uriString);

                //
                // Create the web request   
                //
                HttpWebRequest request = WebRequest.Create(requestUri) as HttpWebRequest;

                request.UserAgent = UserAgent;

                if (IsSetCachePolicy)
                {
                    HttpRequestCachePolicy httpRequestCachePolicy = new HttpRequestCachePolicy(CacheLevel);
                    request.CachePolicy = httpRequestCachePolicy;
                }

                //
                //TimeOut 설정
                //
                if (requestTimeout > 0)
                {
                    request.Timeout = requestTimeout;
                }

                ///
                /// Expect100Continue 무시
                ///
                request.ServicePoint.Expect100Continue = IsExpect100Continue;

                if (parameter != null && parameter.Count > 0)
                {
                    request.Method = "POST";
                    request.ContentType = ContentType + requestEnc;

                    byte[] data = CreatePostData(parameter, Encoding.GetEncoding(requestEnc));

                    ////
                    //// Set the content length in the request headers   
                    ////
                    request.ContentLength = data.Length;

                    ////
                    //// Write data   
                    ////
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(data, 0, data.Length);
                    }
                }
                else
                {
                    request.ContentLength = 0;
                }

                

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    httpStatusCode = response.StatusCode;
                    Toolkit.DebugWriteLine("Response.IsFromCache=" + response.IsFromCache);
                    
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(responseEnc));
                    responseData = reader.ReadToEnd();

                    Toolkit.DebugWriteLine("ResponseData=" + responseData);

                    reader.Close();
                }

                outException = null;
            }
            catch (WebException ex)
            {
                if (ex.Response is HttpWebResponse)
                {
                    httpStatusCode = ((HttpWebResponse)ex.Response).StatusCode;
                }
                else
                {
                    httpStatusCode = HttpStatusCode.InternalServerError;
                }

                outException = ex;
            }
            catch (Exception ex)
            {
                outException = ex;
                httpStatusCode = HttpStatusCode.BadRequest;
            }

            outHttpStatusCode = httpStatusCode;
            return responseData;
        }
    }
}
