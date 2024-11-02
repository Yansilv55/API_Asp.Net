using API_Coffee.Domain;
using Flunt.Notifications;
using Flunt.Validations;
using System.Runtime.CompilerServices;

namespace Coffee_Break.Domain.Produto
{
    public class Category : Entity
    {
        public string name { get; private set; }
        public bool active { get; private set; }

        public Category(string name, string createdBy, string editedBy)
        {
            name = name;
            active = true;
            CreatedBy = createdBy;
            EditedBy = editedBy;
            CreateOn = DateTime.Now;
            EditedOn = DateTime.Now;

            Validate();
        }

        private void Validate()
        {
            var contract = new Contract<Category>()
                .IsNotNullOrEmpty(name, "Name")
                .IsGreaterOrEqualsThan(name, 3, "Name")
                .IsNotNullOrEmpty(CreatedBy, "CreatedBy")
                .IsNotNullOrEmpty(EditedBy, "EditedBy");
            AddNotifications(contract);
        }

        public void EditInfo(string name, bool active, string editedBy)
        {
            active = active;
            name = name;
            EditedBy = editedBy;
            EditedOn = DateTime.Now;

            Validate();
        }


    }
}
