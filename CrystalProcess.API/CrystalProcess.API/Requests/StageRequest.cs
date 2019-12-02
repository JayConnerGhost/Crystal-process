using System.ComponentModel.DataAnnotations;

namespace CrystalProcess.API.Requests
{
    public class NewStageRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public int Order { get; set; }
    }
}