namespace Lib.Models
{
    public interface IDtoGenerator<TDto>
    {
        TDto GenerateDto();
    }
}