using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooglePhotosUploader.Code.ImageHoster.Picasa.DataModel
{
    public class PicasaProject
    {
        public List<PicasaUser> UserCollection { get; set; }

        public PicasaProject()
        {

        }

        public PicasaProject(List<string> userNames)
        {
            UserCollection = userNames.Select(username => new PicasaUser(username)).ToList();
        }

        public void Update()
        {
            UserCollection.ForEach(user => user.Update());
        }
    }
}
