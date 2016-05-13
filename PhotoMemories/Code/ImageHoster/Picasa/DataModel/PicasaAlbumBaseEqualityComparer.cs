using System.Collections.Generic;

namespace GooglePhotosUploader.Code.ImageHoster.Picasa.DataModel
{
    class PicasaAlbumBaseEqualityComparer : IEqualityComparer<PicasaAlbumBase>
    {
        public bool Equals(PicasaAlbumBase x, PicasaAlbumBase y)
        {
            return x.Id == y.Id && x.Updated == y.Updated;
        }

        public int GetHashCode(PicasaAlbumBase obj)
        {
            return obj.Id.GetHashCode() ^ obj.Updated.GetHashCode();
        }
    }
}
