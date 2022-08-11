namespace App.Models {
    public class Exame {

        public int Id { get; set; }
        public DateTime DataAgendamento { get; set; }
        public string CodigoTuss { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
    }
}
