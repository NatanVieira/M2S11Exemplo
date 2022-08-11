namespace App.Models {
    public class Cliente {

        public int Id { get; internal set; }
        public string Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public CarteiraTrabalho CarteiraTrabalho { get; set; }

        public List<Exame> Exames { get; set; }

        public List<Vacina> Vacinas { get; set; }
    }
}
