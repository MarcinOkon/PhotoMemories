using System;

namespace GooglePhotosUploader.Code.ImageHoster.Picasa
{
    public class PicasaDateTime
    {
        public static DateTime GetDateTime(string timestamp)
        {
            if (timestamp != null)
            {
                var unixTimeStamp = Convert.ToInt64(timestamp);
                DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                return dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            }
            return DateTime.MinValue;
        }
    }
}
