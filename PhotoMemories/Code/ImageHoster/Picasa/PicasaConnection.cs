using Google.Apis.Auth.OAuth2;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml.Linq;
using System.Linq;

namespace GooglePhotosUploader.Code.ImageHoster.Picasa
{
    class PicasaConnection
    {
        public static string GetWebResponse(string url, string username)
        {
            var credentials = PicasaSecurity.GetCredentials(username);
            var webClient = GetWebclient(credentials);

            try
            {
                return webClient.DownloadString(url);
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        private static WebClient GetWebclient(UserCredential credentials)
        {
            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            webClient.Headers.Add("GData-Version", "2");
            webClient.Headers.Add("Authorization", string.Format("Bearer {0}", credentials.Token.AccessToken));
            return webClient;
        }
    }
}
