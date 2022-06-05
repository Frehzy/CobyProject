namespace Shared.Factory.Dto;

internal class GuestDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public int Rank { get; set; }

    public GuestDto() { }

    public GuestDto(Guid id, string name, int rank)
    {
        Id = id;
        Name = name;
        Rank = rank;
    }
}