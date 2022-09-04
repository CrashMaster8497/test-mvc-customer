using CustomerDataLayer.BusinessEntities;
using FluentAssertions;

namespace CustomerDataLayer.Tests.BusinessEntities
{
    public class NoteTest
    {
        [Fact]
        public void ShouldBeAbleToCreateNote()
        {
            var note = new Note
            {
                Id = 0,
                CustomerId = 0,
                Text = string.Empty
            };

            note.Should().NotBeNull();

            note.Id.Should().BeOfType(typeof(int));
            note.CustomerId.Should().BeOfType(typeof(int));
            note.Text.Should().BeOfType<string>();
        }

        [Fact]
        public void ShouldHaveDefaultValues()
        {
            var note = new Note();

            note.Id.Should().Be(0);
            note.CustomerId.Should().Be(0);
            note.Text.Should().Be(string.Empty);
        }

        [Fact]
        public void ShouldBeAbleToGetSetNoteId()
        {
            var note = new Note
            {
                Id = 1
            };

            note.Id.Should().Be(1);
        }

        [Fact]
        public void ShouldBeAbleToGetSetCustomerId()
        {
            var note = new Note
            {
                CustomerId = 1
            };

            note.CustomerId.Should().Be(1);
        }

        [Fact]
        public void ShouldBeAbleToGetSetText()
        {
            var note = new Note
            {
                Text = "Text"
            };

            note.Text.Should().Be("Text");
        }
    }
}
