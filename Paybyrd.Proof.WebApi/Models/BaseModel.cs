using System.ComponentModel.DataAnnotations;

namespace Paybyrd.Proof.WebApi;

public class BaseModel
{
    [Required]
    public string Data { get; set; }
}