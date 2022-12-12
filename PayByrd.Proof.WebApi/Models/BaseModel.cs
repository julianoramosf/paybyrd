using System.ComponentModel.DataAnnotations;

namespace PayByrd.Proof.WebApi;

public class BaseModel
{
    [Required]
    public string Data { get; set; }
}