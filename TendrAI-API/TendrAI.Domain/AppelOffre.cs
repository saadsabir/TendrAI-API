namespace TendrAI.Domain;

public class AppelOffre
{
    public Guid Id { get; set; }
    public string Titre { get; set; }
    public string Description { get; set; }
    public DateTime DateLimite { get; set; }
}
