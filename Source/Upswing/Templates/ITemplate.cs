namespace Upswing
{
    public interface ITemplate<T>
    {
        string Render(T templateFileModel);
    }
}