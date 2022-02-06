namespace Upswing
{
    public class SimpleFileNameFormat : IFileNameFormat<ITemplateModel>
    {
        private readonly string format;

        public SimpleFileNameFormat()
            :this("{0}.generated.cs")
        {
        }

        public SimpleFileNameFormat(string format)
        {
            this.format = format;
        }

        public string GetName(ITemplateModel model)
        {
            return string.Format(format, model.Name);
        }
    }
}
