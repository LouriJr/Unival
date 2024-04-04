namespace UniVal.DTOs
{
    public class AlunoDTO
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public DateTime DataNascimento { get; set; }
        public List<MatriculaDTO>? Matriculas { get; set; }
    }
}
