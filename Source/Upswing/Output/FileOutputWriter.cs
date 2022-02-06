using System;
using System.IO;

namespace Upswing
{
    public class FileOutputWriter : IOutputWriter<ITemplateModel>
    {
        private readonly string basePath;
        private readonly IFileNameFormat<ITemplateModel> fileNameFormat;

        public FileOutputWriter(string basePath, IFileNameFormat<ITemplateModel> fileNameFormat)
        {
            this.basePath = basePath;
            this.fileNameFormat = fileNameFormat;
        }

        public void Write(ITemplateModel model, string output)
        {
            var fileName = fileNameFormat.GetName(model);
            var fullPath = Path.Combine(basePath, fileName);

            Console.WriteLine($"Writing file {fullPath}");
            File.WriteAllText(fullPath, output);
        }
    }
}
