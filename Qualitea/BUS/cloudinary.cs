using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DTO;
namespace BUS
{
    public class cloudinary
    {
        private static Cloudinary clou;
        public cloudinary()
        {
            string cloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME");
            string apiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY");
            string apiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET");

            clou = new Cloudinary(new Account(cloudName, apiKey, apiSecret));
        }

        public string addImageForce(string imageSource)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imageSource),
                Transformation = new Transformation().Width(300).Height(300).Crop("fit"),
            };

            var uploadResult = clou.Upload(uploadParams);
            string url = uploadResult.SecureUrl.ToString();
            return url;
        }

        public string addImage(string imageSource)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imageSource),
                Transformation = new Transformation().Width(300).Height(300).Crop("fit"),
                Moderation = "manual"
            };
            var uploadResult = clou.Upload(uploadParams);
            string url = uploadResult.SecureUrl.ToString();
            return url;
        }

        public void delImage(string publicID)
        {
            var delParams = new DeletionParams(publicID);
            var delResult = clou.Destroy(delParams);
        }
    }
}
