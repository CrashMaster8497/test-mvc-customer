using CustomerDataLayer.BusinessEntities;
using CustomerDataLayer.Repositories;

namespace CustomerDataLayer.Integration.Tests.Repositories
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

        public static int? Create(Note note)
        {
            var customer = CustomerRepositoryFixture.GetMinCustomer();
            customer.Id = CustomerRepositoryFixture.Create(customer)!.Value;
            note.CustomerId = customer.Id;


            var repository = new NoteRepository();
            return repository.Create(note);
        }

        public static List<int> Create(List<Note> notes)
        {
            var customer = CustomerRepositoryFixture.GetMinCustomer();
            customer.Id = CustomerRepositoryFixture.Create(customer)!.Value;
            foreach (var note in notes)
            {
                note.CustomerId = customer.Id;
            }

            var ids = new List<int>();
            ids.AddRange(notes.Select(note => Create(note)!.Value));
            return ids;
        }
    }
}
