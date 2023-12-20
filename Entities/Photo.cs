using System.ComponentModel.DataAnnotations.Schema;

namespace APITON.Entities;
[Table("Photos")]

public class Photo
{
    public int AppUserID { get; set; }
    public AppUser? AppUser { get; set; }

    public int Id { get; set; }
    public string? Url { get; set; }
    public string? PublicId { get; set; }
    public bool IsMain { get; set; }
}
