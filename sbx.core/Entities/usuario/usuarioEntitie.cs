
namespace sbx.core.Entities.usuario
{
    public class usuarioEntitie
    {
        public int IdUser { get; set; }
        public int IdIdentificationType { get; set; }
        public string IdentificationNumber { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int IdCountry { get; set; }
        public int IdDepartament { get; set; }
        public int IdCity { get; set; }
        public string TelephoneNumber { get; set; }
        public string Email { get; set; }
        public int IdRole { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime DatePassword { get; set; }
        public int Active { get; set; }
    }
}
