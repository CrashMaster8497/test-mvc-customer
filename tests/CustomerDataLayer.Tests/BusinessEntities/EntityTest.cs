using CustomerDataLayer.BusinessEntities;
using FluentAssertions;

namespace CustomerDataLayer.Tests.BusinessEntities
{
    public class EntityTest
    {
        [Theory]
        [MemberData(nameof(GenerateEntities))]
        public void ShouldBeAbleToCreateEntity(Entity entity)
        {
            entity.Should().NotBeNull();
            entity.Id.Should().Be(0);
        }

        private static IEnumerable<object[]> GenerateEntities()
        {
            yield return new object[] { new Customer() };
            yield return new object[] { new Address() };
            yield return new object[] { new Note() };
        }
    }
}
