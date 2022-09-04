using CustomerDataLayer.BusinessEntities;
using CustomerDataLayer.Repositories;

namespace CustomerDataLayer.Integration.Tests.Repositories
{
    public class BaseRepositoryFixture
    {
        public static BaseRepository? GetRepository(Entity entity)
        {
            return entity switch
            {
                Customer => new CustomerRepository(),
                Address => new AddressRepository(),
                Note => new NoteRepository(),
                _ => null
            };
        }

        public static void ModifyEntity(Entity entity)
        {
            switch (entity)
            {
                case Customer customer:
                    CustomerRepositoryFixture.ModifyCustomer(customer);
                    break;
                case Address address:
                    AddressRepositoryFixture.ModifyAddress(address);
                    break;
                case Note note:
                    NoteRepositoryFixture.ModifyNote(note);
                    break;
            }
        }

        public static int? Create(Entity entity)
        {
            return entity switch
            {
                Customer customer => CustomerRepositoryFixture.Create(customer),
                Address address => AddressRepositoryFixture.Create(address),
                Note note => NoteRepositoryFixture.Create(note),
                _ => null
            };
        }

        public static Entity? Read(Entity entity)
        {
            var repository = GetRepository(entity)!;
            return repository.Read(entity.Id);
        }

        public static bool Update(Entity entity)
        {
            var repository = GetRepository(entity)!;
            return repository.Update(entity);
        }

        public static bool Delete(Entity entity)
        {
            var repository = GetRepository(entity)!;
            return repository.Delete(entity.Id);
        }

        public static List<Entity> ReadAll(Entity entity)
        {
            var repository = GetRepository(entity)!;
            return repository.ReadAll();
        }

        public static int DeleteAll(Entity entity)
        {
            var repository = GetRepository(entity)!;
            return repository.DeleteAll();
        }

        public static int Count(Entity entity)
        {
            var repository = GetRepository(entity)!;
            return repository.Count();
        }

        public static List<Entity> Read(Entity entity, int offset, int count)
        {
            var repository = GetRepository(entity)!;
            return repository.Read(offset, count);
        }
    }
}
