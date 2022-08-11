using System.ComponentModel.DataAnnotations;

namespace App.DTOs {
    public class ClienteDTO {
        [Required]
        public string Nome { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }

    }
}
