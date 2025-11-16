using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Supershop.Helpers
{
    public class ImageHelper : IImageHelper
    {
        public async Task<string> UploadImageAsync(IFormFile imageFile, string folder)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return string.Empty;
            }

            // Criar a pasta se não existir
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image", folder);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Gerar nome único para o ficheiro
            string guid = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(imageFile.FileName);
            if (string.IsNullOrEmpty(extension))
            {
                extension = ".jpg";
            }
            string file = $"{guid}{extension}";

            // Caminho completo do ficheiro
            string path = Path.Combine(folderPath, file);

            // Guardar o ficheiro
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // Retornar o caminho relativo
            return $"~/image/{folder}/{file}";
        }
    }
}
