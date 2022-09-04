using CustomerDataLayer.BusinessEntities;
using CustomerDataLayer.Integration.Tests.Repositories.Fixtures;
using CustomerDataLayer.Repositories;
using FluentAssertions;
using System.Data;

namespace CustomerDataLayer.Integration.Tests.Repositories
{
    public class BaseRepositoryTest
    {
        [Fact]
        public void ShouldBeAbleToGetConnection()
        {
            var connection = BaseRepository.GetConnection();

            connection.Should().NotBeNull();
            connection.State.Should().Be(ConnectionState.Open);
        }

        [Theory]
        [MemberData(nameof(GenerateRepositories))]
        public void ShouldBeAbleToCreateEntityRepository(BaseRepository repository)
        {
            repository.Should().NotBeNull();
        }

        [Theory]
        [MemberData(nameof(GenerateDefaultEntities))]
        public void ShouldBeAbleToCreate(Entity entity)
        {
            BaseRepositoryFixture.DeleteAll(entity);

            int? id = BaseRepositoryFixture.Create(entity);

            id.Should().BeGreaterThan(0);
        }

        [Theory]
        [MemberData(nameof(GenerateDefaultEntities))]
        public void ShouldBeAbleToRead(Entity entity)
        {
            BaseRepositoryFixture.DeleteAll(entity);

            entity.Id = BaseRepositoryFixture.Create(entity)!.Value;

            var readEntity = BaseRepositoryFixture.Read(entity);

            readEntity.Should().BeEquivalentTo(entity);
        }

        [Theory]
        [MemberData(nameof(GenerateDefaultEntities))]
        public void ShouldBeAbleToUpdate(Entity entity)
        {
            BaseRepositoryFixture.DeleteAll(entity);

            entity.Id = BaseRepositoryFixture.Create(entity)!.Value;

            var modifiedEntity = BaseRepositoryFixture.Read(entity);
            BaseRepositoryFixture.ModifyEntity(entity);

            bool isUpdated = BaseRepositoryFixture.Update(modifiedEntity!);
            var readEntity = BaseRepositoryFixture.Read(entity);

            isUpdated.Should().BeTrue();
            readEntity.Should().BeEquivalentTo(modifiedEntity);
        }

        [Theory]
        [MemberData(nameof(GenerateDefaultEntities))]
        public void ShouldBeAbleToDelete(Entity entity)
        {
            BaseRepositoryFixture.DeleteAll(entity);

            entity.Id = BaseRepositoryFixture.Create(entity)!.Value;

            bool isDeleted = BaseRepositoryFixture.Delete(entity);
            var readEntity = BaseRepositoryFixture.Read(entity);

            isDeleted.Should().BeTrue();
            readEntity.Should().BeNull();
        }

        [Theory]
        [MemberData(nameof(GenerateMultipleEntities))]
        public void ShouldBeAbleToReadAll(List<Entity> entities)
        {
            BaseRepositoryFixture.DeleteAll(entities[0]);

            foreach (var entity in entities)
            {
                entity.Id = BaseRepositoryFixture.Create(entity)!.Value;
            }

            var readEntities = BaseRepositoryFixture.ReadAll(entities[0]);

            readEntities.Should().BeEquivalentTo(entities);
        }

        [Theory]
        [MemberData(nameof(GenerateMultipleEntities))]
        public void ShouldBeAbleToDeleteAll(List<Entity> entities)
        {
            BaseRepositoryFixture.DeleteAll(entities[0]);

            foreach (var entity in entities)
            {
                BaseRepositoryFixture.Create(entity);
            }

            int affectedRows = BaseRepositoryFixture.DeleteAll(entities[0]);
            var readEntities = BaseRepositoryFixture.ReadAll(entities[0]);

            affectedRows.Should().Be(entities.Count);
            readEntities.Should().BeEmpty();
        }

