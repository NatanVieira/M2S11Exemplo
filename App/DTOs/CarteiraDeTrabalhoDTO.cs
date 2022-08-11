using System.ComponentModel.DataAnnotations;

namespace App.DTOs {
    public class CarteiraDeTrabalhoDTO {
        [Required]
        public string PisPasep { get; set; }
        [Required]
        public int ClienteId { get; set; }
    }
}
