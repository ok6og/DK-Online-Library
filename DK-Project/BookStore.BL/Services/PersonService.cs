//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using BookStore.BL.Interfaces;
//using DK_Project.DL.Repositories.InMemoryRepositories;
//using DK_Project.Models.Models;

//namespace BookStore.BL.Services
//{
//    public class PersonService : IPersonService
//    {
//        public readonly IPersonRepository _personRepository;

//        public PersonService(IPersonRepository personRepository)
//        {
//            _personRepository = personRepository;
//        }

//        public Person? AddUser(Person user)
//        {
//            return _personRepository.AddUser(user);
//        }

//        public Person? DeleteUser(int userId)
//        {
//            return _personRepository.DeleteUser(userId);
//        }

//        public IEnumerable<Person> GetAllUsers()
//        {
//            return _personRepository.GetAllUsers();
//        }

//        public Person? GetById(int id)
//        {
//            return _personRepository.GetById(id);
//        }

//        public Person UpdateUser(Person user)
//        {
//            return _personRepository.UpdateUser(user);
//        }
//    }
//}
