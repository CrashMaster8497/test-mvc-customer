using CustomerDataLayer.BusinessEntities;
using CustomerDataLayer.Integration.Tests.Repositories.Fixtures;
using CustomerDataLayer.Repositories;
using FluentAssertions;

namespace CustomerDataLayer.Integration.Tests.Repositories
{
    public class NoteRepositoryTest
    {
        [Fact]
        public void ShouldBeAbleToCreateNoteRepository()
        {
            var repository = new NoteRepository();

            repository.Should().NotBeNull();
            repository.TableName.Should().Be("Note");
            repository.UsedByTables.Should().BeEmpty();
            repository.KeyColumn.Should().Be("NoteId");
            repository.NonKeyColumns.Should().Contain(new[] { "CustomerId", "Text" });
        }

        [Fact]
        public void ShouldBeAbleToGetKeyParameter()
        {
            var repository = new NoteRepository();
            var entity = new Note();

            var keyParameter = repository.GetKeyParameter(entity);

            keyParameter.Should().NotBeNull();

            keyParameter = repository.GetKeyParameter(1);

            keyParameter.Should().NotBeNull();
        }

        [Fact]
        public void ShouldBeAbleToGetNonKeyParameters()
        {
            var repository = new NoteRepository();
            var entity = new Note();

            var nonKeyParameters = repository.GetNonKeyParameters(entity);

            nonKeyParameters.Should().NotBeNull();
            nonKeyParameters.Length.Should().Be(2);
        }

        [Theory]
        [MemberData(nameof(GenerateNoteGroups))]
        public void ShouldBeAbleToReadByCustomerId(List<List<Note>> notesList)
        {
            var customers = new List<Customer>();
            foreach (var notes in notesList)
            {
                var customer = CustomerRepositoryFixture.GetMinCustomer();
                CustomerRepositoryFixture.Create(customer);
                customers.Add(customer);

                foreach (var note in notes)
                {
                    NoteRepositoryFixture.Create(note, customer);
                }
            }

            for (int i = 0; i < notesList.Count; i++)
            {
                var readNotes = NoteRepositoryFixture.ReadByCustomerId(customers[i].Id);

                readNotes.Should().BeEquivalentTo(notesList[i]);
            }
        }

        [Theory]
        [MemberData(nameof(GenerateNoteGroups))]
        public void ShouldBeAbleToDeleteByCustomerId(List<List<Note>> notesList)
        {
            var customers = new List<Customer>();
            foreach (var notes in notesList)
            {
                var customer = CustomerRepositoryFixture.GetMinCustomer();
                CustomerRepositoryFixture.Create(customer);
                customers.Add(customer);

                foreach (var note in notes)
                {
                    NoteRepositoryFixture.Create(note, customer);
                }
            }

            for (int i = 0; i < notesList.Count; i++)
            {
                int affectedRows = NoteRepositoryFixture.DeleteByCustomerId(customers[i].Id);
                var deletedNotes = NoteRepositoryFixture.ReadByCustomerId(customers[i].Id);

                affectedRows.Should().Be(notesList[i].Count);
                deletedNotes.Should().BeEmpty();

                for (int j = i + 1; j < notesList.Count; j++)
                {
                    var notDeletedNotes = NoteRepositoryFixture.ReadByCustomerId(customers[j].Id);

                    notDeletedNotes.Should().BeEquivalentTo(notesList[j]);
                }
            }
        }

        private static IEnumerable<object[]> GenerateNoteGroups()
        {
            yield return new object[] { new List<List<Note>>
            {
                Enumerable.Range(0, 1).Select(_ => NoteRepositoryFixture.GetMinNote()).ToList(),
                Enumerable.Range(0, 0).Select(_ => NoteRepositoryFixture.GetMinNote()).ToList(),
                Enumerable.Range(0, 3).Select(_ => NoteRepositoryFixture.GetMinNote()).ToList()
            } };
        }
    }
}
