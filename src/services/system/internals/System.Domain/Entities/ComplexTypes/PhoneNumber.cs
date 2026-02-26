using System.ComponentModel.DataAnnotations;
using Core.Entity;

namespace System.Domain.Entities.ComplexTypes
{
    public class PhoneNumber : ValueObject
    {
        [MaxLength(10)]
        public string Code { get; private set; }

        [MaxLength(20)]
        public string Phone { get; private set; }

        public PhoneNumber() { }

        public PhoneNumber(string code, string phone)
        {
            Code = code;
            Phone = phone;
        }

        public override string ToString()
        {
            return $"{Code}-{Phone}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
            yield return Phone;
        }
    }
}
