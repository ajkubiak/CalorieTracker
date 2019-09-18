using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lib.Models.Auth;

namespace Lib.Models
{
    public abstract class BaseOwnedByModel : BaseModel
    {
        [Required]
        public string OwnedById { get; set; }

        [Required]
        [ForeignKey("OwnedById")]
        virtual public User OwnedBy { get; set; }
    }
}
