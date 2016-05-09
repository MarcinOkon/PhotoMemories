using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using System.Threading;

namespace GooglePhotosUploader.Code.ImageHoster.Picasa
{
    class PicasaSecurity
    {
        public static UserCredential GetCredentials(string username)
        {
            var clientSecrets = new ClientSecrets { ClientId = "832661240038-d0mjm5a4pi3r55v26sfrukuip4ogfta1.apps.googleusercontent.com", ClientSecret = "KaK5xt1MZVDmzXIfqLZDZSta" };
            string[] scopes = { "https://picasaweb.google.com/data/" };
            var dataStore = new FileDataStore("Drive.Auth.Store");
            var credentials = GoogleWebAuthorizationBroker.AuthorizeAsync(clientSecrets, scopes, username, CancellationToken.None, dataStore).Result;

            if (credentials.Token.IsExpired(credentials.Flow.Clock))
            {
                credentials.RefreshTokenAsync(CancellationToken.None);
            }

            return credentials;
        }
    }
}
