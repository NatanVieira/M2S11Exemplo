using System.ComponentModel.DataAnnotations;

namespace App.Models {
    public class Vacina {

        public int Id { get; internal set; }
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string  Nome { get; set; }
        [Range(1,100, ErrorMessage = "O número de doses precisa estar entre 1 e 100.")]
        public int NumeroDoses { get; set; }
    }
}
