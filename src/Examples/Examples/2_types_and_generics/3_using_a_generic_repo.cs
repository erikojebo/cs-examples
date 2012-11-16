using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Examples._2_types_and_generics
{
    [TestFixture]
    public class _3_using_a_generic_repo
    {
        private UserRepository _repository;
        private Repository<Person> _entityRepository;


        [SetUp]
        public void Test_Fixture_SetUp()
        {
            _repository = new UserRepository();
            _entityRepository = new Repository<Person>();

        }

        [Test]
        public void We_can_add_user_through_our_generic_repo()
        {
            _repository.Add(new User{Id = 3});

            var user = _repository.FindById(3);

            Assert.AreEqual(3, user.Id);
        }

        [Test]
        public void We_also_get_the_whole_list_of_typed_objects_back_from_repo()
        {
            _repository.Add(new User{Id = 3});
            _repository.Add(new User{Id = 4});
            _repository.Add(new User { Id = 6 });

            var actual = _repository.FindAll().Count;
           
            Assert.AreEqual(3, actual);
        }

        [Test]
        public void We_can_put_type_constraints_on_our_generic_structures()
        {
            _entityRepository.Add(new Person{Id = 5, Name = "Erik"});
            _entityRepository.Add(new Person { Id = 6, Name = "DAFO"});

            var actual = _entityRepository.FindAll().Count;
            var firstPerson = _entityRepository.FindById(5);
            var secondPerson = _entityRepository.FindById(6);
            

            Assert.AreEqual(2, actual);
            Assert.AreEqual("Erik", firstPerson.Name);
            Assert.AreEqual("DAFO", secondPerson.Name);
        }

        [Test]
        public void But_we_can_still_act_on_derived_types_own_members()
        {
            var expected = new Person {Id = 1, Name = "Henric"};
            _entityRepository.Add(expected);

            var actual = _entityRepository.FindById(1);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual("Henric", actual.Name);
        }

        [TearDown]
        public void RemoveTempEntriesFromInMemDbs()
        {
            _entityRepository = new Repository<Person>();
            _repository = new UserRepository();
        }
    }

    #region Regular Interface Repo
    public interface IRepository<T>
    {
        void Add(T t);
        T FindById(int Id);
        List<T> FindAll();
    }

    //Concrete implementation for a certain type
    public class UserRepository : IRepository<User>
    {
        private List<User> _FakeUserRepo = new List<User>();

        public void Add(User t)
        {
            _FakeUserRepo.Add(t);
        }

        public User FindById(int Id)
        {
            var user = _FakeUserRepo.FirstOrDefault(x => x.Id == Id);

            return user;
        }

        public List<User> FindAll()
        {
            return _FakeUserRepo;
        }
    }

    public class User
    {
        public int Id { get; set; }       
    }
    #endregion

    #region Base Repo Class with Type Constraint
    //This way we can get ALL our entities in a domain model through a simple base class repo of T
    public class Repository<T> where T : EntityBase
    {
        private List<T> _repo = new List<T>();

        public void Add(T t)
        {
            _repo.Add(t);
        }

        public T FindById(int Id)
        {
            var entity = _repo.FirstOrDefault(x => x.Id == Id);

            return entity;
        }

        public List<T> FindAll()
        {
            return _repo;
        }
    }

    public class EntityBase
    {
        public int Id { get; set; }
    }

    public class Person : EntityBase
    {
        public string Name { get; set; }
    }
    #endregion

}