        [Theory]
        [MemberData(nameof(GenerateMultipleEntities))]
        public void ShouldBeAbleToCount(List<Entity> entities)
        {
            BaseRepositoryFixture.DeleteAll(entities[0]);

            foreach (var entity in entities)
            {
                BaseRepositoryFixture.Create(entity);
            }

            int count = BaseRepositoryFixture.Count(entities[0]);

            count.Should().Be(entities.Count);
        }

        [Theory]
        [MemberData(nameof(GenerateEntitiesOffsetCount))]
        public void ShouldBeAbleToReadByOffsetCount(List<Entity> entities, int offset, int count)
        {
            BaseRepositoryFixture.DeleteAll(entities[0]);

            foreach (var entity in entities)
            {
                entity.Id = BaseRepositoryFixture.Create(entity)!.Value;
            }

            var readEntities = BaseRepositoryFixture.Read(entities[0], offset, count);

            readEntities.Should().BeEquivalentTo(entities.Skip(offset).Take(count));
        }

        private static IEnumerable<object[]> GenerateRepositories()
        {
            yield return new object[]
            {
                CustomerRepositoryFixture.GetRepository()
            };
            yield return new object[]
            {
                AddressRepositoryFixture.GetRepository()
            };
            yield return new object[]
            {
                NoteRepositoryFixture.GetRepository()
            };
        }

        private static IEnumerable<object[]> GenerateDefaultEntities()
        {
            yield return new object[]
            {
                CustomerRepositoryFixture.GetMinCustomer()
            };
            yield return new object[]
            {
                CustomerRepositoryFixture.GetMaxCustomer()
            };
            yield return new object[]
            {
                AddressRepositoryFixture.GetMinAddress()
            };
            yield return new object[]
            {
                AddressRepositoryFixture.GetMaxAddress()
            };
            yield return new object[]
            {
                NoteRepositoryFixture.GetMinNote()
            };
            yield return new object[]
            {
                NoteRepositoryFixture.GetMaxNote()
            };
        }

        private static IEnumerable<object[]> GenerateMultipleEntities()
        {
            yield return new object[]
            {
                Enumerable.Range(1, 1).Select(_ => (Entity)CustomerRepositoryFixture.GetMinCustomer()).ToList()
            };
            yield return new object[]
            {
                Enumerable.Range(1, 3).Select(_ => (Entity)CustomerRepositoryFixture.GetMinCustomer()).ToList()
            };
            yield return new object[]
            {
                Enumerable.Range(1, 1).Select(_ => (Entity)AddressRepositoryFixture.GetMinAddress()).ToList()
            };
            yield return new object[]
            {
                Enumerable.Range(1, 3).Select(_ => (Entity)AddressRepositoryFixture.GetMinAddress()).ToList()
            };
            yield return new object[]
            {
                Enumerable.Range(1, 1).Select(_ => (Entity)NoteRepositoryFixture.GetMinNote()).ToList()
            };
            yield return new object[]
            {
                Enumerable.Range(1, 3).Select(_ => (Entity)NoteRepositoryFixture.GetMinNote()).ToList()
            };
        }

        private static IEnumerable<object[]> GenerateEntitiesOffsetCount()
        {
            yield return new object[]
            {
                Enumerable.Range(1, 3).Select(_ => (Entity)CustomerRepositoryFixture.GetMinCustomer()).ToList(),
                1,
                1
            };
            yield return new object[]
            {
                Enumerable.Range(1, 3).Select(_ => (Entity)AddressRepositoryFixture.GetMinAddress()).ToList(),
                1,
                4
            };
            yield return new object[]
            {
                Enumerable.Range(1, 3).Select(_ => (Entity)NoteRepositoryFixture.GetMinNote()).ToList(),
                0,
                2
            };
            yield return new object[]
            {
                Enumerable.Range(1, 3).Select(_ => (Entity)CustomerRepositoryFixture.GetMinCustomer()).ToList(),
                1,
                0
            };
            yield return new object[]
            {
                Enumerable.Range(1, 3).Select(_ => (Entity)CustomerRepositoryFixture.GetMinCustomer()).ToList(),
                4,
                1
            };
        }
    }
}
