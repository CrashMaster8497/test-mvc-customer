using CustomerDataLayer.BusinessEntities;
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
    }
}
