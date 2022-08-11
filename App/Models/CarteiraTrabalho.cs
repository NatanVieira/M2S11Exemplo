namespace App.Models {
    public class CarteiraTrabalho {

        public int Id { get; internal set; }
        public string PisPasep { get; set; }
        public  int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
    }
}
