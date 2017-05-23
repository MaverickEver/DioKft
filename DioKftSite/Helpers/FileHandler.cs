using System.IO;
using System.Web;

namespace MS.WebSolutions.DioKft.Helpers
{
    public class FileHandler
    {
        private HttpServerUtilityBase server;

        public FileHandler(HttpServerUtilityBase server)
        {
            this.server = server;
        }

        public string Save(int id, HttpPostedFileBase image, FileLocations location)
        {
            var path = string.Empty;
            try
            {
                var extension = Path.GetExtension(image.FileName);
                var folderPath = this.server.MapPath($"~/Content/DynamicResources/{location.ToString()}");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                path = $"~/Content/DynamicResources/{location.ToString()}/{id}{extension}";
                var serverPath = server.MapPath(path);
                image.SaveAs(serverPath);
            }
            catch
            {
                return string.Empty;
            }

            return path;
        }

        public void Remove(string imageUrl)
        {
            if (!File.Exists(imageUrl)) { return; }

            var serverImageUrl = this.server.MapPath(imageUrl);

            File.Delete(imageUrl);
        }
    }

    public enum FileLocations
    {
        ContactImages,
        ProductDocuments,
        ImportFolder
    }
}