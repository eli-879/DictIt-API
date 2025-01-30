using DictItApi.Entities;

namespace DictItApi.Dtos;

public class DictionaryAPIResponseDto
{
    public DictionaryEntry[] Entries { get; set; } = [];
}
