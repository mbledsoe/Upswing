using Scriban;

namespace Upswing.Scriban
{
    public interface ITemplateSource
    {
        Template ReadTemplate();
    }
}