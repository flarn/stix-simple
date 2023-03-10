using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Stix.Api.Models
{
    public class IdRequest
    {

        //Name must be lower for swagger ui to be able to pass route parameter, maybe can be fixed?
        [FromRoute]
        [Required]
#pragma warning disable IDE1006 // Naming Styles
        public string id { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
