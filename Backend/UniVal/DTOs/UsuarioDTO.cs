namespace UniVal.DTOs
{
    public class UsuarioDTO
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string? ImagemURL { get; set; }
        public string Base64 { get; set; }
    }
}
