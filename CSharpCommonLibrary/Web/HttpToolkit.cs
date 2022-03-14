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
    public class CreateHttpWebRequestEventArgs : EventArgs
    {
        public HttpWebRequest Request { get; set; }
        public byte[] Data { get; set; }

        public Dictionary<string, string> ParamValue { get; set; }
        public Dictionary<string, string[]> ParamValueArray { get; set; }

        public CreateHttpWebRequestEventArgs(HttpWebRequest request, byte[] data)
        {
            Request = request;
            Data = data;
        }

        public CreateHttpWebRequestEventArgs(HttpWebRequest request, byte[] data, Dictionary<string, string> paramValue)
        {
            Request = request;
            Data = data;
            ParamValue = paramValue;
        }

        public CreateHttpWebRequestEventArgs(HttpWebRequest request, byte[] data, Dictionary<string, string[]> paramValueArray)
        {
            Request = request;
            Data = data;
            ParamValueArray = paramValueArray;
        }
    }

    public class CreateHttpWebResponseEventArgs : EventArgs
    {
        public HttpWebResponse Response { get; set; }
        public string Data { get; set; }
        public CreateHttpWebResponseEventArgs(HttpWebResponse response, string data)
        {
            Response = response;
            Data = data;
        }
    }

    public delegate void CreateHttpWebRequestEventHandler(object sender, CreateHttpWebRequestEventArgs e);
    public delegate void CreateHttpWebResponseEventHandler(object sender, CreateHttpWebResponseEventArgs e);

    /// <summary>
    /// Http 통신 관련 툴킷 클래스
    /// </summary>
    public class HttpToolkit
    {
        /// <summary>
        /// HttpWebRequest 생성후 파라미터 데이터를 생성후 호출되는 이벤트핸들러입니다.
        /// </summary>
        public event CreateHttpWebRequestEventHandler CreateHttpWebRequest;
        
        /// <summary>
        /// HttpWebResponse 생성후 응답데이터를 생성후 호출되는 이벤트 핸들러입니다.
        /// </summary>
        public event CreateHttpWebResponseEventHandler CreateHttpWebResponse;

        public static string DefaultContentType = "application/x-www-form-urlencoded; charset=";
        public string DefaultUserAgent { get; set; }
        public bool DefaultIsSetCachePolicy { get; set; }
        public HttpRequestCacheLevel DefaultCacheLevel { get; set; }

        public bool DefaultIsExpect100Continue { get; set; }

        public HttpToolkit()
        {
            DefaultUserAgent = "Mozilla/4.0";
            DefaultIsSetCachePolicy = false;
            DefaultCacheLevel = HttpRequestCacheLevel.Revalidate;

            DefaultIsExpect100Continue = false;
        }

        public byte[] CreatePostData(Dictionary<string, string> parameter, Encoding encoding)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var key in parameter.Keys)
            {
                if (builder.Length != 0)
                {
                    builder.Append("&");
                }

                string paramValue = parameter[key];
                /*
                    string paramValue = value;
                    paramValue = Uri.EscapeDataString(value);
                    paramValue = Uri.EscapeUriString(value);
                */
                builder.Append(String.Format("{0}={1}", key, HttpUtility.UrlEncode(paramValue, encoding)));
            }

            string postData = builder.ToString();
            byte[] buffer = encoding.GetBytes(postData);
            return buffer;
        }

        private void SetRequestDefaultProperties(HttpWebRequest request, int requestTimeout)
        {
            request.UserAgent = DefaultUserAgent;

            //
            //TimeOut 설정
            //
            if (requestTimeout > 0)
            {
                request.Timeout = requestTimeout;
            }


            if (DefaultIsSetCachePolicy)
            {
                HttpRequestCachePolicy httpRequestCachePolicy = new HttpRequestCachePolicy(DefaultCacheLevel);
                request.CachePolicy = httpRequestCachePolicy;
            }

            ///
            /// Expect100Continue 무시
            ///
            request.ServicePoint.Expect100Continue = DefaultIsExpect100Continue;

            request.ContentType = DefaultContentType;
        }

        public string GetResponseByPost(string uriString, Dictionary<string, string> parameter, string requestEnc, string responseEnc, int requestTimeout, out HttpStatusCode outHttpStatusCode, out Exception outException)
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
                byte[] data = null;
                //
                // Create the web request   
                //
                HttpWebRequest request = WebRequest.Create(requestUri) as HttpWebRequest;
                if (parameter != null && parameter.Count > 0)
                {
                    SetRequestDefaultProperties(request, requestTimeout);

                    data = CreatePostData(parameter, Encoding.GetEncoding(requestEnc));
                    request.Method = "POST";
                    request.ContentLength = data.Length;

                    CreateHttpWebRequest?.Invoke(this, new CreateHttpWebRequestEventArgs(request, data, parameter));
                }
                else
                {
                    SetRequestDefaultProperties(request, requestTimeout);

                    request.Method = "GET";
                    request.ContentLength = 0;

                    CreateHttpWebRequest?.Invoke(this, new CreateHttpWebRequestEventArgs(request, null));
                }

                if (parameter != null && parameter.Count > 0)
                {
                    ////
                    //// Write data   
                    ////
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(data, 0, data.Length);
                    }
                }

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    httpStatusCode = response.StatusCode;
                    Toolkit.DebugWriteLine("Response.IsFromCache=" + response.IsFromCache);

                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(responseEnc));
                    responseData = reader.ReadToEnd();

                    CreateHttpWebResponse?.Invoke(this, new CreateHttpWebResponseEventArgs(response, responseData));

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
                    // 500 번대 Server 에러
                    httpStatusCode = HttpStatusCode.InternalServerError;
                }

                outException = ex;
            }
            catch (Exception ex)
            {
                outException = ex;
                // 400 번대 Client 에러
                httpStatusCode = HttpStatusCode.BadRequest;
            }

            outHttpStatusCode = httpStatusCode;
            return responseData;
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

                        /*
                        string paramValue = value;
                        paramValue = Uri.EscapeDataString(value);
                        paramValue = Uri.EscapeUriString(value);
                        */

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
                byte[] data = null;
                //
                // Create the web request   
                //
                HttpWebRequest request = WebRequest.Create(requestUri) as HttpWebRequest;
                if (parameter != null && parameter.Count > 0)
                {
                    SetRequestDefaultProperties(request, requestTimeout);

                    data = CreatePostData(parameter, Encoding.GetEncoding(requestEnc));
                    request.Method = "POST";
                    request.ContentLength = data.Length;

                    CreateHttpWebRequest?.Invoke(this, new CreateHttpWebRequestEventArgs(request, data, parameter));
                }
                else
                {
                    SetRequestDefaultProperties(request, requestTimeout);

                    request.Method = "GET";
                    request.ContentLength = 0;

                    CreateHttpWebRequest?.Invoke(this, new CreateHttpWebRequestEventArgs(request, null));
                }

                if (parameter != null && parameter.Count > 0)
                {
                    ////
                    //// Write data   
                    ////
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(data, 0, data.Length);
                    }
                }

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    httpStatusCode = response.StatusCode;
                    Toolkit.DebugWriteLine("Response.IsFromCache=" + response.IsFromCache);
                    
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(responseEnc));
                    responseData = reader.ReadToEnd();

                    CreateHttpWebResponse?.Invoke(this, new CreateHttpWebResponseEventArgs(response, responseData));

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
                    // 500 번대 Server 에러
                    httpStatusCode = HttpStatusCode.InternalServerError;
                }

                outException = ex;
            }
            catch (Exception ex)
            {
                outException = ex;
                // 400 번대 Client 에러
                httpStatusCode = HttpStatusCode.BadRequest;
            }

            outHttpStatusCode = httpStatusCode;
            return responseData;
        }
    }
}
