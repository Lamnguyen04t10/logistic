namespace System.Domain.Entities.AddresAgr
{
    public partial class Address
    {
        public Address() { }

        public Address(
            string street,
            string city,
            string state,
            string country,
            string zipcode,
            int position,
            bool isDefault,
            Guid userId
        )
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipcode;
            UserId = userId;
            Position = position;
            IsDefault = isDefault;
        }

        public void AddTenant(Guid tenantId)
        {
            TenantId = tenantId;
        }

        public void AddUser(Guid userId)
        {
            UserId = userId;
        }
    }
}
