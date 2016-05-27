using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoMemories.Code.ImageHoster.Picasa.DataModel
{
    public class PicasaProject
    {
        public PicasaProject()
        {

        }

        public PicasaProject(List<string> userNames)
        {
            UserCollection = userNames.Select(username => new PicasaUser(username)).ToList();
        }

        public List<PicasaUser> UserCollection { get; set; }
    }
}
