using System;

namespace Secullum.Validation.Tests
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public int? Zipcode { get; set; }
        public float Height { get; set; }
        public float? Weight { get; set; }
        public string Cpf { get; set; }
        public string Cnpj { get; set; }
        public string Cep { get; set; }
        public string Pis { get; set; }
        public string Hour { get; set; }
        public DateTime Birth { get; set; }
        public DateTime? Death { get; set; }
        public Guid GlobalId { get; set; }
        public Guid? UniversalId { get; set; }
    }
}
