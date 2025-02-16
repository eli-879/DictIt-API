namespace DictItApi.Dtos;

public class SavedWordResponseDto
{
    public List<SavedWordDto> SavedWords { get; set; } = [];

    public int ResultsLength { get; set; }
}
