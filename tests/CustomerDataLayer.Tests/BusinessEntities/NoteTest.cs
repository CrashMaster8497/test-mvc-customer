using CustomerDataLayer.BusinessEntities;
using FluentAssertions;

namespace CustomerDataLayer.Tests.BusinessEntities
{
    public class NoteTest
    {
        [Fact]
        public void ShouldBeAbleToCreateNote()
        {
            var note = new Note();

            note.Should().NotBeNull();

            note.NoteId.Should().BeOfType(typeof(int));
            note.CustomerId.Should().BeOfType(typeof(int));
            note.Text.Should().BeOfType<string>();
        }

        [Fact]
        public void ShouldHaveDefaultValues()
        {
            var note = new Note();

            note.NoteId.Should().Be(0);
            note.CustomerId.Should().Be(0);
            note.Text.Should().Be(string.Empty);
        }

        [Fact]
        public void ShouldBeAbleToGetSetNoteId()
        {
            var note = new Note
            {
                NoteId = 1
            };

            note.NoteId.Should().Be(1);
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
