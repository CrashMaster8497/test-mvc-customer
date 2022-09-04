using CustomerDataLayer.BusinessEntities;
using CustomerDataLayer.Repositories;

namespace CustomerDataLayer.Integration.Tests.Repositories.Fixtures
{
    public class NoteRepositoryFixture
    {
        public static NoteRepository GetRepository()
        {
            return new NoteRepository();
        }

        public static Note GetMinNote()
        {
            return new Note
            {
                Text = "Text"
            };
        }

        public static Note GetMaxNote()
        {
            return new Note
            {
                Text = "Text"
            };
        }

        public static void ModifyNote(Note note)
        {
            note.Text = "New Text";
        }

        public static int? Create(Note note, Customer customer)
        {
            note.CustomerId = customer.Id;

            var repository = new NoteRepository();

            int? id = repository.Create(note);
            note.Id = id ?? note.Id;

            return id;
        }

        public static int? Create(Note note)
        {
            var customer = CustomerRepositoryFixture.GetMinCustomer();
            CustomerRepositoryFixture.Create(customer);

            return Create(note, customer);
        }

        public static List<Note> ReadByCustomerId(int customerId)
        {
            var repository = new NoteRepository();
            return repository.ReadByCustomerId(customerId);
        }

        public static int DeleteByCustomerId(int customerId)
        {
            var repository = new NoteRepository();
            return repository.DeleteByCustomerId(customerId);
        }
    }
}
