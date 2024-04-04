namespace UniVal.DTOs
{
    public class MateriaDTO
    {
        public int ID { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set;}
        public ProfessorDTO? Professor { get; set; }
        public List<MateriaDTO>? Dependencias { get; set; }
        public List<MatriculaDTO>? Matriculas { get; set; }
    }
}
