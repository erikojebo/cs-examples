using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Examples._2_types_and_generics
{
    [TestFixture]
    public class _3_using_a_generic_repo
    {

        [Test]
        public void We_can_add_user_through_our_generic_repo()
        {
            var userRepo = new UserRepository();

            userRepo.Add(new User { Id = 3 });

            var user = userRepo.FindById(3);

            Assert.AreEqual(3, user.Id);
        }

        [Test]
        public void We_also_get_the_whole_list_of_typed_objects_back_from_repo()
        {
            var userRepo = new UserRepository();

            userRepo.Add(new User { Id = 3 });
            userRepo.Add(new User { Id = 4 });
            userRepo.Add(new User { Id = 6 });

            var actual = userRepo.FindAll().Count;
           
            Assert.AreEqual(3, actual);
        }

        [Test]
        public void We_can_put_type_constraints_on_our_generic_structures()
        {
            var entityRepo = new Repository<Person>();

            entityRepo.Add(new Person { Id = 5, Name = "Erik" });
            entityRepo.Add(new Person { Id = 6, Name = "DAFO" });

            var actual = entityRepo.FindAll().Count;
            var firstPerson = entityRepo.FindById(5);
            var secondPerson = entityRepo.FindById(6);
            

            Assert.AreEqual(2, actual);
            Assert.AreEqual("Erik", firstPerson.Name);
            Assert.AreEqual("DAFO", secondPerson.Name);
        }

        [Test]
        public void But_we_can_still_act_on_derived_types_own_members()
        {
            var entityRepo = new Repository<StockItem>();

            var expected = new StockItem {Id = 1, ItemName = "Useless Part", Category = "Parts"};
            entityRepo.Add(expected);

            var actual = entityRepo.FindById(1);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual("Useless Part", actual.ItemName);
            Assert.AreEqual("Parts", actual.Category);
        }

        [Test]
        public void Subclassing_helps_us_with_cases_specially_tailored_to_a_certain_type()
        {
            var entityRepo = new ItemRespository();

            var item1 = new StockItem { Id = 1, ItemName = "Useless Part", Category = "Parts" };
            var item2 = new StockItem { Id = 2, ItemName = "Useless Part 2", Category = "Parts" };
            var item3 = new StockItem { Id = 3, ItemName = "Useless Part 2", Category = "Misc" };
            
            entityRepo.Add(item1);
            entityRepo.Add(item2);
            entityRepo.Add(item3);

            var items = entityRepo.FindByCategoryName("Parts");
            var found1 = items[0];
            var found2 = items[1];

            Assert.AreEqual(2, items.Count);
            Assert.AreEqual(item1, found1);
            Assert.AreEqual(item2, found2);

        }

        //[Test]
        //public void Type_constraint_hinders_us_from_using_other_types()
        //{
        //    //No go
        //    var repo = new Repository<User>();
        //}
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
        protected List<T> _repo = new List<T>();

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

    public class StockItem : EntityBase
    {
        public string Category { get; set; }
        public string ItemName { get; set; }
    }
    #endregion

#region Subclassing for specialized search

    public class ItemRespository : Repository<StockItem>
    {
        public List<StockItem> FindByCategoryName(string category)
        {
            var items = _repo.Where(x => x.Category == category).ToList();

            return items;
        }
    }

#endregion

}
