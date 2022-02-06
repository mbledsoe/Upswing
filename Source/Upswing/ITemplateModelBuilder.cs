namespace Upswing
{
    public interface ITemplateModelBuilder<TModel>
    {
        TModel Build(TableDefinition tableDefinition);
    }
}