namespace UniVal.DTOs
{
    public class MatriculaDTO
    {
        public int ID { get; set; }
        public string Status { get; set; }
        public MateriaDTO Materia { get; set; }
        public AlunoDTO Aluno { get; set; }
    }
}
